using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Content.Items.AccessoriesPH;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Back)]
	public class PredatorQuiver : ModItem
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
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(0, 40, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magicQuiver = true;
			player.arrowDamage += 0.1f;
			player.GetArmorPenetration(DamageClass.Ranged) += 12f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MagicQuiver)
				.AddIngredient<MorbidNecklace>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
