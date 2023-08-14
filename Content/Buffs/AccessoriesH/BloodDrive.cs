using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AccessoriesH
{
	public class BloodDrive : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
			player.ClearBuff(BuffID.Panic); // Incompatible with 'Panic!' from the other Panic Necklace accessories to avoid stacking the speed boost
			player.moveSpeed += 1f;
		}
    }
}