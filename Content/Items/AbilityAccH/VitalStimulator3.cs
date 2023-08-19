using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class VitalStimulator3 : ModItem
	{
        public static int HealthRestore3 = 4 * 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(HealthRestore3);
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
			player.GetModPlayer<ArtificerPlayer>().hpStimType = 3;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<VitalStimulator2>()
				.AddIngredient(ItemID.GreaterHealingPotion, 2)
				.AddIngredient(ItemID.CrystalShard, 15)
				.AddIngredient(ItemID.PixieDust, 20)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
