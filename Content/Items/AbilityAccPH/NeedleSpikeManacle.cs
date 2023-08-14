using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	[AutoloadEquip(new EquipType[]
	{
		EquipType.HandsOn,
		EquipType.HandsOff
	})]
	public class NeedleSpikeManacle : ModItem
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
			Item.rare = ItemRarityID.Green;
			Item.defense = 1;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().spikeManacle = true;
			if (player.thorns < 1f)
			{
				player.thorns += 0.5f;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Shackle)
				.AddIngredient(ItemID.Cactus, 10)
				.AddRecipeGroup("ArtificerMod:MetalBars2", 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
