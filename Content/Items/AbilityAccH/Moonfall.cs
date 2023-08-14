using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class Moonfall : ModItem
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
			Item.damage = 200;
			Item.knockBack = 10f;
			Item.DamageType = DamageClass.Generic;
			Item.value = Item.buyPrice(1, 0, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().orbitalStrike = true;
			player.GetModPlayer<ArtificerPlayer>().moonfallEquip = Item;
		}
	}
}
