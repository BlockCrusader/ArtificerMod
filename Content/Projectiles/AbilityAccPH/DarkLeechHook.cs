using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using ArtificerMod.Content.Buffs.AbilityAccPH;
using System;
using ReLogic.Content;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Projectiles.AbilityAccPH
{
	public class DarkLeechHook : ModProjectile
	{
		private const string ChainTexturePath = "ArtificerMod/Content/Projectiles/AbilityAccPH/DarkLeechChain"; // The folder path to the chain sprite

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Default;
			Projectile.penetrate = -1;
			Projectile.hide = true;
			Projectile.alpha = 255;
			DrawOffsetX = -6;
			DrawOriginOffsetY = -3;
			DrawOriginOffsetX = 3;
			Projectile.extraUpdates = 1;
		}

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			if (Projectile.ai[0] == 1f) 
			{
				int npcIndex = (int)Projectile.ai[1];
				if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
				{
					if (Main.npc[npcIndex].behindTiles)
					{
						behindNPCsAndTiles.Add(index);
					}
					else
					{
						behindNPCs.Add(index);
					}

					return;
				}
			}
			
			behindProjectiles.Add(index);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			
			return projHitbox.Intersects(targetHitbox);
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			HookRetracting = true;
			return false;
        }

        public override Color? GetAlpha(Color lightColor)
		{
			Color drawColor = Lighting.GetColor((int)Projectile.position.X / 16, (int)(Projectile.position.Y / 16f));
			return drawColor;
		}

		public bool IsStickingToTarget
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public int TargetWhoAmI
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public bool HookRetracting
		{
			get => Projectile.ai[2] == 1f;
			set => Projectile.ai[2] = value ? 1f : 0f;
		}

		private const int MAX_TETHERS = 256; 
		private readonly Point[] _stickingTethers = new Point[MAX_TETHERS];

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (!HookRetracting)
            {
				IsStickingToTarget = true; 
				TargetWhoAmI = target.whoAmI; 
				Projectile.velocity =
					(target.Center - Projectile.Center) *
					0.75f; 
				Projectile.netUpdate = true; 
				target.AddBuff(ModContent.BuffType<Leech>(), 600);
				Projectile.damage = 0;

				UpdateTethers(target);
			}
		}

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			modifiers.DamageVariationScale *= 0f; // Disable damage variation
			modifiers.ScalingArmorPenetration += 1f; // Ignore armor

            // Manually address crit chance
            if (Main.player[Projectile.owner].GetTotalCritChance(DamageClass.Generic) > Main.rand.Next(100))
            {
				modifiers.SetCrit();
			}
            else
            {
				modifiers.DisableCrit();
            }
        }

        private void UpdateTethers(NPC target)
		{
			int currentProjIndex = 0; 

			for (int i = 0; i < Main.maxProjectiles; i++) 
			{
				Projectile currentProjectile = Main.projectile[i];
				if (i != Projectile.whoAmI 
					&& currentProjectile.active 
					&& currentProjectile.owner == Main.myPlayer 
					&& currentProjectile.type == Projectile.type 
					&& currentProjectile.ModProjectile is DarkLeechHook leechProjectile 
					&& leechProjectile.IsStickingToTarget 
					&& leechProjectile.TargetWhoAmI == target.whoAmI)
				{

					_stickingTethers[currentProjIndex++] = new Point(i, currentProjectile.timeLeft); 
					if (currentProjIndex >= _stickingTethers.Length) 
						break;
				}
			}
		}

		bool farFromOwner = false;
		public override void AI()
		{
			if (Main.player[Projectile.owner].dead)
			{
				Projectile.Kill();
				return;
			}
			Vector2 vectorToOwner = Main.player[Projectile.owner].Center - Projectile.Center;
			if (vectorToOwner.Length() > 1375f)
			{
				Projectile.Kill();
			}
			else if (vectorToOwner.Length() > (IsStickingToTarget ? 1250f : 500f)) // Retract the hook if it's too far away
			{
				HookRetracting = true;
			}
			if(vectorToOwner.Length() > 1200)
            {
				farFromOwner = true;
			}

			bool getArtificer = Main.player[Projectile.owner].TryGetModPlayer(out ArtificerPlayer artificer);

			if (!getArtificer || artificer.tetherLeech <= 0)
			{
				HookRetracting = true;
			}

			Projectile.timeLeft = 5;

            if (HookRetracting)
            {
				IsStickingToTarget = false;
				Projectile.tileCollide = false;
				Projectile.ignoreWater = true;
				Projectile.netUpdate = true;
			}

			Projectile.rotation = vectorToOwner.ToRotation() + MathHelper.Pi;

			UpdateAlpha();
			
			if (IsStickingToTarget) StickyAI();
			else NormalAI(vectorToOwner);
		}

		private void UpdateAlpha()
		{
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 25;
			}

			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
		}

		// Adapted from grappling hook AI
		private void NormalAI(Vector2 vectToOwner)
		{
			float distToOwner = vectToOwner.Length();

            if (HookRetracting)
            {
				float spdFacor = 16f;
				if (distToOwner < 24f)
				{
					Projectile.Kill();
				}
                if (farFromOwner)
                {
					spdFacor = 20f;
				}
				float retractSpd = spdFacor / distToOwner;
				float retractSpdX = (Main.player[Projectile.owner].Center.X - Projectile.Center.X) * retractSpd;
				float retractSpdY = (Main.player[Projectile.owner].Center.Y - Projectile.Center.Y) * retractSpd;
				Projectile.velocity.X = retractSpdX;
				Projectile.velocity.Y = retractSpdY;
			}
		}

		private void StickyAI()
		{
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = false; 

			Projectile.localAI[0] += 1f;

			// Every 60 ticks, perform a hit effect
			bool hitEffect = Projectile.localAI[0] % 120f == 0f;
			int projTargetIndex = (int)TargetWhoAmI;
			if (Projectile.localAI[0] > 1240 || projTargetIndex < 0 || projTargetIndex >= 200)
			{ 
				HookRetracting = true;
				return;
			}
			else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage)
			{ 
			  
				Projectile.Center = Main.npc[projTargetIndex].Center - Projectile.velocity * 2f;
				Projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;

				if (hitEffect)
				{ 
					Main.npc[projTargetIndex].HitEffect(0, 0.0);
					
					Lifesteal(Main.npc[projTargetIndex]);
				}
			}
			else
			{ 
				HookRetracting = true;
				return;
			}
		}

		public void Lifesteal(NPC target)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				float hpTracker = 0f;
				int healTargetID = Projectile.owner;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && !Main.player[i].dead &&
						((!Main.player[Projectile.owner].hostile && !Main.player[i].hostile) || Main.player[Projectile.owner].team == Main.player[i].team)
						&& Vector2.Distance(Projectile.Center, Main.player[i].Center) < 1400f 
						&& (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > hpTracker)
					{
						hpTracker = Main.player[i].statLifeMax2 - Main.player[i].statLife;
						healTargetID = i;
					}
				}
				
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Projectile.velocity * 0f, ModContent.ProjectileType<DarkLeechHeal>(), 0, 0f, Projectile.owner, healTargetID, 3f);
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Player owner = Main.player[Projectile.owner];

			Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);

			Rectangle? chainSourceRectangle = null;
			float chainHeightAdjustment = 0f; 

			Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
			Vector2 chainDrawPosition = Projectile.Center;
			Vector2 vectorFromProjectileToPlayer = owner.Center - chainDrawPosition;
			Vector2 unitVectorFromProjectileToPlayer = vectorFromProjectileToPlayer.SafeNormalize(Vector2.Zero);
			float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
			if (chainSegmentLength == 0)
				chainSegmentLength = 10; // When the chain texture is being loaded, the height is 0 which would cause infinite loops.
			float chainRotation = unitVectorFromProjectileToPlayer.ToRotation() + MathHelper.PiOver2;
			int chainCount = 0;
			float chainLengthRemainingToDraw = vectorFromProjectileToPlayer.Length() + chainSegmentLength / 2f;

			while (chainLengthRemainingToDraw > 0f)
			{
				Color chainDrawColor = Lighting.GetColor((int)chainDrawPosition.X / 16, (int)(chainDrawPosition.Y / 16f));

				var chainTextureToDraw = chainTexture;

				Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, chainDrawColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);

				chainDrawPosition += unitVectorFromProjectileToPlayer * chainSegmentLength;
				chainCount++;
				chainLengthRemainingToDraw -= chainSegmentLength;
			}

			return true;
		}
	}
}