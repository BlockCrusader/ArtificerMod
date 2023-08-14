using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class ObliterationCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 190;
			Item.knockBack = 8f;
			Item.DamageType = DamageClass.Generic;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 40, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().obliterationCore = true;
			player.GetModPlayer<ArtificerPlayer>().oCoreEquip = Item;
		}
	}
}
