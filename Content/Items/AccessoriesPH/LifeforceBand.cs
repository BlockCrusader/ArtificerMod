using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class LifeforceBand : ModItem
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
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 5, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.lifeRegen += 2;
			player.statLifeMax2 += 20;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.BandofRegeneration)
				.AddIngredient(ItemID.BandofStarpower)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
