using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Shield)]
	public class CorruptedBulwark : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 26;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 50, 0, 0);
			Item.defense = 8;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.noKnockback = true;
			player.aggro -= 250;
			if(player.statLife < (int)(player.statLifeMax2 / 4f)) 
			{
				player.GetDamage(DamageClass.Generic) += 0.15f;
            }
		}

        public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.PaladinsShield)
				.AddIngredient(ItemID.PutridScent)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
