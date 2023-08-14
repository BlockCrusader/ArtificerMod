using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class LifesurgeBand : ModItem
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
			Item.rare = ItemRarityID.Green;
			Item.value = Item.buyPrice(0, 6, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.lifeRegen += 2;
			player.statLifeMax2 += 20;
			player.GetCritChance(DamageClass.Generic) += 2f;
			player.GetModPlayer<ArtificerPlayer>().lifesurge = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<LifeforceBand>()
				.AddIngredient<DireGlaze>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
