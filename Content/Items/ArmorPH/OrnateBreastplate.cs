using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.ArmorPH
{
	[AutoloadEquip(EquipType.Body)]
	public class OrnateBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16;
			Item.value = Item.buyPrice(0, 3, 50, 0); 
			Item.rare = ItemRarityID.Blue;
			Item.defense = 4; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.02f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("ArtificerMod:MetalBars3", 15)
				.AddRecipeGroup("ArtificerMod:MetalBars4", 12)
				.AddIngredient(ItemID.Diamond, 3)
				.AddIngredient(ItemID.Emerald, 2)
				.AddIngredient(ItemID.Ruby, 2)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
