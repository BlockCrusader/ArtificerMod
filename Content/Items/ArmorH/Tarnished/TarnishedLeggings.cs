using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH.Tarnished
{
	[AutoloadEquip(EquipType.Legs)]
	public class TarnishedLeggings : ModItem
	{
		public static int IncreasedDmg = 4;
		public static int IncreasedCritChance = 2;
		public static int IncreasedMovementSpeed = 5;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedCritChance, IncreasedMovementSpeed);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 7, 50, 0); 
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.04f;
			player.GetCritChance(DamageClass.Generic) += 2f;
			player.moveSpeed += 0.05f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<PossessedLeggings>()
				.AddIngredient(ItemID.SoulofNight, 8)
				.AddIngredient(ItemID.PurificationPowder, 20)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
