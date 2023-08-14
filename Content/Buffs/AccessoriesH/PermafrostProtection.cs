using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AccessoriesH
{
	public class PermafrostProtection : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.statDefense += 5;
			player.lifeRegen += 6;
		}
    }
}