using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ArtificerMod.Content.Projectiles.AbilityAccPH
{
	class RepulseShockwave : ModProjectile
	{
        public override void SetDefaults()
		{
            Projectile.width = 24;
            Projectile.height = 68;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 200;
            Projectile.alpha = 255;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = height = 24;
            return true;
        }

        public override void AI()
        {
            if (Projectile.timeLeft > 10)
            {
                if (Projectile.alpha > 0)
                {
                    Projectile.alpha -= 30;
                }
                if (Projectile.alpha < 50)
                {
                    Projectile.alpha = 50;
                }
            }
            else
            {
                Projectile.alpha += 20;
                if (Projectile.alpha > 255)
                {
                    Projectile.alpha = 255;
                }
            }

            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
            if (Projectile.direction == 1)
            {
                DrawOffsetX = -6; 
                DrawOriginOffsetY = -6;
                DrawOriginOffsetX = 2;
            }
            else
            {
                DrawOffsetX = -2;
                DrawOriginOffsetY = -6;
                DrawOriginOffsetX = -2;
            }

            Projectile.velocity *= 0.99f;

            for (int i = 0; i < 2; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100);
                Main.dust[dust].scale = 0.1f + (float)Main.rand.Next(5) * 0.25f;
                Main.dust[dust].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Main.rand.NextVector2FromRectangle(Projectile.Hitbox);
            }
			if(Main.rand.NextBool(6))
            {
                int smoke = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, default, Main.rand.Next(61, 64));
                Main.gore[smoke].velocity = Projectile.velocity * 0.4f;
                Main.gore[smoke].scale *= Main.rand.NextFloat(0.4f, 1f);
                Main.gore[smoke].position = Main.rand.NextVector2FromRectangle(Projectile.Hitbox);
            }
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.8f);
            if(Projectile.damage <= 1 && Projectile.timeLeft > 10)
            {
                Projectile.timeLeft = 10;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity * 0.95f;
            Projectile.position -= Projectile.velocity;
            if (Projectile.timeLeft > 20)
            {
                Projectile.timeLeft = 20;
            }
            return false;
        }
    }
}