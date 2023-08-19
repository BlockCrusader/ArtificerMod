using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ArtificerMod.Content.Items.Others;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class VortexEmblem : ModItem
	{
		public static int IncreasedRangedDamage = 16;
		public static int IncreasedRangedCritChance = 5;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedRangedDamage, IncreasedRangedCritChance);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.buyPrice(0, 50, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Ranged) += 0.16f; 
			player.GetCritChance(DamageClass.Ranged) += 5f;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.RangerEmblem)
				.AddIngredient<NovaFragment>(4)
				.AddIngredient(ItemID.FragmentVortex, 8)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
