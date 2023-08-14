using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class BorealisBarrier : ModBuff
	{
		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 5;
			player.lifeRegen += 6;
			player.aggro -= 250;
			player.endurance += 0.05f;

			if (Main.rand.NextBool(2))
			{
				List<Color> colors = new List<Color>();
				colors.Add(new Color(220f, 0f, 220f, 255f));
				colors.Add(new Color(85f, 0f, 255f, 255f));
				colors.Add(new Color(0f, 85f, 255f, 255f));
				colors.Add(new Color(0f, 135f, 135f, 255f));
				colors.Add(new Color(0f, 145f, 0f, 255f));

				int dustIndex = Dust.NewDust(new Vector2(player.position.X, player.position.Y + player.height / 4f), player.width, player.height / 2, DustID.RainbowMk2, 0f, 0f, 0,
					MultiLerpColors(Main.rand.NextFloat(), colors), 1f);
				Main.dust[dustIndex].velocity = new Vector2(player.velocity.X, player.velocity.Y + Main.rand.NextFloat(-2f, 2f));
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].fadeIn = Main.rand.NextFloat() * 1.5f;
			}
		}

		// Adapted from vanilla utility method 'MultiLerp'
		private Color MultiLerpColors(float percent, List<Color> colors)
		{
			float num = 1f / ((float)colors.Count - 1f);
			float num2 = num;
			int num3 = 0;
			while (percent / num2 > 1f && num3 < colors.Count - 2)
			{
				num2 += num;
				num3++;
			}
			return Color.Lerp(colors[num3], colors[num3 + 1], (percent - num * (float)num3) / num);
		}
	}
}