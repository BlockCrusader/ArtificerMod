using ArtificerMod.Content.Buffs.AbilityAccH;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class AuroraBorealis : ModProjectile
	{
		float colorTimer = 0f;
		int timerDir = 0;

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.HallowBossDeathAurora);
			Projectile.scale = 0.5f;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.timeLeft = 240;
		}

        public override void AI()
        {
			AuraDebuff(225f);
			AuraBuff(225f);

			if(timerDir == 0)
            {
				colorTimer = Main.rand.NextFloat();
				colorTimer = Math.Clamp(colorTimer, 0f, 1f);
				timerDir = colorTimer >= 0.5f ? -1 : 1;
			}

            switch (timerDir)
            {
				case 1:
					if (colorTimer < 1f)
					{
						colorTimer += 0.01f;
					}
					if (colorTimer >= 1f)
					{
						colorTimer = 1f;
						timerDir = -1;
					}
					break;
				case -1:
					if (colorTimer > 0f)
					{
						colorTimer -= 0.01f;
					}
					if (colorTimer <= 0f)
					{
						colorTimer = 0f;
						timerDir = 1;
					}
					break;
				default:
					colorTimer = Math.Clamp(colorTimer, 0f, 1f);
					timerDir = colorTimer >= 0.5f ? -1 : 1;
					break;
            }
			colorTimer = Math.Clamp(colorTimer, 0f, 1f);
		}

		public void AuraDebuff(float maxDetectDistance)
		{
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];
				if (target.CanBeChasedBy())
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center + new Vector2(0f, 50f));
					if (sqrDistanceToTarget < sqrMaxDetectDistance && Main.myPlayer == Projectile.owner)
					{
						target.AddBuff(ModContent.BuffType<BorealisBurn>(), 300);
						target.AddBuff(BuffID.Confused, 300);
					}
				}
			}
		}

		public void AuraBuff(float maxDetectDistance)
		{
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			for (int k = 0; k < Main.maxPlayers; k++)
			{
				float sqrDistanceToTarget = Vector2.DistanceSquared(Main.player[k].Center, Projectile.Center + new Vector2(0f, 50f));
				if (sqrDistanceToTarget < sqrMaxDetectDistance)
				{
					if (Main.player[k].active && !Main.player[k].dead
						&& ((!Main.player[Projectile.owner].hostile && !Main.player[k].hostile) || Main.player[Projectile.owner].team == Main.player[k].team))
					{
						Main.player[k].AddBuff(ModContent.BuffType<BorealisBarrier>(), 300);
					}
				}
			}
		}

		public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
			// Adapted from the vanilla 'Death Aruroa' projectile's drawing

			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 origin = texture.Size() / 2f;
			float timer = Main.GlobalTimeWrappedHourly % 10f / 10f;
			Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, 50f);
			float[] array = new float[15];
			float[] array2 = new float[15];
			float[] array3 = new float[15];
			int projLifetime = 240;
			float lerp1 = Utils.GetLerpValue(0f, 60f, Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(projLifetime, projLifetime - 60, Projectile.timeLeft, clamped: true);
			float lerp2 = Utils.GetLerpValue(0f, 60f, Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(projLifetime, 90f, Projectile.timeLeft, clamped: true);
			lerp2 = MathHelper.Lerp(0.2f, 0.5f, lerp2);
			float drawScalar = 800f / (float)texture.Width;
			float drawScalarBase = drawScalar * 0.8f;
			float drawScalarMult = (drawScalar - drawScalarBase) / 15f;
			float orbitRadiusY = 30f;
			float orbitRadiusX = 200f;
			Vector2 sizeScalar = new Vector2(1.5f, 3f);
			for (int i = 0; i < 15; i++)
			{
				_ = (float)(i + 1) / 50f;
				float num8 = (float)Math.Sin((double)(timer * ((float)Math.PI * 2f) + (float)Math.PI / 2f + (float)i / 2f));
				array[i] = num8 * (orbitRadiusX - (float)i * 3f);
				array2[i] = (float)Math.Sin((double)(timer * ((float)Math.PI * 2f) * 2f + (float)Math.PI / 3f + (float)i)) * orbitRadiusY;
				array2[i] -= (float)i * 3f;
				array3[i] = drawScalarBase + (float)(i + 1) * drawScalarMult;
				array3[i] *= 0.3f;

				float offsetTimer = OffsetTimer(i, colorTimer, timerDir);
				List<Color> colors = new List<Color>();
				colors.Add(new Color(220f, 0f, 220f, 255f));
				colors.Add(new Color(85f, 0f, 255f, 255f));
				colors.Add(new Color(0f, 85f, 255f, 255f));
				colors.Add(new Color(0f, 135f, 135f, 255f));
				colors.Add(new Color(0f, 145f, 0f, 255f));
				Color color = MultiLerpColors(offsetTimer, colors) * lerp1 * lerp2;
				color.A /= 4;
				float rotation = (float)Math.PI / 2f + num8 * ((float)Math.PI / 4f) * -0.3f + (float)Math.PI * (float)i;
				Main.EntitySpriteDraw(texture, drawPos + new Vector2(array[i], array2[i]), null, color, rotation, origin, new Vector2(array3[i], array3[i]) * sizeScalar, SpriteEffects.None);
			}
		}

		private Color MultiLerpColors(float percent, List<Color> colors)
		{
			float num = 1f / ((float)colors.Count - 1f);
			float num2 = num;
			int num3 = 0;
				while (percent / num2 > 1f && num3< colors.Count - 2)
				{
					num2 += num;
					num3++;
				}
			return Color.Lerp(colors[num3], colors[num3 + 1], (percent - num* (float) num3) / num);
		}

		private float OffsetTimer(int offsetCount, float startingAmount, int timerDirection)
        {
			float timerWithOffset = startingAmount;
			int oTimerDir = timerDirection;
			for (int i = 0; i < offsetCount; i++)
            {
				switch (oTimerDir)
				{
					case 1:
						if (timerWithOffset < 1f)
						{
							timerWithOffset += 0.06f;
						}
						if (timerWithOffset >= 1f)
						{
							timerWithOffset = 1f;
							oTimerDir = -1;
						}
						break;
					case -1:
						if (timerWithOffset > 0f)
						{
							timerWithOffset -= 0.06f;
						}
						if (timerWithOffset <= 0f)
						{
							timerWithOffset = 0f;
							oTimerDir = 1;
						}
						break;
					default:
						timerWithOffset = Math.Clamp(timerWithOffset, 0f, 1f);
						oTimerDir = timerWithOffset >= 0.5f ? -1 : 1;
						break;
				}
				timerWithOffset = Math.Clamp(timerWithOffset, 0f, 1f);
			}
			return timerWithOffset;
        }
    }
}