using ArtificerMod.Common;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class StealthShroudBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			if (player.GetModPlayer<ArtificerPlayer>().stealthShroudFlag == true)
			{
				player.aggro -= 900;

				if(player.immuneAlpha < 120)
                {
					player.immuneAlpha = 120;
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