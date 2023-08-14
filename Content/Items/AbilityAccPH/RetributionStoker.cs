using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	public class RetributionStoker : ModItem
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
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 35, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().retributionStoker = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Hellforge)
				.AddIngredient(ItemID.LifeCrystal)
				.AddIngredient(ItemID.MeteoriteBar, 15)
				.AddIngredient(ItemID.HellstoneBar, 15)
				.AddRecipeGroup("ArtificerMod:MetalBarsEvil", 15)
				.AddRecipeGroup("ArtificerMod:DmgPotions", 3)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
