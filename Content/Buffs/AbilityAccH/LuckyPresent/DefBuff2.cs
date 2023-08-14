using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent
{
	public class DefBuff2 : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.statDefense += 15;
		}
    }
}