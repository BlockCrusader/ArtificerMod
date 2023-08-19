using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class DarkmoonPact : ModItem
	{
		public static int IncreasedDmg = 15;
		public static int IncreasedCritChance = 10;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedCritChance);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.buyPrice(0, 50, 0, 0);
			Item.expert = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statLifeMax2 = (int)(player.statLifeMax2 * 0.8f); // -20% max HP
			player.endurance = 1f - ((1f + .1f) * (1f - player.endurance)); // -10% DR
			player.GetCritChance(DamageClass.Generic) += 10f;
			player.GetDamage(DamageClass.Generic) += .15f;
			player.lifeRegen += 2;
		}
	}
}
