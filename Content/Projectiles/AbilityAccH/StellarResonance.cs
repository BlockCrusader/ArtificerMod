using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	public class StellarResonance : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 2;
			Projectile.height = 2;
			Projectile.aiStyle = -1;
			Projectile.hostile = false;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 3000;
			Projectile.extraUpdates = 4;
		}

		float orbitRadius; 
		float orbitTimer;
		float orbitSpeed;
		float orbitDirection;
		float rotDir;
		float rotSpeed;
		float sizeScalar;
		bool orbitLock = false;
        public override void OnSpawn(IEntitySource source)
        {
			orbitRadius = (float)Main.rand.Next(35, 66);
			orbitSpeed = 0.075f * (orbitRadius / 30f) * Main.rand.NextFloat(0.7f, 1.1f);
			orbitTimer = (float)Main.rand.Next(1, 60);
			orbitDirection = Main.rand.NextBool() ? 1 : -1;
			rotDir = Main.rand.NextBool() ? 1 : -1;
			rotSpeed = Main.rand.NextFloat(0.0075f, 0.015f);
			sizeScalar = Main.rand.NextFloat(0.35f, 0.65f);

			for (int i = 0; i < 3; i++)
			{
				int dustID;
				switch (Main.rand.Next(4))
				{
					case 0:
						dustID = DustID.YellowStarDust;
						break;
					case 1:
						dustID = DustID.FireworkFountain_Yellow;
						break;
					case 2:
						dustID = DustID.Firework_Yellow;
						break;
					default:
						dustID = DustID.YellowTorch;
						break;
				}

				Vector2 speed = Main.rand.NextVector2Circular(1.5f, 1.5f);
				Dust d = Dust.NewDustPerfect(Projectile.Center, dustID, speed, Scale: 0.3f);
				d.noGravity = true;
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

			Lighting.AddLight(Projectile.Center, .65f, .6f, .15f);

			if (Main.rand.NextBool(200))
			{
				int dustID;
				switch (Main.rand.Next(4))
				{
					case 0:
						dustID = DustID.YellowStarDust;
						break;
					case 1:
						dustID = DustID.FireworkFountain_Yellow;
						break;
					case 2:
						dustID = DustID.Firework_Yellow;
						break;
					default:
						dustID = DustID.YellowTorch;
						break;
				}

				Vector2 speed = Main.rand.NextVector2Circular(1.5f, 1.5f);
				Dust d = Dust.NewDustPerfect(Projectile.Center, dustID, speed, Scale: 0.4f);
				d.noGravity = true;
			}

			Projectile.rotation += rotSpeed * rotDir;

			if (Projectile.timeLeft > 60)
            {
				DefineOrbitPos(owner, out Vector2 orbitPos);

				// Homes in on the position defined above
				if ((orbitPos - Projectile.Center).Length() > 5f && orbitLock == false)
				{
					Projectile.velocity = (orbitPos - Projectile.Center).SafeNormalize(Vector2.Zero) * 10f;
				}
				else // Once close enough, just set the projectile position to the orbit postion.
				{
					orbitLock = true;
					Projectile.Center = orbitPos;
				}
			}
            else
            {
				Vector2 finalDest = new Vector2(owner.Center.X, owner.Center.Y - 80f);
				if ((finalDest - Projectile.Center).Length() > 5f)
				{
					Projectile.velocity = (finalDest - Projectile.Center).SafeNormalize(Vector2.Zero) * 3f;
				}
				else 
				{
					Projectile.Kill();
				}
			}
		}

		private void DefineOrbitPos(Player owner, out Vector2 orbitPos)
		{
			orbitTimer += 1f;
			
			Vector2 offsetFromPlayer = new Vector2(1f).RotatedBy((float)Math.PI * orbitSpeed * (orbitTimer / 60f) * orbitDirection);

			orbitPos = owner.MountedCenter + offsetFromPlayer * orbitRadius;
		}

		public override bool? CanDamage()
        {
			return false;
        }

		public override bool PreDraw(ref Color lightColor)
		{
			return false;
		}

		public override void PostDraw(Color lightColor)
		{
			float shineAmount = 0.75f - (0.75f * (Projectile.timeLeft / 3000f));

			VisualHelpers.VanillaSparkleFX(Projectile.Opacity, SpriteEffects.None, Projectile.Center - Main.screenPosition,
				new Color(255, 255, 255, 0) * 0.5f, new Color(255, 220, 80),
				shineAmount + 0.25f, 0f, 0.9f, 0.99f, 1f,
				Projectile.rotation, Vector2.One * sizeScalar, Vector2.One);
		}

		public override void Kill(int timeLeft)
		{
			Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), Math.PI);
			Vector2 dustOffset = new Vector2(1.05f, 1f);
			for (float i = 0f; i < 5f; i++)
			{
				int dustID;
				switch (Main.rand.Next(4))
				{
					case 0:
						dustID = DustID.YellowStarDust;
						break;
					case 1:
						dustID = DustID.FireworkFountain_Yellow;
						break;
					case 2:
						dustID = DustID.Firework_Yellow;
						break;
					default:
						dustID = DustID.YellowTorch;
						break;
				}


				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, dustID, 0f, 0f, 0, Color.Transparent, 0.2f);
				Main.dust[dustIndex].position = Projectile.Center;

				double dustRot = (float)Math.PI * 2f * i / 5f;
				Main.dust[dustIndex].velocity = spinningpoint.RotatedBy(dustRot) * dustOffset * (0.8f + Main.rand.NextFloat() * 0.2f) * 0.6f;
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].scale += Main.rand.NextFloat() * 0.2f;
				Main.dust[dustIndex].scale *= 0.25f;
				Main.dust[dustIndex].fadeIn = 1.5f;
			}
		}
	}
}