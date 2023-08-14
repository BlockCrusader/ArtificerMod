using ArtificerMod.Content.Glowmasks;
using ArtificerMod.Content.Items.Others;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Body)]
	public class AstralCuirass : ModItem
	{
		private int cape = -1;

		public override void Load()
		{
			if (!Main.dedServ) 
			{
				cape = EquipLoader.AddEquipTexture(Mod, Texture + "_Back", EquipType.Back, null, Name + "_Back");
			}
		}

		public override void SetStaticDefaults()
		{
			ArmorIDs.Body.Sets.IncludedCapeBack[Item.bodySlot] = cape;
			ArmorIDs.Body.Sets.IncludedCapeBackFemale[Item.bodySlot] = cape; 

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


			if (!Main.dedServ)
			{
				SimpleBodyGlowPlayer.RegisterData(Item.bodySlot, () => Color.White);

				CapeLayer.RegisterData(Item.bodySlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorH/AstralCuirass_Cape")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 70, 0, 0); 
			Item.rare = ItemRarityID.Red;
			Item.defense = 17;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}


		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.06f;
			player.GetCritChance(DamageClass.Generic) += 3f;
			player.maxMinions++;
			player.lifeRegen += 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<NovaFragment>(20)
				.AddIngredient(ItemID.LunarBar, 16)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
