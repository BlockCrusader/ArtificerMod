using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Legs)]
	public class ChampionsBoots : ModItem
	{
		public static int IncreasedDmg = 5;
		public static int IncreasedCritChance = 3;
		public static int IncreasedMovementSpeed = 5;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedCritChance, IncreasedMovementSpeed);
		
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
