using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccPH
{
	class RepulseBomb : ModProjectile
	{
        public override void SetDefaults()
		{
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 150;
            Projectile.alpha = 255;
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

			Projectile.rotation += 0.05f * Projectile.direction;
			Projectile.rotation += Projectile.velocity.X * 0.1f;

			Projectile.velocity.Y += 0.3f;
			if(Projectile.velocity.Y > 16f)
            {
				Projectile.velocity.Y = 16f;
            }

			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100);
			Main.dust[dust].scale = 0.5f + (float)Main.rand.Next(5) * 0.1f;
			Main.dust[dust].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.2f;
			Main.dust[dust].noGravity = true;
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

			for (int n = 0; n < 2; n++)
            {
				Vector2 projVelocity = n == 1 ? new Vector2(10f, 0f) : new Vector2(-10f, 0f);
				int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, projVelocity, ModContent.ProjectileType<RepulseShockwave>(), (int)(Projectile.damage / 2f), Projectile.knockBack, Projectile.owner);
				Main.projectile[proj].Center = Projectile.Center;
			}

			for (int i = 0; i < 20; i++)
			{
				int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.25f);
				Main.dust[dust1].velocity *= 1f;
			}
			for (int j = 0; j < 10; j++)
			{
				int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 5f;
				dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.25f);
				Main.dust[dust2].velocity *= 2f;
			}
			for (int k = 0; k < 2; k++)
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