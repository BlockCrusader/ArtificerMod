using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Back)]
	public class PermafrostShell : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 45, 0, 0);
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().permafrost = true;
			player.GetModPlayer<ArtificerPlayer>().pShellEquip = Item;
			if (player.statLife < (int)(player.statLifeMax2 / 2f)) 
			{
				player.AddBuff(BuffID.IceBarrier, 5);
				player.iceBarrier = false; // Disables Frozen Turtle Shell visual
			}
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
        }

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FrozenTurtleShell)
				.AddIngredient<PermafrostCloak>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
