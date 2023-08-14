using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	[AutoloadEquip(EquipType.Shoes)]
	public class OverdriveLeg : ModItem
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
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(0, 15, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().overdriveLeg = true;
			player.accRunSpeed = 6f;
			player.jumpSpeedBoost += 2f;
			player.autoJump = true;
			player.extraFall += 20;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<AugmentedLeg>()
				.AddIngredient<TurboBoots>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
