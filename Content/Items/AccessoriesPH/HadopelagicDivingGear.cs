using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.Face)]
	public class HadopelagicDivingGear : ModItem
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
			Item.rare = ItemRarityID.LightPurple;
			Item.defense = 2;
			Item.value = Item.buyPrice(0, 35, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.accFlipper = true;
			player.accDivingHelm = true;
			player.GetModPlayer<ArtificerPlayer>().ultrabrightDiving = true;
			player.nightVision = true;
			player.arcticDivingGear = true;
			player.iceSkate = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.UltrabrightHelmet)
				.AddIngredient(ItemID.ArcticDivingGear)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient<UltrabrightDivingGear>()
				.AddIngredient(ItemID.IceSkates)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
