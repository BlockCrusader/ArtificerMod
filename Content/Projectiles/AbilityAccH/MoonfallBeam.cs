using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	public class MoonfallBeam : ModProjectile
	{
		private const float MAX_CHARGE = 60f;
		// The distance charge particle from the player center
		private const float MOVE_DISTANCE = 45f;

		public float Distance 
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public float Charge 
		{
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public bool IsAtMaxCharge => Charge == MAX_CHARGE;

		public override void SetDefaults() {
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 7;
			Projectile.timeLeft = 960;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if (IsAtMaxCharge) 
			{
				DrawLaser(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, Projectile.Center + new Vector2(0f, -1000f),
				Projectile.velocity, 60f, Projectile.damage, -1.57f, new Vector2(1.5f, 1.5f), 3000f, new Color(255, 255, 255, 0) * 0.9f, (int)MOVE_DISTANCE);
			}
			return false;
		}

		// The core function of drawing a laser
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture2, Vector2 start, Vector2 unit, float step, int damage, float rotation, Vector2 scale, float maxDist, Color color, int transDist)
		{
			float r = unit.ToRotation() + rotation;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step)
			{
				Color c = color;
				var origin = start + i * unit;
				Main.EntitySpriteDraw(texture2, origin - Main.screenPosition,
					new Rectangle(0, 48, 72, 42), i < transDist ? Color.Transparent : c, r,
					new Vector2(72 * .5f, 42 * .5f), 1.5f, 0, 0);
			}

			// Draws the laser 'tail'
			Main.EntitySpriteDraw(texture2, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 2, 72, 42), color, r, new Vector2(72 * .5f, 42 * .5f), 1.5f, 0, 0);

			// Draws the laser 'head'
			Main.EntitySpriteDraw(texture2, start + (Distance + 10 + (step / 2)) * unit - Main.screenPosition,
				new Rectangle(0, 90, 72, 42), color, r, new Vector2(72 * .5f, 42 * .5f), 1.5f, 0, 0);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (!IsAtMaxCharge) return false;

			Vector2 unit = Projectile.velocity;
			float point = 0f;

			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center + new Vector2(0f, -1000f),
				Projectile.Center + new Vector2(0f, -1000f) + unit * Distance, 99, ref point);
		}

		bool beamBurst = false;
		float portalRot;
		public override void AI() {
			Player player = Main.player[Projectile.owner];

			portalRot += 0.2f;
			ChargeLaser(player);
			Projectile.position = UpdatePos(Projectile.position);

			if (Charge < MAX_CHARGE) return;

			SetLaserPosition(player);
			CastLights();
			if (beamBurst == false)
			{
				SoundEngine.PlaySound(SoundID.Zombie104, player.position);
				beamBurst = true;
			}
			
		}

		private void SetLaserPosition(Player player) {
			for (Distance = MOVE_DISTANCE; Distance <= 3000f; Distance += 5f) 
			{
				var start = Projectile.Center + Projectile.velocity * Distance;
			}
		}

		private void ChargeLaser(Player player)
		{
			if (player.dead || !player.active)
			{
				Projectile.Kill();
			}
			else
			{
				if (Charge < MAX_CHARGE)
				{
					Charge++;
				}
			}
		}


		private Vector2 UpdatePos(Vector2 currentPos)
        {
			if (Projectile.owner == Main.myPlayer)
			{
				Vector2 targetPos = Main.MouseWorld;

				if ((targetPos - Projectile.Center).Length() > 5f)
				{
					float moveSpeed = (targetPos - Projectile.Center).Length() / 400f;
					moveSpeed = Math.Clamp(moveSpeed, 1f, 3.5f);

					Vector2 displacement = (targetPos - Projectile.Center).SafeNormalize(Vector2.Zero) * moveSpeed;

					currentPos += displacement; 
					currentPos.Y = targetPos.Y; 
				}
			}
			return currentPos;
        }

		private void CastLights() {
			DelegateMethods.v3_1 = new Vector3(0f, 0.8f, 0.6f);
			Utils.PlotTileLine(Projectile.Center + new Vector2(0f, -1000f), Projectile.Center + new Vector2(0f, -1000f) + Projectile.velocity * (Distance - MOVE_DISTANCE), 74, DelegateMethods.CastLight);
		}

        public override void PostDraw(Color lightColor)
        {
			// Draw code adapted from Lunar Portal sentry
			Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, -1000f);
			Texture2D texture = TextureAssets.Projectile[ProjectileID.MoonlordTurret].Value;
			Color color1 = new Color(255, 255, 255, 255);
			Vector2 drawOrigin1 = new Vector2(texture.Width, texture.Height) / 2f;
			float rotMod = portalRot;

			Color color2 = color1 * 0.8f;
			color2.A /= 2;
			Color color3 = Color.Lerp(color1, Color.Black, 0.5f);
			color3.A = color1.A;
			float fxScale = 0.95f + (portalRot * 0.75f).ToRotationVector2().Y * 0.1f;
			color3 *= fxScale;
			float drawScale = 0.6f + Projectile.scale * 1.8f * fxScale;
			Texture2D texture2 = TextureAssets.Extra[50].Value;
			Vector2 drawOrigin2 = texture2.Size() / 2f;

			SpriteEffects dir = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				dir = SpriteEffects.FlipHorizontally;
			}

			Main.EntitySpriteDraw(texture2, drawPos, null, color3, 0f - rotMod + 0.35f, drawOrigin2, drawScale, dir ^ SpriteEffects.FlipHorizontally);
			Main.EntitySpriteDraw(texture2, drawPos, null, color1, 0f - rotMod, drawOrigin2, Projectile.scale * 3f, dir ^ SpriteEffects.FlipHorizontally);
			Main.EntitySpriteDraw(texture, drawPos, null, color2, (0f - rotMod) * 0.7f, drawOrigin1, Projectile.scale * 3f, dir ^ SpriteEffects.FlipHorizontally);
			Main.EntitySpriteDraw(texture2, drawPos, null, color1 * 0.8f, rotMod * 0.5f, drawOrigin2, Projectile.scale * 2.7f, dir);
			color1.A = 0;
		}

        public override bool ShouldUpdatePosition() => false;
	}
}