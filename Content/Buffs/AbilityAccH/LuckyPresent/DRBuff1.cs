using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent
{
	public class DRBuff1 : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.endurance += 0.05f;
		}
    }
}