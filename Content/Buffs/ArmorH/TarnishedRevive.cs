using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.ArmorH
{
	public class TarnishedRevive : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
	}
}