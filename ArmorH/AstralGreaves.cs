using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using ArtificerMod.Content.Items.Others;
using Terraria.ID;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Legs)]
	public class AstralGreaves : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
			{
				Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/AstralGreaves_Glow")
			});
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 52, 50, 0);
			Item.rare = ItemRarityID.Red;
			Item.defense = 16;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.04f;
			player.GetCritChance(DamageClass.Generic) += 4f;
			player.statManaMax2 += 20;
			player.moveSpeed += 0.12f;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<NovaFragment>(15)
				.AddIngredient(ItemID.LunarBar, 12)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
