using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorPH
{
	[AutoloadEquip(EquipType.Legs)]
	public class OrnateLeggings : ModItem
	{
		public static int IncreasedDmg = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16;
			Item.value = Item.buyPrice(0, 3, 0, 0); 
			Item.rare = ItemRarityID.Blue;
			Item.defense = 3; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.01f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("ArtificerMod:MetalBars3", 10)
				.AddRecipeGroup("ArtificerMod:MetalBars4", 8)
				.AddIngredient(ItemID.Emerald, 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
