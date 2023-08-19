using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class ArcanaStimulator4 : ModItem
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
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.buyPrice(0, 40, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			
			player.GetModPlayer<ArtificerPlayer>().manaStimType = 4;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<ArcanaStimulator3>()
				.AddIngredient(ItemID.SuperManaPotion, 2)
				.AddIngredient(ItemID.SoulofLight, 15)
				.AddIngredient(ItemID.HallowedBar, 15)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
