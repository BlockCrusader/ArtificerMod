using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent
{
	public class CritBuff1 : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetCritChance(DamageClass.Generic) += 10f;
		}
    }
}