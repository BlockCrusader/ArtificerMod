using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.Shoes)]
	public class HeavyBoots : ModItem
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
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 3, 0, 0);
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<DynamicBoots>() || equippedItem.type == ModContent.ItemType<PowerBoots>())
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if(!player.mount.Active)
            {
				if (!player.PortalPhysicsEnabled)
				{
					player.maxFallSpeed *= 2f;
				}
				else
				{
					player.maxFallSpeed *= 1.25f;
				}
			}
			player.GetModPlayer<ArtificerPlayer>().heavyShoes = true;
			player.extraFall += 15;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Silk, 12)
				.AddRecipeGroup("ArtificerMod:MetalBars2", 10)
				.AddRecipeGroup("Sand", 100)
				.AddTile(TileID.HeavyWorkBench)
				.Register();
		}
	}
}
