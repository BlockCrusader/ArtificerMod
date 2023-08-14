using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;
using ArtificerMod.Content.Items.AccessoriesH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
    [AutoloadEquip(new EquipType[]
    {
        EquipType.HandsOn,
        EquipType.HandsOff,
		EquipType.Shoes
    })]
    public class ArtificerProsthetics : ModItem
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
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 45, 0, 0);
		}

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ModContent.ItemType<ArtificerArm>())
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
			player.GetModPlayer<ArtificerPlayer>().electricArm = true;
			player.GetModPlayer<ArtificerPlayer>().prosthetics = true;
			player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
			player.autoReuseGlove = true;
			player.accRunSpeed = 6f;
			player.jumpSpeedBoost += 2f;
			player.autoJump = true;
			player.extraFall += 20;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<SuperchargedArm>()
				.AddIngredient<OverdriveLeg>()

				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
