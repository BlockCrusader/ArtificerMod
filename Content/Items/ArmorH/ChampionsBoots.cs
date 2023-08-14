using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Legs)]
	public class ChampionsBoots : ModItem
	{
		public override void SetStaticDefaults() 
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 60, 0, 0); 
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 12; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.05f;
			player.GetCritChance(DamageClass.Generic) += 3f;
			player.moveSpeed += 0.05f;
		}
	}
}
