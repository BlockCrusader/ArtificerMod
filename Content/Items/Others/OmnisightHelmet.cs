using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace ArtificerMod.Content.Items.Others
{
	[AutoloadEquip(EquipType.Head)]
	public class OmnisightHelmet : ModItem
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
			Item.defense = 12; 
			Item.rare = ItemRarityID.Yellow;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<OmnisightPlayer>().omnisight = true;
			player.nightVision = true;
			player.dangerSense = true;
			player.findTreasure = true;
			player.detectCreature = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.MiningShirt && legs.type == ItemID.MiningPants;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "5 defense\n15% increased mining speed";
			player.statDefense += 6;
			player.pickSpeed -= 0.15f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<UltrasightHelmet>()
				.AddIngredient(ItemID.SoulofSight, 15)
                .AddIngredient(ItemID.SpelunkerPotion, 5)
				.AddIngredient(ItemID.HunterPotion, 5)
				.AddIngredient(ItemID.TrapsightPotion, 5)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}

	public class OmnisightPlayer : ModPlayer
	{
		public bool omnisight;
		public override void ResetEffects()
		{
			omnisight = false;
		}

		public override void ModifyZoom(ref float zoom)
		{
			if (omnisight && Main.mouseRight)
			{
				zoom = 1f;
			}
		}
	}
}
