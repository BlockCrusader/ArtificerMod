using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AccessoriesH;
using Terraria.ID;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	public class EnergyCell : ModItem
	{
		public static int Cooldown = 10;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Cooldown);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().energyCellBonus = true;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{ 
			if (equippedItem.type == ModContent.ItemType<CoolingCell>() || equippedItem.type == ModContent.ItemType<ArtificerEmblem>() || equippedItem.type == ModContent.ItemType<ClockworkStopwatch>() || equippedItem.type == ModContent.ItemType<NovaEmblem>())
			{
				return false;
			}
			return true;
		}
	}
}
