using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using ArtificerMod.Content.Items.Others;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using ArtificerMod.Content.Glowmasks;

namespace ArtificerMod.Content.Items.ArmorPH
{
	[AutoloadEquip(EquipType.Legs)]
	public class StarplateGreaves : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateGreaves_Glow"),
					ExtraTextureNight = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateGreaves_Night")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 3, 50, 0); 
			Item.rare = ItemRarityID.Green;
			Item.defense = 4; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.01f;
			player.GetCritChance(DamageClass.Generic) += 1f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MeteoriteBar, 10)
				.AddIngredient(ItemID.SunplateBlock, 40)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
