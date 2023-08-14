using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Neck)]
	public class BloodNecklace : ModItem
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
			Item.value = Item.buyPrice(0, 45, 0, 0);
			Item.defense = 8;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.aggro += 400;
			player.GetModPlayer<ArtificerPlayer>().bloodNecklace = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PanicNecklace)
				.AddIngredient(ItemID.FleshKnuckles)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
