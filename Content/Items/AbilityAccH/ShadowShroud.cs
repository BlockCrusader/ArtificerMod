using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
    [AutoloadEquip(new EquipType[]
    {
        EquipType.Back,
        EquipType.Face
    })]
    public class ShadowShroud : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Face.Sets.PreventHairDraw[Item.faceSlot] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 2;
			Item.value = Item.buyPrice(0, 7, 50, 0);
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
            player.GetModPlayer<ArtificerPlayer>().shadowShroud = true;
			player.aggro -= 200;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<StealthShroud>()
				.AddIngredient(ItemID.SoulofNight, 15)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
