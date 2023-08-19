using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class HeartsurgeCharm : ModItem
	{
		public static int IncreasedCritChance = 2;
		public static int IncreasedMaxLife = 20;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedCritChance, IncreasedMaxLife);
		
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(0, 16, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.lifeRegen += 2;
			player.statLifeMax2 += 20;
			player.GetCritChance(DamageClass.Generic) += 2f;
			player.GetModPlayer<ArtificerPlayer>().heartsurge = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<AceOfHearts>()
				.AddIngredient<LifesurgeBand>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
