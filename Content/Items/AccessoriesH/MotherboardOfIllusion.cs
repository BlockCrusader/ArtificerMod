using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using Terraria.ID;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class MotherboardOfIllusion : ModItem
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
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.expert = true;
		}
        
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            // Disallow stacking the Motherboard with the Brain, to avoid excessively high dodge chance when paired with the Black Belt
            if (equippedItem.type == ItemID.BrainOfConfusion) 
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
			player.GetModPlayer<ArtificerPlayer>().illusionMotherboard = true;
            player.aggro -= 200;
		}

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BrainOfConfusion)
                .AddIngredient(ItemID.MechanicalWagonPiece)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
