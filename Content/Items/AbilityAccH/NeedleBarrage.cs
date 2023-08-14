using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class NeedleBarrage : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 75;
			Item.knockBack = 3f;
			Item.crit = 20;
			Item.DamageType = DamageClass.Generic;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 50, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().needleAttack = 3;
			player.GetModPlayer<ArtificerPlayer>().needleEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<AssassinNeedles>()
				.AddIngredient(ItemID.Razorpine)
				.AddIngredient(ItemID.ShroomiteBar, 12)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
