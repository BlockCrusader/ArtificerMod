using Terraria;
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
	public class StarplateVisor : ModItem
	{
		public static int IncreasedCritChance = 2;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedCritChance);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; 

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateVisor_Glow"),
					ExtraTextureNight = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateVisor_Night")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 3, 0, 0); 
			Item.defense = 4; 
			Item.rare = ItemRarityID.Green;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Generic) += 2f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<StarplateBreastplate>() && legs.type == ModContent.ItemType<StarplateGreaves>();
		}


		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Armorset.Starplate"); 
			player.GetModPlayer<ArtificerPlayer>().starplateSetBonus = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MeteoriteBar, 5)
				.AddIngredient(ItemID.SunplateBlock, 30)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
