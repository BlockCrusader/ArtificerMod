using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent
{
	public class DmgBuff2 : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetDamage(DamageClass.Generic) += .2f;
		}
    }
}