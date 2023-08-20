using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AccessoriesPH;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class CoolingCell : ModItem
	{
		public static int Cooldown = 20;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Cooldown);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16; 
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(0, 15, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[BuffID.Chilled] = true;
			player.buffImmune[BuffID.Frozen] = true;
			player.buffImmune[BuffID.Frostburn] = true;
			player.GetModPlayer<ArtificerPlayer>().coolingBonus = true;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<EnergyCell>() || equippedItem.type == ModContent.ItemType<ArtificerEmblem>() || equippedItem.type == ModContent.ItemType<ClockworkStopwatch>() || equippedItem.type == ModContent.ItemType<NovaEmblem>())
			{
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<EnergyCell>()
				.AddIngredient(ItemID.FrostCore)
				.AddRecipeGroup("ArtificerMod:MetalBarsHM3", 10)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
