using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using Terraria.ID;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class SuperMagnet : ModItem
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
			Item.value = Item.buyPrice(0, 40, 0, 0);
		}

        public override void UpdateInventory(Player player)
        {
			player.GetModPlayer<ArtificerPlayer>().superMagnetWeak = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().superMagnet = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.TreasureMagnet)
				.AddIngredient(ItemID.CelestialMagnet)
				.AddIngredient(ItemID.GoldRing)
				.AddIngredient(ItemID.HeartreachPotion, 3)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
