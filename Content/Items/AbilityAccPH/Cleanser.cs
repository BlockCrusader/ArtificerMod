using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	[AutoloadEquip(EquipType.Back)]
	public class Cleanser : ModItem
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
			Item.rare = ItemRarityID.Green;
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().debuffRemovalAcc = 3;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Curer>()
				.AddIngredient<Extinguisher>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
