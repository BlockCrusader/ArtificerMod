using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	public class DireGlaze : ModItem
	{
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
