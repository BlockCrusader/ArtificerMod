using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AbilityAccH
{
    [AutoloadEquip(new EquipType[]
    {
        EquipType.Back,
        EquipType.Neck
    })]
    public class SwaggerCape : ModItem
	{
		public static int IncreasedCritChance = 5;
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedCritChance);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(0, 7, 50, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.GetModPlayer<ArtificerPlayer>().swaggerCape = true;
			player.aggro += 250;
			player.GetCritChance(DamageClass.Generic) += 5f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<FlamboyantCape>()
				.AddIngredient(ItemID.GoldDust, 20)
				.AddIngredient(ItemID.CursedFlame, 10)
				.AddTile(TileID.TinkerersWorkbench)
				.AddDecraftCondition(Condition.CorruptWorld)
				.Register();

			CreateRecipe()
				.AddIngredient<FlamboyantCape>()
				.AddIngredient(ItemID.GoldDust, 20)
				.AddIngredient(ItemID.Ichor, 10)
				.AddTile(TileID.TinkerersWorkbench)
				.AddDecraftCondition(Condition.CrimsonWorld)
				.Register();
		}
	}
}
