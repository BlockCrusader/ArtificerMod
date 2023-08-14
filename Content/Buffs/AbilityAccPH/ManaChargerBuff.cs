using ArtificerMod.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class ManaChargerBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetModPlayer<ArtificerPlayer>().manaChargerActive = true;
		}
    }
}