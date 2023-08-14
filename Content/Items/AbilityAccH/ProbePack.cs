using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	[AutoloadEquip(EquipType.Back)]
	public class ProbePack : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.damage = 60;
			Item.knockBack = 4f;
			Item.DamageType = DamageClass.Generic;
			Item.value = Item.buyPrice(0, 30, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().probePack = true;
			player.GetModPlayer<ArtificerPlayer>().probeEquip = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 15)
				.AddIngredient(ItemID.SoulofMight, 15)
                .AddIngredient(ItemID.Wire, 60)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
