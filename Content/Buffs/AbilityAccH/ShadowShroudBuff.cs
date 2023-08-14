using ArtificerMod.Common;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class ShadowShroudBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			if (player.GetModPlayer<ArtificerPlayer>().shadowShroudFlag == true)
			{
				player.aggro -= 1000;

				if (player.immuneAlpha < 160)
				{
					player.immuneAlpha = 160;
				}
				player.GetModPlayer<ArtificerPlayer>().stealthActive = true;
			}

			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
    }
}