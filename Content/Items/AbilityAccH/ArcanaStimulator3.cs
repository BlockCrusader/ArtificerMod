using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class ArcanaStimulator3 : ModItem
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
			Item.value = Item.buyPrice(0, 22, 50, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			
			player.GetModPlayer<ArtificerPlayer>().manaStimType = 3;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<ArcanaStimulator2>()
				.AddIngredient(ItemID.GreaterManaPotion, 2)
				.AddIngredient(ItemID.CrystalShard, 15)
				.AddIngredient(ItemID.PixieDust, 20)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
