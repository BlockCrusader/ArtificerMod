using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent
{
	public class DmgBuff1 : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetDamage(DamageClass.Generic) += .1f;
		}
    }
}