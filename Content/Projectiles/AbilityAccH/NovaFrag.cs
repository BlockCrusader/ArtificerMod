using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class NovaFrag : ModProjectile
	{
        public override void SetStaticDefaults()
        {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

        public override void SetDefaults()
		{
            Projectile.width = 6;
            Projectile.height = 6;
			Projectile.scale = 1f;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
			Projectile.extraUpdates = 4;
            Projectile.timeLeft = 1200;
			Projectile.tileCollide = false;
		}

		float star1rot;
		float star1rotDir;
		float star1rotSpd;
		float star2rot;
		float star2rotDir;
		float star2rotSpd;
		public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			if(Projectile.ai[0] == 0) // Visual setup
            {
				star1rotDir = Main.rand.NextBool() ? 1 : -1;
				star2rotDir = Main.rand.NextBool() ? 1 : -1;
				star1rotSpd = Main.rand.NextFloat(0.01f, 0.0175f);
				star2rotSpd = Main.rand.NextFloat(0.015f, 0.02f);
				star1rot += Main.rand.NextFloat(0f, 2f * MathHelper.Pi);
				star2rot += Main.rand.NextFloat(0f, 2f * MathHelper.Pi);
			}
			star1rot += star1rotSpd * star1rotDir;
			star2rot += star2rotSpd * star2rotDir;

			Projectile.ai[0]++;

			if (Main.rand.NextBool(10))
            {
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, GetDustType(), 0f, 0f, 100);
				Main.dust[dust].scale = 0.7f + (float)Main.rand.Next(5) * 0.12f;
				Main.dust[dust].noGravity = true;
			}
		}

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
			int hitboxSize = 10;
			if(Projectile.timeLeft >= 1199) // XL hitbox for first frames
            {
				hitboxSize = 40;
			}
			projHitbox.Inflate(hitboxSize, hitboxSize);
            return projHitbox.Intersects(targetHitbox);
        }

        private int GetDustType()
        {
			switch (Main.rand.Next(4))
			{
				case 0:
					return DustID.YellowStarDust;
				case 1:
					return DustID.FireworkFountain_Yellow;
				case 2:
					return DustID.Firework_Yellow;
				default:
					return DustID.YellowTorch;
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
		}


		public override void Kill(int timeLeft)
        {
			SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);

			for (int i = 0; i < 20; i++)
			{
				Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

				Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 10, GetDustType(), speed * 10f, Scale: 1f);
				d.fadeIn = 1f;
				d.noGravity = true;
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			VisualHelpers.TrailFX2(Projectile, new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f), 150f);
			return false;
		}

		public override void PostDraw(Color lightColor)
		{
			for (int i = 0; i < 2; i++)
            {
				float size = ((float)i + 1);
				size = MathHelper.Clamp(size, 1f, 1.75f);

				float rotation;
                switch (i)
                {
					case 1:
						rotation = star2rot;
						break;
					default:
						rotation = star1rot;
						break;
                }

				VisualHelpers.VanillaSparkleFX(Projectile.Opacity, SpriteEffects.None, Projectile.Center - Main.screenPosition,
				new Color(255, 255, 255, 0) * 0.5f, new Color(255, 220, 80),
				1f, 0f, 0.5f, 1.01f, 1.01f,
				rotation, Vector2.One * size, Vector2.One);
			}
		}
	}
}