using ArtificerMod.Common;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class PowerArmorBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			if (player.GetModPlayer<ArtificerPlayer>().powerArmorFlag == true)
			{
				player.statDefense += 16;
				player.endurance = 1f - ((1f - .8f) * (1f - player.endurance));
				player.GetModPlayer<ArtificerPlayer>().powerArmorActive = true;
			}

			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
    }
}