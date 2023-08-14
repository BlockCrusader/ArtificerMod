using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	[AutoloadEquip(EquipType.Shoes)]
	public class TurboBoots : ModItem
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
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().turboBoots = true;
			player.accRunSpeed = 6f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HermesBoots)
				.AddIngredient(ItemID.PanicNecklace)
				.AddIngredient(ItemID.SwiftnessPotion, 3)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.SailfishBoots)
				.AddIngredient(ItemID.PanicNecklace)
				.AddIngredient(ItemID.SwiftnessPotion, 3)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.FlurryBoots)
				.AddIngredient(ItemID.PanicNecklace)
				.AddIngredient(ItemID.SwiftnessPotion, 3)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.SandBoots)
				.AddIngredient(ItemID.PanicNecklace)
				.AddIngredient(ItemID.SwiftnessPotion, 3)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
