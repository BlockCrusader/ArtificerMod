using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	public class RepulseCharge : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 30;
			Item.knockBack = 20f;
			Item.DamageType = DamageClass.Generic;
			Item.accessory = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().repulseCharge = true;
			player.GetModPlayer<ArtificerPlayer>().rChargeEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Dynamite, 5)
				.AddRecipeGroup("ArtificerMod:MetalBars1", 12)
				.AddRecipeGroup("ArtificerMod:MetalBars2", 8)
				.AddRecipeGroup("Sand", 50)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
