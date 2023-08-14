using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	public class XRadar : ModItem
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
			Item.value = Item.buyPrice(0, 4, 50, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().xRadar = true;
		}

        public override void UpdateInfoAccessory(Player player)
        {
			player.accThirdEye = true; // Radar effect
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Radar)
				.AddIngredient(ItemID.ShinePotion, 5)
				.AddIngredient(ItemID.HunterPotion, 5)
				.AddIngredient(ItemID.ManaCrystal)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
