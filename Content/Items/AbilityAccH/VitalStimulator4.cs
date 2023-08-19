using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class VitalStimulator4 : ModItem
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
			
			player.GetModPlayer<ArtificerPlayer>().hpStimType = 4;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<VitalStimulator3>()
				.AddIngredient(ItemID.SuperHealingPotion, 2)
				.AddIngredient(ItemID.ChlorophyteBar, 15)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
