using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class SanguineStone : ModItem
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
			Item.rare = ItemRarityID.LightPurple;
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.defense = 8;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.aggro += 400;
			player.pStone = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PhilosophersStone)
				.AddIngredient(ItemID.FleshKnuckles)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
