using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AccessoriesH
{

	public class TwinLaser : ModProjectile
	{

		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.alpha = 255;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 300;
			Projectile.ArmorPenetration = 25;
		}

		public override void AI()
		{
			// drawing/visuals
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			DrawOffsetX = 1;
			Lighting.AddLight(Projectile.Center, 0.5f, 0.05f, 0.05f);

			// Fade in
			if (Projectile.alpha > 0)
				Projectile.alpha -= 15;
			if (Projectile.alpha < 0)
				Projectile.alpha = 0;
		}


        public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f);
		}

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			modifiers.DamageVariationScale *= 0f; // No damage randomization, to match the Shield's bash
        }
    }
}