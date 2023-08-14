using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.ArmorH
{
	class LihzahrdArmorBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.extraUpdates = 4;
			Projectile.timeLeft = 50; 

			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
		}

        public override void AI()
        {
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}

        public override bool PreDraw(ref Color lightColor)
		{
			VisualHelpers.TrailFX2(Projectile, new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f), 75f);
			return false;
		}
	}
}