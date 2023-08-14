using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
    [AutoloadEquip(new EquipType[]
    {
        EquipType.HandsOn,
        EquipType.HandsOff
    })]
    public class PrimeGauntlet : ModItem
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
			Item.expert = true;
			Item.value = Item.buyPrice(0, 20, 0, 0);
		}

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ItemID.BoneGlove)
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
			
            player.GetModPlayer<ArtificerPlayer>().primeGauntlet = true;
            player.GetModPlayer<ArtificerPlayer>().primeEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
                .AddIngredient(ItemID.BoneGlove)
                .AddIngredient(ItemID.MechanicalBatteryPiece)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
