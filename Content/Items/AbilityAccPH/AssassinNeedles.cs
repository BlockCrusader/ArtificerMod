using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	public class AssassinNeedles : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 25;
			Item.knockBack = 1.5f;
			Item.crit = 8;
			Item.DamageType = DamageClass.Generic;
			Item.accessory = true;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.buyPrice(0, 20, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().needleAttack = 2;
			player.GetModPlayer<ArtificerPlayer>().needleEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<HiddenBlade>()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ItemID.Bone, 30)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
