using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.Others;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Head)]
	public class AstralHeadgear : ModItem
	{		
		public static int IncreasedDmg = 3;
		public static int IncreasedCritChance = 6;
		public static int ReducedManaCost = 5;
		public static int ChanceSaveAmmo = 10;
		public static int MaxMinion = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedCritChance, ReducedManaCost, ChanceSaveAmmo, MaxMinion);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; 

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/AstralHeadgear_Glow"),
					ExtraTextureVisor = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/AstralHeadgear_Visor")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 35, 0, 0); 
			Item.defense = 15; 
			Item.rare = ItemRarityID.Red;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.03f;
			player.GetCritChance(DamageClass.Generic) += 6f;
			player.maxMinions++;
			player.manaCost -= 0.05f;
			player.GetModPlayer<ArtificerPlayer>().ammoCost90Helm = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<AstralCuirass>() && legs.type == ModContent.ItemType<AstralGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Armorset.AstralNova"); 
			player.GetModPlayer<ArtificerPlayer>().astralSetBonus = true;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadowLokis = true; 
			player.armorEffectDrawShadowSubtle = true;
			player.armorEffectDrawOutlines = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<NovaFragment>(10)
				.AddIngredient(ItemID.LunarBar, 8)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
