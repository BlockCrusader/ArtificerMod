using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	public class Supernova : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;

			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.timeLeft = 1;
			Projectile.penetrate = 1;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			float velocityScalar = Projectile.ai[0] / 0.7f;

			Vector2 extraVelocity = target.Center -Main.player[Projectile.owner].Center;
			extraVelocity = extraVelocity.SafeNormalize(Vector2.Zero) * (16f * velocityScalar);

			int dustNum = (int)(Projectile.ai[0] / 5f + Main.rand.Next(-3, 4));
			dustNum = Math.Clamp(dustNum, 5, 40);
			for (int j = 0; j < dustNum; j++)
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

				int dust = Dust.NewDust(Projectile.Center, 0, 0, dustID, 0f, 0f, 0, default(Color), 1.25f);
				Main.dust[dust].velocity *= 1.6f;
				Main.dust[dust].velocity.Y -= 1f;
				Main.dust[dust].velocity += extraVelocity;
				Main.dust[dust].noGravity = true;
			}
		}
    }
}