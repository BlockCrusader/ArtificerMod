using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace ArtificerMod.Content.Items.Others
{
	[AutoloadEquip(EquipType.Head)]
	public class UltrasightHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 25, 0, 0); 
			Item.defense = 5; 
			Item.rare = ItemRarityID.LightPurple;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<UltrasightPlayer>().ultrasight = true;
			player.nightVision = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.MiningShirt && legs.type == ItemID.MiningPants;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "10% increased mining speed";
			player.pickSpeed -= 0.1f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<GazersGoggles>()
				.AddIngredient(ItemID.UltrabrightHelmet)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}

	public class UltrasightPlayer : ModPlayer
	{
		public bool ultrasight;
		public override void ResetEffects()
		{
			ultrasight = false;
		}

		public override void ModifyZoom(ref float zoom)
		{
			if (ultrasight && Main.mouseRight)
			{
				zoom = 0.8f;
			}
		}
	}
}
