using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.ArmorH
{
	public class PrismMelee : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetDamage(DamageClass.Melee) += 0.04f;
			if (!player.TryGetModPlayer(out ArtificerPlayer artificer))
			{
				return;
			}
			if (artificer.radiantArmorFlag)
			{
				player.GetCritChance(DamageClass.Melee) += 4f;
			}
		}
    }
}