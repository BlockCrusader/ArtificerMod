using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	public class VitalStimulator2 : ModItem
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
			Item.rare = ItemRarityID.Green;
			Item.value = Item.buyPrice(0, 9, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().hpStimType = 2;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<VitalStimulator>()
				.AddIngredient(ItemID.HealingPotion, 2)
				.AddIngredient(ItemID.PinkGel, 10)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
