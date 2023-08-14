using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{

	public class StarwriterSavePos : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.hostile = false;
			Projectile.friendly = false;
			Projectile.penetrate = -1;
			Projectile.alpha = 0;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 60;
			DrawOffsetX = -13;
			DrawOriginOffsetY = -12;
		}

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			Projectile.rotation += 0.05f * (float)Projectile.direction; 

			Projectile.velocity *= 0f;

            if (owner.dead == false && owner.active && owner.GetModPlayer<ArtificerPlayer>().starwriter)
            {
				Projectile.timeLeft = 5;
            }
            else
            {
				Projectile.Kill();
            }

			Lighting.AddLight(Projectile.Center, .65f, .6f, .15f);

			if (Main.rand.NextBool(20))
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

				Vector2 speed = Main.rand.NextVector2Circular(2f, 2f);
				Dust d = Dust.NewDustPerfect(Projectile.Center, dustID, speed, Scale: 1.25f);
				d.noGravity = true;
			}
		}

        public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f);
		}

        public override void Kill(int timeLeft)
        {
			SoundEngine.PlaySound(SoundID.NPCDeath7, Projectile.Center);
			
			Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), Math.PI);
			Vector2 dustOffset = new Vector2(1.05f, 1f);
			for (float i = 0f; i < 24f; i++)
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

				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, dustID, 0f, 0f, 0, Color.Transparent);
				Main.dust[dustIndex].position = Projectile.Center;

				double dustRot = (float)Math.PI * 2f * i / 24f;
				Main.dust[dustIndex].velocity = spinningpoint.RotatedBy(dustRot) * dustOffset * (0.8f + Main.rand.NextFloat() * 0.4f) * 2f;
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].scale += 0.5f + Main.rand.NextFloat();
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			return false;
		}

		public override void PostDraw(Color lightColor)
		{
			VisualHelpers.VanillaSparkleFX(Projectile.Opacity, SpriteEffects.None, Projectile.Center - Main.screenPosition,
				new Color(255, 255, 255, 0) * 0.5f, new Color(255, 220, 80),
				1f, 0f, 0.5f, 1.01f, 1.01f,
				Projectile.rotation, Vector2.One, Vector2.One * 1.5f);
		}
	}
}