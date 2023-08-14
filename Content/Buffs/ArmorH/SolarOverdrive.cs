using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.ArmorH
{
	public class SolarOverdrive : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetDamage(DamageClass.Generic) += 0.12f;
        }
    }
}