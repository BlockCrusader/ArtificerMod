using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class Lifeline : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.DamageType = DamageClass.Generic;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.buyPrice(0, 30, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().tetherLeech = 3;
			player.GetModPlayer<ArtificerPlayer>().tetherEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<DarkLeech>()
				.AddIngredient(ItemID.ChlorophyteBar, 12)
				.AddIngredient(ItemID.LifeFruit)
				.AddTile(TileID.TinkerersWorkbench)
				.AddDecraftCondition(Condition.CorruptWorld)
				.Register();

			CreateRecipe()
				.AddIngredient<LeechingTether>()
				.AddIngredient(ItemID.ChlorophyteBar, 12)
				.AddIngredient(ItemID.LifeFruit)
				.AddTile(TileID.TinkerersWorkbench)
				.AddDecraftCondition(Condition.CrimsonWorld)
				.Register();
		}
	}
}
