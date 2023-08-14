using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	public class SpaceCore : ModItem
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
			Item.value = Item.buyPrice(0, 15, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetCritChance(DamageClass.Magic) += 3f;
			player.GetDamage(DamageClass.Magic) += .03f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MeteoriteBar, 20)
				.AddIngredient(ItemID.ManaCrystal)
				.AddIngredient(ItemID.FallenStar, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
