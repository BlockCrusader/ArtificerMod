using ArtificerMod.Common;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.ArmorH
{
	public class PrismMagic : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Magic) += 0.04f;
			if (!player.TryGetModPlayer(out ArtificerPlayer artificer))
			{
				return;
			}
			if (artificer.radiantArmorFlag)
			{
				player.GetCritChance(DamageClass.Magic) += 4f;
			}
		}
	}
}