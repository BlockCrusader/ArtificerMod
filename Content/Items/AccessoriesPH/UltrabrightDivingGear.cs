using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.Face)]
	public class UltrabrightDivingGear : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Face.Sets.PreventHairDraw[Item.faceSlot] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 2;
			Item.value = Item.buyPrice(0, 20, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.accFlipper = true;
			player.accDivingHelm = true;
			player.GetModPlayer<ArtificerPlayer>().ultrabrightDiving = true;
			player.nightVision = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.UltrabrightHelmet)
				.AddIngredient(ItemID.JellyfishDivingGear)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
