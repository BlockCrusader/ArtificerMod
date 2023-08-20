using ArtificerMod.Content.Glowmasks;
using ArtificerMod.Content.Items.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Body)]
	public class XenoSuit : ModItem
	{
		public static int IncreasedMaxMana = 20;
		public static int IncreasedDmg = 3;
		public static int IncreasedCritChance = 4;
		public static int MaxMinion = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMaxMana, IncreasedDmg, IncreasedCritChance, MaxMinion);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				SimpleBodyGlowPlayer.RegisterData(Item.bodySlot, () => Color.White);

				SpecialBodyLayer.RegisterData(Item.bodySlot, new SpecialBodyLayerData()
				{
					TextureShield = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/XenoSuit_Shield")
				});
				SpecialFrontArmLayer.RegisterData(Item.bodySlot, new SpecialBodyLayerData()
				{
					TextureShield = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/XenoSuit_Shield")
				});
				SpecialBackArmLayer.RegisterData(Item.bodySlot, new SpecialBodyLayerData()
				{
					TextureShield = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/XenoSuit_Shield")
				});
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
			player.GetDamage(DamageClass.Generic) += 0.03f;
			player.GetCritChance(DamageClass.Generic) += 4f;
			player.maxMinions++;
			player.statManaMax2 += 20;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<MartianScrap>(6)
				.AddIngredient(ItemID.MartianConduitPlating, 100)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
