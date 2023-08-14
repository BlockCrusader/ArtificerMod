using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccPH
{
	class RepulseMissile : ModProjectile
	{
        public override void SetDefaults()
		{
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            DrawOffsetX = -1;
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 100;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			if (Math.Abs(Projectile.velocity.X) >= 8f || Math.Abs(Projectile.velocity.Y) >= 8f)
			{
				for (int n = 0; n < 2; n++)
				{
					float xVel = 0f;
					float yVel = 0f;
					if (n == 1)
					{
						xVel = Projectile.velocity.X * 0.5f;
						yVel = Projectile.velocity.Y * 0.5f;
					}
					int dust1 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + xVel, 
						Projectile.position.Y + 3f + yVel) - Projectile.velocity * 0.25f, 
						Projectile.width - 8, Projectile.height - 8, 6, 0f, 0f, 100);

					Main.dust[dust1].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
					Main.dust[dust1].velocity *= 0.2f;
					Main.dust[dust1].noGravity = true;

					dust1 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + xVel, Projectile.position.Y + 3f + yVel) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[dust1].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
					Main.dust[dust1].velocity *= 0.05f;
				}
			}
			int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 100);
			Main.dust[dust2].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
			Main.dust[dust2].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
			Main.dust[dust2].noGravity = true;
			Main.dust[dust2].position = Projectile.Center + new Vector2(0f, -Projectile.height / 2).RotatedBy(Projectile.rotation) * 1.1f;
			Dust dust3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100);
			dust3.scale = 1f + (float)Main.rand.Next(5) * 0.1f;
			dust3.noGravity = true;
			dust3.position = Projectile.Center + new Vector2(0f, -Projectile.height / 2 - 6).RotatedBy(Projectile.rotation) * 1.1f;
		}

        public override void Kill(int timeLeft)
        {
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 100);
			Projectile.Center = Projectile.position;
			Projectile.maxPenetrate = -1;
			Projectile.penetrate = -1;
			Projectile.Damage();
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

			for (int i = 0; i < 30; i++)
			{
				int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.25f);
				Main.dust[dust1].velocity *= 1f;
			}
			for (int j = 0; j < 15; j++)
			{
				int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 5f;
				dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.25f);
				Main.dust[dust2].velocity *= 2f;
			}
			for (int k = 0; k < 3; k++)
			{
				float smokeScalar = 0.15f * (1+ k);
				int smoke1 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64));
				Main.gore[smoke1].velocity *= smokeScalar;
				Main.gore[smoke1].scale *= 1.5f * smokeScalar;
				Main.gore[smoke1].velocity.X += 1f;
				Main.gore[smoke1].velocity.Y += 1f;
				smoke1 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64));
				Main.gore[smoke1].velocity *= smokeScalar;
				Main.gore[smoke1].scale *= 1.5f * smokeScalar;
				Main.gore[smoke1].velocity.X -= 1f;
				Main.gore[smoke1].velocity.Y += 1f;
				smoke1 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64));
				Main.gore[smoke1].velocity *= smokeScalar;
				Main.gore[smoke1].scale *= 1.5f * smokeScalar;
				Main.gore[smoke1].velocity.X += 1f;
				Main.gore[smoke1].velocity.Y -= 1f;
				smoke1 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64));
				Main.gore[smoke1].velocity *= smokeScalar;
				Main.gore[smoke1].scale *= 1.5f * smokeScalar;
				Main.gore[smoke1].velocity.X -= 1f;
				Main.gore[smoke1].velocity.Y -= 1f;
			}
		}
    }
}