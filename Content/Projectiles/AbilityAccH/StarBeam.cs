using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class StarBeam : ModProjectile
	{
        public override void SetStaticDefaults()
        {
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

        public override void SetDefaults()
		{
            Projectile.width = 20;
            Projectile.height = 20;
			Projectile.scale = 2f;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
			Projectile.extraUpdates = 4;
            Projectile.timeLeft = 1200;
			Projectile.tileCollide = false;
		}

		float star1rot;
		float star1rotDir;
		float star1rotSpd;
		float star2rot;
		float star2rotDir;
		float star2rotSpd;
		float star3rot;
		float star3rotDir;
		float star3rotSpd;
        public override void OnSpawn(IEntitySource source)
        {
			// Visual setup
			star1rotDir = Main.rand.NextBool() ? 1 : -1;
			star2rotDir = Main.rand.NextBool() ? 1 : -1;
			star3rotDir = Main.rand.NextBool() ? 1 : -1;
			star1rotSpd = Main.rand.NextFloat(0.01f, 0.0175f);
			star2rotSpd = Main.rand.NextFloat(0.015f, 0.02f);
			star3rotSpd = Main.rand.NextFloat(0.02f, 0.025f);
			star1rot += Main.rand.NextFloat(0f, 2f * MathHelper.Pi);
			star2rot += Main.rand.NextFloat(0f, 2f * MathHelper.Pi);
			star3rot += Main.rand.NextFloat(0f, 2f * MathHelper.Pi);
		}

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			
			star1rot += star1rotSpd * star1rotDir;
			star2rot += star2rotSpd * star2rotDir;
			star3rot += star3rotSpd * star3rotDir;

			// Homing effect
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 45f && Projectile.ai[1] != 1)
			{
				NPC target = GetTarget(1200f);
				if(target != null)
                {
					Projectile.timeLeft = 600;
					Vector2 vectToTarget = (target.Center - Projectile.Center).SafeNormalize(-Vector2.UnitY) * 12.5f;
					Projectile.velocity = Vector2.Lerp(Projectile.velocity, vectToTarget, 0.015f);
				}
			}

            // Explosion
            if (Projectile.ai[1] == 1)
            {
				Projectile.velocity *= 0f; 
				Projectile.ai[2] += 1f;
                if (Projectile.ai[2] >= 3)
                {
					// Creates secondary projectiles
					for (int i = 0; i < 3; i++)
                    {
						Vector2 vecctor = Main.rand.NextVector2CircularEdge(4f, 4f);
						vecctor *= Main.rand.NextFloat(0.8f, 1.2f);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vecctor, ModContent.ProjectileType<NovaFrag>(),
								Projectile.damage, Projectile.knockBack, Projectile.owner);
					}

					// Manual explosion effect that hits all enemies within 20 tiles every 3 updates/ticks (Max 4 hits)

					float sqrMaxRange = 320f * 320f; 
					for (int i = 0; i < 200; i++)
					{
						NPC target = Main.npc[i];

						float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
						if (sqrDistanceToTarget < sqrMaxRange)
						{
							// Gets a value from 0.5 to 2 to scale damage and knockback; enemies closer to the center are more effected
							float epicenterFactor = 0.5f + (0.075f * (320f / Vector2.Distance(target.Center, Projectile.Center)));
							epicenterFactor = MathHelper.Clamp(epicenterFactor, 0.5f, 2f);

							int blastDmg = (int)(Projectile.damage * epicenterFactor);
							float blastKb = (int)(Projectile.knockBack * epicenterFactor);

							Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<Supernova>(),
								blastDmg, blastKb, Projectile.owner, epicenterFactor);
						}
					}

					int dustNum = (int)(90 * (1.6f * (Projectile.timeLeft / 10f)));
					int dustNum2 = (int)(dustNum * 0.75f);
					int dustNum3 = (int)(dustNum * 0.5f);

					float scaleFactor = Main.rand.NextFloat(0.8f, 1.4f) * (1.6f * (Projectile.timeLeft / 10f));

					for (int i = 0; i < dustNum; i++)
					{
						Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
						Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 30, GetDustType(), speed * 40f, Scale: 1.5f * scaleFactor);
						d.fadeIn = 1.4f * scaleFactor;
						d.noGravity = true;
					}
					for (int i = 0; i < dustNum2; i++)
					{
						Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
						Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 10, GetDustType(), speed * 30f, Scale: 1.25f * scaleFactor);
						d.fadeIn = 1.2f * scaleFactor;
						d.noGravity = true;
					}
					for (int i = 0; i < dustNum3; i++)
					{
						Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

						Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 20, GetDustType(), speed * 20f, Scale: 1f * scaleFactor);
						d.fadeIn = 1f * scaleFactor;
						d.noGravity = true;
					}

					Projectile.ai[2] = 0;
				}
			}
		}

		private int GetDustType()
        {
			switch (Main.rand.Next(4))
			{
				case 0:
					return DustID.YellowStarDust;
				case 1:
					return DustID.FireworkFountain_Yellow;
				case 2:
					return DustID.Firework_Yellow;
				default:
					return DustID.YellowTorch;
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
			if (target == GetTarget(1200f) && Projectile.ai[1] != 1)
			{
				SoundStyle blastSound = new SoundStyle($"{nameof(ArtificerMod)}/Assets/SupernovaBlast")
				{
					Volume = 1f,
					PitchVariance = 0.15f,
					MaxInstances = 3,
				};
				SoundEngine.PlaySound(blastSound, Projectile.Center);

				modifiers.SourceDamage *= 3f; // Deals extra damage to its lock-on target

				// Triggers explosion
				Projectile.timeLeft = 13;
				Projectile.ai[1] = 1;
			}
		}

		public NPC GetTarget(float maxDetectDistance)
		{
			NPC target = null;

			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
			float recordHP = 0;
			float recordClosest = float.MaxValue;

			// Selects in-range target with most HP
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy())
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(npc.Center, Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						if (npc.life > recordHP)
						{
							target = npc;
							recordHP = npc.life;
							recordClosest = sqrDistanceToTarget;
						}
						// Distance-based tie-breaker
						else if (npc.life == recordHP && sqrDistanceToTarget < recordClosest)
						{
							target = npc;
							recordHP = npc.life;
							recordClosest = sqrDistanceToTarget;
						}
					}
				}
			}
			return target;
		}

		public override void Kill(int timeLeft)
        {
			// Dust FX for despawn if the explosion never happened
            if (Projectile.ai[1] != 1)
            {
				SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);

				for (int i = 0; i < 36; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

					Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 15, GetDustType(), speed * 15f, Scale: 1.2f);
					d.fadeIn = 1f;
					d.noGravity = true;
				}
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
            if (Projectile.ai[1] == 1)
            {
				return false;
            }

			VisualHelpers.TrailFX1(Projectile, Vector2.Zero, new Color(255, 220, 80), 12);
			return false;
		}

		public override void PostDraw(Color lightColor)
		{
			if (Projectile.ai[1] == 1 || Projectile.ai[0] == 0)
			{
				return;
			}

			VisualHelpers.VanillaStarFX(Projectile, new Color(255, 220, 80), 0.6f, new Color(255, 150, 50), 0.4f, 40);

			for (int i = 0; i < 3; i++)
            {
				float size = ((float)i + 1);
				size = MathHelper.Clamp(size, 1.5f, 2.5f);

				float rotation;
                switch (i)
                {
					case 2:
						rotation = star3rot;
						break;	
					case 1:
						rotation = star2rot;
						break;
					default:
						rotation = star1rot;
						break;
                }

				VisualHelpers.VanillaSparkleFX(Projectile.Opacity, SpriteEffects.None, Projectile.Center - Main.screenPosition,
				new Color(255, 255, 255, 0) * 0.5f, new Color(255, 220, 80),
				0.6f, 0f, 0.5f, 1.01f, 1.01f,
				rotation, Vector2.One * size, Vector2.One);
			}
		}
	}
}