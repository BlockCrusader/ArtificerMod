using ArtificerMod.Common;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Body)]
	public class LihzahrdPlate : ModItem
	{
		public static int IncreasedDmg = 6;
		public static int IncreasedCritChance = 3;
		public static int ChanceSaveAmmo = 5;
		public static int MaxMinion = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedCritChance, ChanceSaveAmmo, MaxMinion);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				SimpleBodyGlowPlayer.RegisterData(Item.bodySlot, () => Color.White);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 14; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.06f;
			player.GetCritChance(DamageClass.Generic) += 3f;
			player.maxMinions++;
			player.GetModPlayer<ArtificerPlayer>().ammoCost95Chest = true;
			player.aggro += 50;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LunarTabletFragment, 6)
				.AddIngredient(ItemID.LihzahrdBrick, 50)
				.AddIngredient(ItemID.LihzahrdPowerCell)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
