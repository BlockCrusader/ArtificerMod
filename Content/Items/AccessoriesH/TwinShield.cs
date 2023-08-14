using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Shield)]
	public class TwinShield : ModItem
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
			Item.defense = 4;
			Item.DamageType = DamageClass.Melee;
			Item.damage = 30;
			Item.knockBack = 9;
			Item.expert = true;
		}

        public override bool MeleePrefix()
        {
            return false;
        }

        public override bool WeaponPrefix()
        {
            return false;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ItemID.EoCShield)
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
			player.noKnockback = true;
			player.dashType = 2;
            player.GetModPlayer<ArtificerPlayer>().tShieldEquip = Item;
        }

        public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.EoCShield)
				.AddIngredient(ItemID.MechanicalWheelPiece)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
