using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AccessoriesPH;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class ClockworkStopwatch : ModItem
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
			Item.rare = ItemRarityID.LightPurple;
			Item.value = Item.buyPrice(0, 30, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().clockworkBonus = true;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<EnergyCell>() || equippedItem.type == ModContent.ItemType<CoolingCell>() || equippedItem.type == ModContent.ItemType<ArtificerEmblem>() || equippedItem.type == ModContent.ItemType<NovaEmblem>())
			{
				return false;
			}
			return true;
		}
	}
}
