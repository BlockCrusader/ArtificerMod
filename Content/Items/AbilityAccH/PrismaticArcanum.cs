using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class PrismaticArcanum : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 90;
			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Generic;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 40, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().arcanum2 = true;
			player.GetModPlayer<ArtificerPlayer>().arcanum2Equip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<OverchargedArcanum>()
				.AddIngredient(ItemID.FairyQueenMagicItem)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
