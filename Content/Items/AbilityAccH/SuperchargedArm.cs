using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AccessoriesH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class SuperchargedArm : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 35, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			
			player.GetModPlayer<ArtificerPlayer>().electricArm = true;
			player.GetModPlayer<ArtificerPlayer>().superArm = true;
			player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
			player.autoReuseGlove = true;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<ArtificerArm>())
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<ArtificerArm>()
				.AddIngredient(ItemID.ShroomiteBar, 12)
                .AddIngredient(ItemID.Nanites, 25)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
