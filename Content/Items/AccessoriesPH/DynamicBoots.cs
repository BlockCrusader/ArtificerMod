using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.Shoes)]
	public class DynamicBoots : ModItem
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
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(0, 8, 0, 0);
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<HeavyBoots>() || equippedItem.type == ModContent.ItemType<PowerBoots>())
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
			if (!player.mount.Active)
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
			player.extraFall += 20;
			player.rocketBoots = 1;
            if (!hideVisual)
            {
				player.vanityRocketBoots = 1;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<HeavyBoots>()
				.AddIngredient(ItemID.RocketBoots)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
