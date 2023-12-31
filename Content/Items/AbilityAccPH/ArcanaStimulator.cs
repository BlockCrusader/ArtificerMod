using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	public class ArcanaStimulator : ModItem
	{
		public static int ManaRestore1 = 1 * 30;
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ManaRestore1);
		
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
			Item.value = Item.buyPrice(0, 6, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{			
			player.GetModPlayer<ArtificerPlayer>().manaStimType = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LesserManaPotion, 2)
				.AddIngredient(ItemID.BottledWater)
				.AddIngredient(ItemID.ManaCrystal)
				.AddRecipeGroup("ArtificerMod:MetalBars2", 5)
				.AddTile(TileID.HeavyWorkBench)
				.Register();
		}
	}
}
