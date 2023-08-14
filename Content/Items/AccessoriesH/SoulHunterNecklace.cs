using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Content.Items.AccessoriesPH;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Neck)]
	public class SoulHunterNecklace : ModItem
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
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 30, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().soulHunter = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<MorbidNecklace>()
				.AddIngredient(ItemID.Ectoplasm, 10)
				.AddIngredient(ItemID.SoulofNight)
				.AddIngredient(ItemID.SoulofLight)
				.AddIngredient(ItemID.SoulofFlight)
				.AddIngredient(ItemID.SoulofMight)
				.AddIngredient(ItemID.SoulofSight)
				.AddIngredient(ItemID.SoulofFright)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
