using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace ArtificerMod.Content.Items.Others
{
	public class MartianScrap : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.maxStack = 9999;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, new Color(0, 175, 75, 255).ToVector3()); 
		}

	}
}