using ArtificerMod.Common;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.ArmorH
{
	public class XenoShieldDisplay : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = false;
		}

		int hpDisplay = -1;
		public override void Update(Player player, ref int buffIndex)
		{
			hpDisplay = player.GetModPlayer<ArtificerPlayer>().xenoShieldPower;
		}

		public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
			if(hpDisplay > 0)
            {
				tip = $"Your armor is charged and will block some incoming damage!\nCurrent shield charge: {hpDisplay} hit points";
			}
            else
            {
				tip = "Your armor is charged  and will block some incoming damage!";
			}
        }
    }
}