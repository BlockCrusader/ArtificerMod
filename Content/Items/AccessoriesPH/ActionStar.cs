using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	[AutoloadEquip(EquipType.Neck)]
	public class ActionStar : ModItem
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
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(0, 25, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().papermarioreference = true;
			player.equipmentBasedLuckBonus += 0.05f;
			player.GetModPlayer<ArtificerPlayer>().extraCritDmg = true;  
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<LuckyStar>()
				.AddIngredient<DireGlaze>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
