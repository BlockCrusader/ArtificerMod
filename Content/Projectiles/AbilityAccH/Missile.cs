using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class Missile : ModProjectile
	{
        public override void SetStaticDefaults()
        {
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

        public override void SetDefaults()
		{
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 300;
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

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			for (int l = 0; l < 2; l++)
			{
				float xOffset = 0f;
				float yOffset = 0f;
				if (l == 1)
				{
					xOffset = Projectile.velocity.X * 0.5f;
					yOffset = Projectile.velocity.Y * 0.5f;
				}
				if (Main.rand.NextBool(2))
				{
					int dust1 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + xOffset, Projectile.position.Y + 3f + yOffset) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, DustID.Torch, 0f, 0f, 100);
					Main.dust[dust1].scale *= 1.4f + (float)Main.rand.Next(10) * 0.1f;
					Main.dust[dust1].velocity *= 0.2f;
					Main.dust[dust1].noGravity = true;
				}
				if (Main.rand.NextBool(2))
				{
					int dust2 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + xOffset, Projectile.position.Y + 3f + yOffset) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, DustID.Smoke, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[dust2].fadeIn = 0.5f + (float)Main.rand.Next(5) * 0.1f;
					Main.dust[dust2].velocity *= 0.05f;
				}
			}

			// Homing effect
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 45f)
			{
				NPC target = GetTarget(800f);
				if(target != null)
                {
                    if (Projectile.localAI[0] == 0)
                    {
						Projectile.timeLeft = 300;
						Projectile.localAI[0] = -1;
					}
					Vector2 vectToTarget = (target.Center - Projectile.Center).SafeNormalize(-Vector2.UnitY) * 12.5f;
					Projectile.velocity = Vector2.Lerp(Projectile.velocity, vectToTarget, 0.1f);
				}
			}
		}

		public NPC GetTarget(float maxDetectDistance)
		{
			NPC target = null;

			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy() && Collision.CanHit(Projectile, npc))
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(npc.Center, Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						target = npc;
						sqrMaxDetectDistance = sqrDistanceToTarget;
					}
				}
			}
			return target;
		}

		public override void Kill(int timeLeft)
        {
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 128);
			Projectile.Center = Projectile.position;
			Projectile.maxPenetrate = -1;
			Projectile.penetrate = -1;
			Projectile.Damage();
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

			// Visual FX, from Rocket AI
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 22);
			Projectile.Center = Projectile.position;
			for (int i = 0; i < 30; i++)
			{
				int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 100, default, 1.5f);
				Main.dust[dust1].velocity *= 1.4f;
			}
			for (int j = 0; j < 20; j++)
			{
				int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default, 3.5f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 7f;
				dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default, 1.5f);
				Main.dust[dust2].velocity *= 3f;
			}
			for (int k = 0; k < 2; k++)
			{
				float smokeScalar = 0.4f;
				if (k == 1)
				{
					smokeScalar = 0.8f;
				}
				int smoke1 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64));
				Main.gore[smoke1].velocity *= smokeScalar;
				Main.gore[smoke1].velocity.X += 1f;
				Main.gore[smoke1].velocity.Y += 1f;
				smoke1 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64));
				Main.gore[smoke1].velocity *= smokeScalar;
				Main.gore[smoke1].velocity.X -= 1f;
				Main.gore[smoke1].velocity.Y += 1f;
				smoke1 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64));
				Main.gore[smoke1].velocity *= smokeScalar;
				Main.gore[smoke1].velocity.X += 1f;
				Main.gore[smoke1].velocity.Y -= 1f;
				smoke1 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, default, Main.rand.Next(61, 64));
				Main.gore[smoke1].velocity *= smokeScalar;
				Main.gore[smoke1].velocity.X -= 1f;
				Main.gore[smoke1].velocity.Y -= 1f;
			}
		}
    }
}