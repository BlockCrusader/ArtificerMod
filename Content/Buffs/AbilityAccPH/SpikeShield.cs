using ArtificerMod.Common;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class SpikeShield : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			if (player.GetModPlayer<ArtificerPlayer>().spikeManacleFlag == true)
			{
				player.statDefense += 4;
				if (player.thorns < 2f)
				{
					player.thorns = 2f;
				}
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
    }
}