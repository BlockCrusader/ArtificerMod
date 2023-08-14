using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace ArtificerMod.Content.Items.ArmorH.Tarnished
{
	[AutoloadEquip(EquipType.Head)]
	public class PossessedHelmet : ModItem
	{
		public override void SetStaticDefaults() {

			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<PossessedLeggings>();
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 7, 50, 0);
			Item.vanity = true;
			Item.rare = ItemRarityID.LightRed;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<PossessedBreastplate>() && legs.type == ModContent.ItemType<PossessedLeggings>();
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}
	}
}
