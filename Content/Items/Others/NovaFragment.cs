using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.Others
{
	public class NovaFragment : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
			ItemID.Sets.ItemIconPulse[Item.type] = true; 
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Cyan;
			Item.maxStack = 9999;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FragmentNebula)
				.AddIngredient(ItemID.FragmentSolar)
				.AddIngredient(ItemID.FragmentStardust)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.FragmentVortex)
				.AddIngredient(ItemID.FragmentSolar)
				.AddIngredient(ItemID.FragmentStardust)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.FragmentNebula)
				.AddIngredient(ItemID.FragmentVortex)
				.AddIngredient(ItemID.FragmentStardust)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.FragmentNebula)
				.AddIngredient(ItemID.FragmentSolar)
				.AddIngredient(ItemID.FragmentVortex)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}