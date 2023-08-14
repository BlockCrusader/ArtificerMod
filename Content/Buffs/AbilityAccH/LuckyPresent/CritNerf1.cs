using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH.LuckyPresent
{
	public class CritNerf1 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetCritChance(DamageClass.Generic) -= 5f;
		}
    }
}