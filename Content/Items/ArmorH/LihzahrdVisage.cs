﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using ArtificerMod.Common;
using Microsoft.Xna.Framework.Graphics;
using ArtificerMod.Content.Glowmasks;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Head)]
	public class LihzahrdVisage : ModItem
	{
		public static int IncreasedDmg = 4;
		public static int IncreasedCritChance = 3;
		public static int ReducedManaCost = 10;
		public static int MaxMinion = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedCritChance, ReducedManaCost, MaxMinion);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/LihzahrdVisage_Glow")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 24, 0, 0);
			Item.defense = 12;
			Item.rare = ItemRarityID.Yellow;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.04f;
			player.GetCritChance(DamageClass.Generic) += 3f;
			player.maxMinions++;
			player.manaCost -= .1f;
			player.aggro += 50;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<LihzahrdPlate>() && legs.type == ModContent.ItemType<LihzahrdGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Armorset.Lihzahrd"); 
			player.GetModPlayer<ArtificerPlayer>().lihzahrdSetBonus = true;
			player.aggro += 50;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadowSubtle = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LunarTabletFragment, 4)
				.AddIngredient(ItemID.LihzahrdBrick, 30)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
