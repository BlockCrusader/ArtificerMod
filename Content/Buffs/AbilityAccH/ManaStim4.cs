using ArtificerMod.Common;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class ManaStim4 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetModPlayer<ArtificerPlayer>().manaStim = 4;
		}
    }
}