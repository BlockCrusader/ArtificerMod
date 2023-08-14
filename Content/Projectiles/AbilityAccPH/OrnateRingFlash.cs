using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccPH
{
	class OrnateRingFlash : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			DrawOffsetX = -5;

			// This is only a visual effect, so it deals no damage
			Projectile.penetrate = -1;
			Projectile.timeLeft = 20;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f);
		}

		Vector2 startPos;
		public override void OnSpawn(IEntitySource source)
		{
			startPos = Projectile.Center;
		}

		public override void AI()
		{
			// Makes sure the beam is harmless, since it's a visual effect
			Projectile.damage = 0;
			Projectile.knockBack = 0;

			Projectile.localAI[0]++;
			Projectile.alpha = (int)MathHelper.Lerp(0f, 255f, Projectile.localAI[0] / 20f);

			Projectile.Center = startPos + (Projectile.velocity * 0.6f);
			Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
	}
}