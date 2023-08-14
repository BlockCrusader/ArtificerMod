using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Body)]
	public class RadiantBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			SimpleBodyGlowPlayer.RegisterData(Item.bodySlot, () => Color.White);
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 17, 50, 0); 
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 11;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.05f;
			player.GetCritChance(DamageClass.Generic) += 2f;
			player.manaCost -= .05f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PearlwoodBreastplate)
				.AddIngredient(ItemID.CrystalShard, 16)
				.AddIngredient(ItemID.PearlstoneBlock, 80)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
