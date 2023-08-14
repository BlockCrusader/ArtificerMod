using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace ArtificerMod.Content.Items.Others
{
	[AutoloadEquip(EquipType.Head)]
	public class GazersGoggles : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 15, 50, 0);
			Item.defense = 2; 
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<GazersGogglesPlayer>().goggles = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Goggles)
				.AddIngredient(ItemID.Binoculars)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}

	public class GazersGogglesPlayer : ModPlayer
	{
		public bool goggles;
        public override void ResetEffects()
        {
			goggles = false;
        }

        public override void ModifyZoom(ref float zoom)
		{
			if (goggles && Main.mouseRight)
			{
				zoom = (2f/3f);
			}
		}
	}
}
