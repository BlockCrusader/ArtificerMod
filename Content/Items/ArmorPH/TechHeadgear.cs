﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using ArtificerMod.Common;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorPH
{
	[AutoloadEquip(EquipType.Head)]
	public class TechHeadgear : ModItem
	{
		public static int IncreasedMaxMana = 20;
		public static int IncreasedDmg = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMaxMana, IncreasedDmg);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; 

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/TechHeadgear_Glow"),
					ExtraTextureVisor = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/TechHeadgear_Visor")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 5, 0, 0); 
			Item.defense = 5; 
			Item.rare = ItemRarityID.Orange;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.01f;
			player.statManaMax2 += 20;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<TechBreastplate>() && legs.type == ModContent.ItemType<TechLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Armorset.Tech"); 
			player.GetModPlayer<ArtificerPlayer>().techSetBonus = true;
		}
	}
}
