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

        public override void Update(NPC npc, ref int buffIndex)
        {
			npc.defense -= 20;
        }
    }
}