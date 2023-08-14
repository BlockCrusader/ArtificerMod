using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(new EquipType[]
	{
		EquipType.HandsOn,
		EquipType.HandsOff,
		EquipType.Face,
		EquipType.Shoes
	})]
	public class TerradepthGear : ModItem
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
			Item.rare = ItemRarityID.Lime;
			Item.defense = 3;
			Item.value = Item.buyPrice(0, 75, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.accFlipper = true;
			player.accDivingHelm = true;
			player.GetModPlayer<ArtificerPlayer>().ultrabrightDiving = true;
			player.nightVision = true;
			player.arcticDivingGear = true;
			player.iceSkate = true;
			player.spikedBoots += 2;
			player.jumpSpeedBoost += 1.75f;
			player.extraFall += 15;
			player.autoJump = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<HadopelagicDivingGear>()
				.AddIngredient(ItemID.FrogGear)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
