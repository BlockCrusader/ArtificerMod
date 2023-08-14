using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class StarlightArmillary : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.buyPrice(1, 0, 0, 0);
			Item.defense = 5;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().seymoursBizzareAdventure = true;
			player.GetModPlayer<ArtificerPlayer>().starlightRiverReference = Item;
		}
	}
}
