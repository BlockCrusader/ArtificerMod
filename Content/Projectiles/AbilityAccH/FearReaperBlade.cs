using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{

	public class FearReaperBlade : ModProjectile
	{
		private const string ChainTexturePath = "ArtificerMod/Content/Projectiles/AbilityAccH/FearReaperChain"; 

		public override void SetDefaults()
		{
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
			Projectile.netImportant = true; // This ensures that the projectile is synced when other players join the world.
			Projectile.width = 32; 
			Projectile.height = 32; 
			Projectile.friendly = true; 
			Projectile.penetrate = -1; 
			Projectile.DamageType = DamageClass.Generic;
			Projectile.timeLeft = 930;

			DrawOffsetX = -1;
			DrawOriginOffsetY = 0;
			DrawOriginOffsetX = -10;
		}

		readonly float orbitSpeed = 4f;
		readonly float orbitDirection = 1;
        public override void OnSpawn(IEntitySource source)
        {
			if (Projectile.ai[0] == 1)
			{
				Projectile.ai[1] = 15f;
			}
			else
			{
				Projectile.ai[1] = 30f;
			}

			Projectile.ai[2] = 30f;

			int particleNum = Main.rand.Next(5, 11);
			DefineOrbitPos(Main.player[Projectile.owner], out Vector2 orbitPos);
			for (int i = 0; i < particleNum; i++)
			{
				ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.BlackLightningSmall, new ParticleOrchestraSettings
				{
					PositionInWorld = Main.rand.NextVector2FromRectangle(Projectile.Hitbox) + (orbitPos - Projectile.Center)
				}, Projectile.owner);
			}
		}

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			if (!owner.active || owner.dead)
			{
				Projectile.Kill();
				return;
			}
			if (!owner.TryGetModPlayer(out ArtificerPlayer artificer) || (!artificer.fearReaper && !artificer.fCharm))
			{
				Projectile.Kill();
				return;
			}

			DefineOrbitPos(owner, out Vector2 orbitPos);

			Projectile.Center = orbitPos;
		}

		private void DefineOrbitPos(Player owner, out Vector2 orbitPos)
		{
			Projectile.ai[1] += 1f;
			Vector2 projOffset = new Vector2(1f).RotatedBy((float)Math.PI * orbitSpeed * (Projectile.ai[1] / 60f) * orbitDirection);

			orbitPos = owner.Center + projOffset * Projectile.ai[2];

			Projectile.rotation = ((float)Math.PI * orbitSpeed * (Projectile.ai[1] / 60f) * orbitDirection) + (3f * MathHelper.PiOver4);

			if(Projectile.ai[2] < 90f && Projectile.timeLeft > 65)
            {
				Projectile.ai[2]++;
            }
			else if(Projectile.ai[2] > 30f && Projectile.timeLeft <= 65)
            {
				Projectile.ai[2]--;
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			// Buff DR effect of Fear Reaper
			Player owner = Main.player[Projectile.owner];
			if(owner.TryGetModPlayer(out ArtificerPlayer artificer))
            {
				artificer.reaperDRCounter++;
            }

			for (int i = 0; i < 2; i++)
			{
				ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.BlackLightningSmall, new ParticleOrchestraSettings
				{
					PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox)
				}, Projectile.owner);
			}
		}

        public override bool PreDraw(ref Color lightColor)
		{
			Player owner = Main.player[Projectile.owner];
			if (!owner.active || owner.dead)
			{
				return true;
			}

			Vector2 flailPos = owner.Center;

			Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);

			Rectangle? chainSourceRectangle = null;
			float chainHeightAdjustment = 0f; 

			Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
			Vector2 chainDrawPosition = Projectile.Center;
			Vector2 vectorFromProjectileToOwner = flailPos.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
			Vector2 unitVectorFromProjectileToOwner = vectorFromProjectileToOwner.SafeNormalize(Vector2.Zero);
			float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
			if (chainSegmentLength == 0)
				chainSegmentLength = 10; // When the chain texture is being loaded, the height is 0 which would cause infinite loops.
			float chainRotation = unitVectorFromProjectileToOwner.ToRotation() + MathHelper.PiOver2;
			int chainCount = 0;
			float chainLengthRemainingToDraw = vectorFromProjectileToOwner.Length() + chainSegmentLength / 2f;

			while (chainLengthRemainingToDraw > 0f)
			{
				Color chainDrawColor = Lighting.GetColor((int)chainDrawPosition.X / 16, (int)(chainDrawPosition.Y / 16f));

				var chainTextureToDraw = chainTexture;

				Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, chainDrawColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);

				chainDrawPosition += unitVectorFromProjectileToOwner * chainSegmentLength;
				chainCount++;
				chainLengthRemainingToDraw -= chainSegmentLength;
			}
			return true;
		}

        public override void Kill(int timeLeft)
        {
			int particleNum = Main.rand.Next(5, 11);
			for (int i = 0; i < particleNum; i++)
			{
				ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.BlackLightningSmall, new ParticleOrchestraSettings
				{
					PositionInWorld = Main.rand.NextVector2FromRectangle(Projectile.Hitbox)
				}, Projectile.owner);;
			}
		}
    }
}