using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	public class DireGlaze : ModItem
	{
		public static int IncreasedCritChance = 2;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedCritChance);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 0, 75, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetCritChance(DamageClass.Generic) += 2f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.BlackLens)
				.AddIngredient(ItemID.Lens, 2)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
