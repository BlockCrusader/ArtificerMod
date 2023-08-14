using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	public class MoonfallFlare : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 150;
		}

		public override void AI()
        {
			for (int i = 0; i < 3; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Phantasmal, 0f, 0f, 0, default(Color), 0.9f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 0f;
				Main.dust[dustIndex].position.Y -= (0f - Projectile.velocity.Y * 0.334f) * (float)i;
			}
		}
    }
}