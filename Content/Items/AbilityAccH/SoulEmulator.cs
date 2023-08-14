using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class SoulEmulator : ModItem
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
			Item.rare = ItemRarityID.LightPurple;
			Item.value = Item.buyPrice(0, 25, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().soulEmulator = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddIngredient(ItemID.ChlorophyteBar, 10)
				.AddIngredient(ItemID.SoulofFright, 5)
				.AddIngredient(ItemID.SoulofSight, 5)
				.AddIngredient(ItemID.SoulofMight, 5)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
