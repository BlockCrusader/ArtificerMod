using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(new EquipType[]
	{
		EquipType.HandsOn,
		EquipType.HandsOff
	})]
	public class SpiritGrasp : ModItem
	{
		public static int IncreasedSummonDamage = 15;
		public static int IncreasedWhipSpeed = 12;
		public static int IncreasedWhipRange = 25;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedSummonDamage, IncreasedWhipSpeed, IncreasedWhipRange);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 42, 0, 0);
			Item.defense = 2;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.SummonMeleeSpeed) += 0.15f;
			player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.12f;
			player.whipRangeMultiplier += 0.25f;
			player.autoReuseGlove = true;
		}
		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<SummonersMitt>() || equippedItem.type == ModContent.ItemType<BeastmasterGlove>())
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<BeastmasterGlove>()
				.AddIngredient(ItemID.Ectoplasm, 10)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
