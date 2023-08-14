using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace ArtificerMod.Content.Items.ArmorH.Tarnished
{
	[AutoloadEquip(EquipType.Legs)]
	public class PossessedLeggings : ModItem
	{
		public override void SetStaticDefaults() {

			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<PossessedBreastplate>();
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 5, 0, 0); 
			Item.rare = ItemRarityID.LightRed;
			Item.vanity = true;
		}
	}
}
