using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	[AutoloadEquip(EquipType.Shield)]
	public class BewitchedBulwark : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.damage = 40;
			Item.knockBack = 12.5f;
			Item.defense = 3;
			Item.DamageType = DamageClass.Generic;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().bBulwark = true; 
			player.GetModPlayer<ArtificerPlayer>().bBulwarkEquip = Item;
		}
	}
}
