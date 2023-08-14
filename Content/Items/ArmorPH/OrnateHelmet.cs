using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.Others;
using System.Collections.Generic;

namespace ArtificerMod.Content.Items.ArmorPH
{
	[AutoloadEquip(EquipType.Head)]
	public class OrnateHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 2, 50, 0);
			Item.defense = 2; 
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Generic) += 2f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<OrnateBreastplate>() && legs.type == ModContent.ItemType<OrnateLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+1 Artificer Accessory Slot" +
                "\nAbility Accessory cooldowns are no longer extended" +
				"\n2 defense"; 
			player.GetModPlayer<ArtificerPlayer>().ornateSetBonus = true;
			player.statDefense += 2;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("ArtificerMod:MetalBars3", 5)
				.AddRecipeGroup("ArtificerMod:MetalBars4", 4)
				.AddIngredient(ItemID.Ruby, 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
