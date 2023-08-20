using Terraria;
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
	public class MechanicalMask : ModItem
	{
		public static int IncreasedMaxMana = 20;
		public static int IncreasedCritChance = 2;
		public static int MaxMinion = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMaxMana, IncreasedCritChance, MaxMinion);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/MechanicalMask_Glow")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16;
			Item.value = Item.buyPrice(0, 25, 0, 0); 
			Item.defense = 12; 
			Item.rare = ItemRarityID.LightPurple;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Generic) += 2f;
			player.maxMinions++;
			player.statManaMax2 += 20;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MechanicalPlate>() && legs.type == ModContent.ItemType<MechanicalGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Armorset.Mechanical"); 
			player.GetModPlayer<ArtificerPlayer>().mechSetBonus = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddIngredient(ItemID.SoulofSight, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
