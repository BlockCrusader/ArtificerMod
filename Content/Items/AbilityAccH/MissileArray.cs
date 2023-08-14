using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	[AutoloadEquip(EquipType.Back)]
	public class MissileArray : ModItem
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
			Item.rare = ItemRarityID.Yellow;
			Item.damage = 150;
			Item.knockBack = 8f;
			Item.DamageType = DamageClass.Generic;
			Item.value = Item.buyPrice(0, 20, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().missileArray = true;
			player.GetModPlayer<ArtificerPlayer>().missileEquip = Item;
		}
	}
}
