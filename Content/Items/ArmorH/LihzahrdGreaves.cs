using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using ArtificerMod.Content.Glowmasks;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Legs)]
	public class LihzahrdGreaves : ModItem
	{
		public static int IncreasedDmg = 6;
		public static int IncreasedCritChance = 3;
		public static int IncreasedMovementSpeed = 10;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmg, IncreasedCritChance, IncreasedMovementSpeed);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/LihzahrdGreaves_Glow")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 20, 0, 0); 
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 13; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.06f;
			player.GetCritChance(DamageClass.Generic) += 3f;
			player.moveSpeed += 0.1f;
			player.aggro += 50;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LunarTabletFragment, 5)
				.AddIngredient(ItemID.LihzahrdBrick, 40)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
