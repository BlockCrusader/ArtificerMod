using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using System;

namespace ArtificerMod.Common
{
	// All of the methods contained in this Helper class are adapted drawing methods from the vanilla sourcecode!
	public class VisualHelpers : ModSystem
    {

		/// <summary>
		/// Draws a trail effect identical to those used by the Kwad Racer Drone.
		/// </summary>
		public static void TrailFX1(Projectile proj, Vector2 rotatableOffsetFromCenter, Color trailColor, float trailWidth = 4f)
		{
			Vector2 projScalar = proj.Size / 2f;
			Texture2D texture = TextureAssets.MagicPixel.Value;
			Vector2[] oldPos = proj.oldPos;
			float[] oldRot = proj.oldRot;
			int trailLength = oldPos.Length;
			for (int i = trailLength - 1; i > 0; i--)
			{
				if (!(oldPos[i] == Vector2.Zero))
				{
					Vector2 drawPos = oldPos[i] + projScalar + rotatableOffsetFromCenter.RotatedBy(oldRot[i]);
					Vector2 trail = oldPos[i - 1] + projScalar + rotatableOffsetFromCenter.RotatedBy(oldRot[i - 1]) - drawPos;
					float lengthScalar = trail.Length();
					float rotation = trail.ToRotation();
					float shadingScalar = Utils.Remap(i, 0f, trailLength, 1f, 0f);
					Main.EntitySpriteDraw(texture, drawPos - Main.screenPosition, new Rectangle(0, 0, 1, 1), trailColor * shadingScalar, rotation + (float)Math.PI / 2f, new Vector2(0.5f, 0.5f), new Vector2(trailWidth, lengthScalar), SpriteEffects.None);
				}
			}
		}

		/// <summary>
		/// Draws a trail effect identical to those used by the Laser Machinegun.
		/// </summary>
		public static void TrailFX2(Projectile proj, Color projectileColor, float trailLength)
        {
			Rectangle nearScreenHitbox = new Rectangle((int)Main.screenPosition.X - 1000, (int)Main.screenPosition.Y - 1000, Main.screenWidth + 2000, Main.screenHeight + 2000);
			if (proj.getRect().Intersects(nearScreenHitbox)) // Only draws while the projetile is close to the screen
			{
				float xOffset = (float)(TextureAssets.Projectile[proj.type].Width() - proj.width) * 0.5f + (float)proj.width * 0.5f;
				Vector2 drawPos = new Vector2(proj.position.X - Main.screenPosition.X + xOffset, proj.position.Y - Main.screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY);

				for (int i = 1; i <= trailLength; i++)
				{
					Vector2 drawOffset = Vector2.Normalize(proj.velocity) * i * 3f;
					Color drawColor = proj.GetAlpha(projectileColor);
					drawColor *= (trailLength - (float)i) / trailLength;
					drawColor.A = 0;
					Main.EntitySpriteDraw(TextureAssets.Projectile[proj.type].Value, drawPos - drawOffset, null, drawColor, proj.rotation, new Vector2(xOffset, proj.height / 2), proj.scale, SpriteEffects.None);
				}
			}
		}

		/// <summary>
		/// Draws an effect identical to those used by Fallen Stars.
		/// </summary>
		public static void VanillaStarFX(Projectile proj, Color innerColor, float innerMult, Color outerColor, float outerMult, byte colorAlpha = 0, float extraScale = 0f)
        {
			Texture2D texture = TextureAssets.Extra[91].Value;
			Rectangle drawRect = texture.Frame();
			Vector2 drawOrigin = new Vector2((float)drawRect.Width / 2f, 10f);
			Vector2 drawOffset = new Vector2(0f, proj.gfxOffY);
			Vector2 spinningpoint = new Vector2(0f, -10f);
			float timer = (float)Main.timeForVisualEffects / 60f;
			Vector2 drawPos = proj.Center + proj.velocity;
			Color outsideColor = outerColor * outerMult; // Default: Blue * 0.2f
			Color insideColor = innerColor * innerMult; // Default: White * 0.5f
			insideColor.A = colorAlpha;

			Color color3 = outsideColor;
			color3.A = 0;

			Main.EntitySpriteDraw(texture, drawPos - Main.screenPosition + drawOffset + spinningpoint.RotatedBy((float)Math.PI * 2f * timer), drawRect, color3, proj.velocity.ToRotation() + (float)Math.PI / 2f, drawOrigin, 1.5f + extraScale, SpriteEffects.None);
			Main.EntitySpriteDraw(texture, drawPos - Main.screenPosition + drawOffset + spinningpoint.RotatedBy((float)Math.PI * 2f * timer + (float)Math.PI * 2f / 3f), drawRect, color3, proj.velocity.ToRotation() + (float)Math.PI / 2f, drawOrigin, 1.1f + extraScale, SpriteEffects.None);
			Main.EntitySpriteDraw(texture, drawPos - Main.screenPosition + drawOffset + spinningpoint.RotatedBy((float)Math.PI * 2f * timer + 4.18879032f), drawRect, color3, proj.velocity.ToRotation() + (float)Math.PI / 2f, drawOrigin, 1.3f + extraScale, SpriteEffects.None);
			Vector2 drawPos2 = proj.Center - proj.velocity * 0.5f;
			for (float i = 0f; i < 1f; i += 0.5f)
			{
				float timer2 = timer % 0.5f / 0.5f;
				timer2 = (timer2 + i) % 1f;
				float timer3 = timer2 * 2f;
				if (timer3 > 1f)
				{
					timer3 = 2f - timer3;
				}
				Main.EntitySpriteDraw(texture, drawPos2 - Main.screenPosition + drawOffset, drawRect, insideColor * timer3, proj.velocity.ToRotation() + (float)Math.PI / 2f, drawOrigin, 0.3f + timer2 * 0.5f, SpriteEffects.None);
			}
		}

		/// <summary>
		/// Draws a trail effect identical to those used by various 1.4+ visual FX (Ex: 1.4.4 sword FX).
		/// </summary>
		public static void VanillaSparkleFX(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
		{
			Texture2D texture = TextureAssets.Extra[98].Value; 
			Color color = shineColor * opacity * 0.5f;
			color.A = 0; 
			Vector2 origin = texture.Size() / 2f; 
			Color color2 = drawColor * 0.5f; 
			float lerpScalar = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
			Vector2 sizeScalar = new Vector2(fatness.X * 0.5f, scale.X) * lerpScalar;
			Vector2 sizeScalar2 = new Vector2(fatness.Y * 0.5f, scale.Y) * lerpScalar;
			color *= lerpScalar;
			color2 *= lerpScalar;
			Main.EntitySpriteDraw(texture, drawpos, null, color, (float)Math.PI / 2f + rotation, origin, sizeScalar, dir);
			Main.EntitySpriteDraw(texture, drawpos, null, color, 0f + rotation, origin, sizeScalar2, dir);
			Main.EntitySpriteDraw(texture, drawpos, null, color2, (float)Math.PI / 2f + rotation, origin, sizeScalar * 0.6f, dir);
			Main.EntitySpriteDraw(texture, drawpos, null, color2, 0f + rotation, origin, sizeScalar2 * 0.6f, dir);
		}
	}
}
