using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	[AutoloadEquip(EquipType.Shoes)]
	public class AugmentedLeg : ModItem
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
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().rocketLeg = true;
			player.jumpSpeedBoost += 2f;
			player.autoJump = true;
			player.extraFall += 20;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FrogLeg)
				.AddIngredient(ItemID.RocketBoots)
				.AddRecipeGroup("ArtificerMod:MetalBars2", 5)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
