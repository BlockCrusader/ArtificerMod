using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent
{
	public class CritBuff3 : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetCritChance(DamageClass.Generic) += 30f;
		}
    }
}