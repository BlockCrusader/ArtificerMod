using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class SolarBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 200;
			Item.knockBack = 8f;
			Item.DamageType = DamageClass.Generic;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 30, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().blaster2 = true;
			player.GetModPlayer<ArtificerPlayer>().blaster2Equip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<StarplateBlaster>()
				.AddIngredient(ItemID.LihzahrdPowerCell)
				.AddIngredient(ItemID.LihzahrdBrick, 60)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
