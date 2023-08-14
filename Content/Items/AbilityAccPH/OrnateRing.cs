using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class OrnateRing : ModItem
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
			Item.defense = 1;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().ornateRing = true;
			player.GetModPlayer<ArtificerPlayer>().ornateEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("ArtificerMod:MetalBars3", 4)
				.AddRecipeGroup("ArtificerMod:MetalBars4", 2)
				.AddRecipeGroup("ArtificerMod:Gems", 4)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
