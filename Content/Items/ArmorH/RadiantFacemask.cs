using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using ArtificerMod.Common;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Humanizer;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Head)]
	public class RadiantFacemask : ModItem
	{
		public static int IncreasedDmgCritChance = 2;
		public static int MaxMinion = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmgCritChance, MaxMinion);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; 

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/RadiantFacemask_Glow"),
					RadiantColoring = true
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.buyPrice(0, 12, 50, 0); 
			Item.defense = 10; 
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.02f;
			player.GetCritChance(DamageClass.Generic) += 2f;
			player.maxMinions++;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<RadiantBreastplate>() && legs.type == ModContent.ItemType<RadiantLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			string bonusTrigger = "DOWN";
            if (Main.ReversedUpDownArmorSetBonuses)
            {
				bonusTrigger = "UP";
			}

			player.setBonus = Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Armorset.Radiant").FormatWith(bonusTrigger); 
			player.GetModPlayer<ArtificerPlayer>().prismSetBonus = true;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlinesForbidden = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PearlwoodHelmet)
				.AddIngredient(ItemID.CrystalShard, 12)
				.AddIngredient(ItemID.PearlstoneBlock, 60)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
