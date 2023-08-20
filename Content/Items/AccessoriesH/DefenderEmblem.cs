using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class DefenderEmblem : ModItem
	{
		public static int IncreasedDmg = 5;
		public static int IncreasedSummonDamage = 10;
		public static int MaxSentry = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedSummonDamage, MaxSentry);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightPurple;
			Item.value = Item.buyPrice(0, 35, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += 0.05f;
			player.GetDamage(DamageClass.Summon) += 0.1f;
			player.maxTurrets++;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ItemID.HuntressBuckler || equippedItem.type == ItemID.MonkBelt || equippedItem.type == ItemID.SquireShield || equippedItem.type == ItemID.ApprenticeScarf)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.AvengerEmblem)
				.AddIngredient(ItemID.ApprenticeScarf)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.AvengerEmblem)
				.AddIngredient(ItemID.HuntressBuckler)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.AvengerEmblem)
				.AddIngredient(ItemID.MonkBelt)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.AvengerEmblem)
				.AddIngredient(ItemID.SquireShield)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
