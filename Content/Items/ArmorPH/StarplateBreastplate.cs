using ArtificerMod.Common;
using ArtificerMod.Content.Glowmasks;
using ArtificerMod.Content.Items.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.ArmorPH
{
	[AutoloadEquip(EquipType.Body)]
	public class StarplateBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				SpecialBodyLayer.RegisterData(Item.bodySlot, new SpecialBodyLayerData()
				{
					TextureNight = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateBreastplate_Night"),
					TextureDay = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateBreastplate_Glow")
				});
				SpecialFrontArmLayer.RegisterData(Item.bodySlot, new SpecialBodyLayerData()
				{
					TextureNight = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateBreastplate_Night"),
					TextureDay = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateBreastplate_Glow")
				});
				SpecialBackArmLayer.RegisterData(Item.bodySlot, new SpecialBodyLayerData()
				{
					TextureNight = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateBreastplate_Night"),
					TextureDay = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/StarplateBreastplate_Glow")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 4, 0, 0); 
			Item.rare = ItemRarityID.Green;
			Item.defense = 5; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.03f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MeteoriteBar, 15)
				.AddIngredient(ItemID.SunplateBlock, 50)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
