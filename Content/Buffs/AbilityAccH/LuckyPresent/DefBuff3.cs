using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent
{
	public class DefBuff3 : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.statDefense += 25;
		}
    }
}