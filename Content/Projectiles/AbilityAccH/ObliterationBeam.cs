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
	public class ObliterationBeam : ModProjectile
	{
		private const float MAX_CHARGE = 60f;
		// The distance charge particle from the player center
		private const float MOVE_DISTANCE = 30f;

		public float Distance 
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
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
			Projectile.hide = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 5;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if (IsAtMaxCharge) 
			{
				DrawLaser(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, Main.player[Projectile.owner].Center,
				Projectile.velocity, 12, -0.5f * (float)Math.PI, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
			}
			return false;
		}

		// The core function of drawing a laser
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, float maxDist = 1200f, Color color = default, int transDist = 50)
		{
			float r = unit.ToRotation() + rotation;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step)
			{
				Color c = Color.White;
				var origin = start + i * unit;
				Main.EntitySpriteDraw(texture, origin - Main.screenPosition,
					new Rectangle(0, 18, 30, 14), i < transDist ? Color.Transparent : c, r,
					new Vector2(30 * .5f, 14 * .5f), scale, 0, 0);
			}

			// Draws the laser 'tail'
			Main.EntitySpriteDraw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 2, 30, 14), Color.White, r, new Vector2(30 * .5f, 14 * .5f), scale, 0, 0);

			// Draws the laser 'head'
			Main.EntitySpriteDraw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 34, 30, 14), Color.White, r, new Vector2(30 * .5f, 14 * .5f), scale, 0, 0);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (!IsAtMaxCharge) return false;

			Player player = Main.player[Projectile.owner];
			Vector2 unit = Projectile.velocity;
			float point = 0f;

			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
				player.Center + unit * Distance, 22, ref point);
		}

        public override bool? CanDamage()
        {
            if (!IsAtMaxCharge)
            {
				return false;
            }
			return null;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        int beamDuration = 150;
		bool beamBurst = false;
		public override void AI() {
			Player player = Main.player[Projectile.owner];
			Projectile.position = player.Center + Projectile.velocity * MOVE_DISTANCE;
			Projectile.timeLeft = 2;

			UpdatePlayer(player);
			ChargeLaser(player);

			if (Charge < MAX_CHARGE) return;

			SetLaserPosition(player);
			CastLights();
			if (beamBurst == false) // The "beamBurst" effect creates a small dust-explosion visual as the beam appears and the charging dust stops
			{
				SoundEngine.PlaySound(SoundID.Item67, player.position);
				Vector2 dustOffset = Projectile.velocity;
				dustOffset *= MOVE_DISTANCE - 20;
				Vector2 dustPosition = player.Center + dustOffset - new Vector2(10, 10);
				Vector2 dustVelocity = Vector2.UnitX * 18f;
				dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
				Vector2 spawnPos = Projectile.Center + dustVelocity;
				for (int k = 0; k < 40; k++)
				{
					Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - 8 * 2);
					Dust dust = Main.dust[Dust.NewDust(dustPosition, 20, 20, DustID.MartianSaucerSpark, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f)];
					dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - 8 * 2f) / 10f;
					dust.velocity *= 5f;
					dust.noGravity = true;
					dust.scale = Main.rand.Next(15, 30) * 0.05f;
				}
				beamBurst = true;
			}

			beamDuration--;
			if(beamDuration <= 0)
            {
				Projectile.Kill();
            }
		}

		private void SetLaserPosition(Player player) {
			for (Distance = MOVE_DISTANCE; Distance <= 1200f; Distance += 5f) {
				var start = player.Center + Projectile.velocity * Distance;
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
				Vector2 offset = Projectile.velocity;
				offset *= MOVE_DISTANCE - 20;
				Vector2 pos = player.Center + offset - new Vector2(10, 10);
				if (Charge < MAX_CHARGE)
				{
					Charge++;
					int chargeFact = (int)(Charge / 15f);
					Vector2 dustVelocity = Vector2.UnitX * 18f;
					dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
					Vector2 spawnPos = Projectile.Center + dustVelocity;
					for (int k = 0; k < chargeFact + 1; k++)
					{
						Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
						Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, DustID.MartianSaucerSpark, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f)];
						dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
						dust.noGravity = true;
						dust.scale = Main.rand.Next(10, 20) * 0.05f;
					}
				}
			}
		}

		private void UpdatePlayer(Player player) 
		{
			if (Projectile.owner == Main.myPlayer) 
			{
				Projectile.velocity = new Vector2(Projectile.ai[0], 0f);
				Projectile.direction = (int)Projectile.ai[0];
				Projectile.netUpdate = true;
			}

			int dir = Projectile.direction;
			player.ChangeDir(dir);
			player.heldProj = Projectile.whoAmI;

			// Attempts to 'hide' any held item by setting its draw location to the edge of the world
			// Almost certainly not the best way to go about this; should change later if possible
			player.itemLocation = new Vector2(Main.rightWorld, Main.bottomWorld);
			player.TryInterruptingItemUsage();
			player.SetDummyItemTime(2);
		}

		private void CastLights() 
		{
			DelegateMethods.v3_1 = new Vector3(1f, 1f, 0.5f);
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;
	}
}