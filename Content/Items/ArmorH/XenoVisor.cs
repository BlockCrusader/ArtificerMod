using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.Others;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Head)]
	public class XenoVisor : ModItem
	{
		public static int IncreasedDmg = 3;
		public static int IncreasedCritChance = 5;
		public static int ChanceSaveAmmo = 10;
		public static int MaxMinion = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedCritChance, ChanceSaveAmmo, MaxMinion);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/XenoVisor_Glow"),
					ExtraTextureShield = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/XenoVisor_Shield")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 24, 0, 0);
			Item.defense = 13; 
			Item.rare = ItemRarityID.Yellow;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.03f;
			player.GetCritChance(DamageClass.Generic) += 5f;
			player.maxMinions++;
			player.GetModPlayer<ArtificerPlayer>().ammoCost90Helm = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<XenoSuit>() && legs.type == ModContent.ItemType<XenoLeggings>();
		}
		public override void ArmorSetShadows(Player player)
		{
			int shieldCharge = player.GetModPlayer<ArtificerPlayer>().xenoShieldPower;
			int shieldCap = player.GetModPlayer<ArtificerPlayer>().xenoShieldCap;
			if (shieldCharge >= shieldCap)
            {
				player.armorEffectDrawOutlines = true;
            }
            else if(shieldCharge > 0)
            {
				player.armorEffectDrawOutlinesForbidden = true;
            }
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.ArtificerMod.CommonItemtooltip.Armorset.Xeno"); 
			player.GetModPlayer<ArtificerPlayer>().xenoSetBonus = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<MartianScrap>(5)
				.AddIngredient(ItemID.MartianConduitPlating, 75)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
