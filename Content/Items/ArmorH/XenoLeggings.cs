using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ArtificerMod.Content.Items.Others;
using Terraria.ID;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Legs)]
	public class XenoLeggings : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/XenoLeggings_Blank"),
					ExtraTextureShield = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/XenoLeggings_Shield")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 20, 0, 0); 
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 14; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.03f;
			player.GetCritChance(DamageClass.Generic) += 5f;
			player.moveSpeed += 0.08f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<MartianScrap>(4)
				.AddIngredient(ItemID.MartianConduitPlating, 50)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
