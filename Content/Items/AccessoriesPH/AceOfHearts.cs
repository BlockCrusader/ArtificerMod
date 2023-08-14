using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	public class AceOfHearts : ModItem
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

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().heartAce = true;
		}
    }
}
