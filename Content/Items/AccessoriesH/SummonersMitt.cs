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
	public class SummonersMitt : ModItem
	{
		public static int IncreasedWhipSpeed = 12;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedWhipSpeed);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(0, 15, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.SummonMeleeSpeed) += 0.15f;
			player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.12f;
			player.autoReuseGlove = true;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<BeastmasterGlove>() || equippedItem.type == ModContent.ItemType<SpiritGrasp>())
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
				.AddIngredient(ItemID.SummonerEmblem)
				.AddIngredient(ItemID.FeralClaws)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
