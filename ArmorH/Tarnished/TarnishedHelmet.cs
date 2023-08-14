using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using ArtificerMod.Common;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;
using ArtificerMod.Content.Buffs.ArmorH;

namespace ArtificerMod.Content.Items.ArmorH.Tarnished
{
	[AutoloadEquip(EquipType.Head)]
	public class TarnishedHelmet : ModItem
	{
		public override void SetStaticDefaults() {

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/TarnishedHelmet_Glow")
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
			player.GetDamage(DamageClass.Generic) += 0.03f;
			player.GetCritChance(DamageClass.Generic) += 3f;
			player.GetModPlayer<ArtificerPlayer>().ammoCost95Helm = true;
		}

		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<TarnishedBreastplate>() && legs.type == ModContent.ItemType<TarnishedLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+1 Artificer Accessory Slot" +
				"\nAbility Accessory cooldowns are no longer extended" +
				"\nIf you take fatal damage, protective souls will instantly restore you to 100 health" +
				"\nThis revival effect has a 5 minute cooldown";
			player.GetModPlayer<ArtificerPlayer>().tarnishedSetBonus = true;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawShadowSubtle = !player.HasBuff(ModContent.BuffType<TarnishedRevive>());
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<PossessedHelmet>()
				.AddIngredient(ItemID.SoulofNight, 12)
				.AddIngredient(ItemID.PurificationPowder, 25)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
