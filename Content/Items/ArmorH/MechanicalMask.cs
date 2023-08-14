using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using ArtificerMod.Common;
using Microsoft.Xna.Framework.Graphics;
using ArtificerMod.Content.Glowmasks;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Head)]
	public class MechanicalMask : ModItem
	{
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
			player.setBonus = "+2 Artificer Accessory Slots" +
				"\n20% reduced Ability Accessory cooldowns" +
				"\nCritical hits may briefly inflict enemies with a shock" +
                "\nShocked enemies deal less contact damage and take more damage"; 
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
