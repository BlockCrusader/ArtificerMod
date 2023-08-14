using ArtificerMod.Common;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class ArtificerArmorBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			if (player.GetModPlayer<ArtificerPlayer>().artificerArmorFlag == true)
			{
				player.statDefense += 6;
				player.endurance = 1f - ((1f - .5f) * (1f - player.endurance));
				player.GetModPlayer<ArtificerPlayer>().artificerArmorActive = true;
			}
			// Remove the buff if the player doesn't have Artificer's Armor
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
    }
}