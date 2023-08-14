using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class ArtificerArm : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.buyPrice(0, 30, 0, 0);
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().electricArm = true;
			player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
			player.autoReuseGlove = true;
		}

        public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.MechanicalGlove)
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddIngredient(ItemID.Wire, 50)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
