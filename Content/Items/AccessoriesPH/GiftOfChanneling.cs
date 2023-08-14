using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class GiftOfChanneling : ModItem
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
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(0, 12, 50, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.manaCost -= 0.12f;
			player.statManaMax2 += 20;
			player.manaFlower = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.ManaFlower)
				.AddIngredient(ItemID.BandofStarpower)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
