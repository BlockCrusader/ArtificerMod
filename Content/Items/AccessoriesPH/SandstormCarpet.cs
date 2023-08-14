using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	public class SandstormCarpet : ModItem
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
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ItemID.FlyingCarpet || equippedItem.type == ItemID.SandstorminaBottle)
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
			player.carpet = true;
			player.hasJumpOption_Sandstorm = true;
			player.GetModPlayer<ArtificerPlayer>().sandstormCarpet = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FlyingCarpet)
				.AddIngredient(ItemID.SandstorminaBottle)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
