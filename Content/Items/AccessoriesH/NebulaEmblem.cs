using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ArtificerMod.Content.Items.Others;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class NebulaEmblem : ModItem
	{
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
			player.GetDamage(DamageClass.Magic) += 0.19f;
			player.manaCost -= 0.12f;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SorcererEmblem)
				.AddIngredient<NovaFragment>(4)
				.AddIngredient(ItemID.FragmentNebula, 8)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
