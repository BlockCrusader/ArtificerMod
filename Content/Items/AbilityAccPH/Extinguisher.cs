using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	[AutoloadEquip(EquipType.Back)]
	public class Extinguisher : ModItem
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
			player.GetModPlayer<ArtificerPlayer>().debuffRemovalAcc = 2;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.WaterBucket)
				.AddIngredient(ItemID.BottledWater, 5)
				.AddIngredient(ItemID.Coral, 10)
				.AddRecipeGroup("ArtificerMod:MetalBars3", 10)
				.AddTile(TileID.HeavyWorkBench)
				.Register();
		}
	}
}
