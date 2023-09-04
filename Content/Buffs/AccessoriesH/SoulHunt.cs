using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AccessoriesH
{
	public class SoulHunt : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = true;
		}
    	}
}
