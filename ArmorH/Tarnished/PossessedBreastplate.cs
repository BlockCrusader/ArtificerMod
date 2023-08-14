using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.ArmorH.Tarnished
{
	[AutoloadEquip(EquipType.Body)]
	public class PossessedBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<PossessedHelmet>();
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 10, 0, 0); 
			Item.rare = ItemRarityID.LightRed;
			Item.vanity = true;
		}
	}
}
