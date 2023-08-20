using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.Others;
using ArtificerMod.Content.Items.AccessoriesPH;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	public class NovaEmblem : ModItem
	{
		public static int IncreasedDmg = 5;
		public static int Cooldown = 33;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, Cooldown);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.buyPrice(0, 50, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += 0.05f;
			player.GetModPlayer<ArtificerPlayer>().astralEmblemBonus = true;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<CoolingCell>() || equippedItem.type == ModContent.ItemType<ArtificerEmblem>() || equippedItem.type == ModContent.ItemType<ClockworkStopwatch>() || equippedItem.type == ModContent.ItemType<EnergyCell>())
			{
				return false;
			}
			return true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<ArtificerEmblem>()
				.AddIngredient<NovaFragment>(12)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
