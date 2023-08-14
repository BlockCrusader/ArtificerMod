using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.ArmorPH
{
	public class StarplateDay : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetDamage(DamageClass.Generic) += 0.02f;
			player.moveSpeed += 0.02f;
			player.runAcceleration *= 1.02f;
			player.maxRunSpeed *= 1.02f;
			player.accRunSpeed *= 1.02f;
			player.runSlowdown *= 1.02f;
		}
    }
}