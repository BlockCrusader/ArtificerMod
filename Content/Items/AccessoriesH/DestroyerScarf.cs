using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Neck)]
	public class DestroyerScarf : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.expert = true;
			Item.defense = 6;
		}

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
			// Disallow stacking the Destroyer Scarf with the Worm Scarf, to avoid excessively high dodge DR
			if (equippedItem.type == ItemID.WormScarf)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetArmorPenetration(DamageClass.Generic) += 8;
			player.endurance += 0.18f;
		}

        public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.WormScarf)
				.AddIngredient(ItemID.MechanicalWagonPiece)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
