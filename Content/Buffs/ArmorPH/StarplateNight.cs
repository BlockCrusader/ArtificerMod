using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.ArmorPH
{
	public class StarplateNight : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.statDefense += 1;
			player.endurance = 1f - ((1f - 0.01f) * (1f - player.endurance));
			player.lifeRegen += 2;
        }
    }
}