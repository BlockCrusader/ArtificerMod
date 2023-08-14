using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.Others;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class Starwriter : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.buyPrice(0, 50, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().starwriter = true;
			player.GetModPlayer<ArtificerPlayer>().starwriterEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<NovaFragment>(18)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
