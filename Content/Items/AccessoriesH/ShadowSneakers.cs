using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Shoes)]
	public class ShadowSneakers : ModItem
	{
		public static int IncreasedMovementSpeed = 10;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMovementSpeed);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 20, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// Disable other dashes
			player.dashType = 1;

			player.accRunSpeed = 6.375f;
			player.moveSpeed += 0.1f;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<PhasestrideBoots>())
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HermesBoots)
				.AddIngredient(ItemID.Tabi)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.SailfishBoots)
				.AddIngredient(ItemID.Tabi)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.FlurryBoots)
				.AddIngredient(ItemID.Tabi)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.SandBoots)
				.AddIngredient(ItemID.Tabi)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
