using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class PygmyBracer : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 20;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.defense = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.whipRangeMultiplier += 0.15f;
		}
	}
}
