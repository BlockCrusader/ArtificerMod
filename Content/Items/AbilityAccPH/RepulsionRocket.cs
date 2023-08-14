using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	public class RepulsionRocket : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 45;
			Item.knockBack = 20f;
			Item.DamageType = DamageClass.Generic;
			Item.accessory = true;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().repulseRocket = true;
			player.GetModPlayer<ArtificerPlayer>().rRocketEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<RepulseCharge>()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
