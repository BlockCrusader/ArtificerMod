using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.AbilityAccPH;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	[AutoloadEquip(EquipType.Back)]
	public class AnkhRenewer : ModItem
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
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.buyPrice(0, 25, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[BuffID.Poisoned] = true;
			player.buffImmune[BuffID.Darkness] = true;
			player.buffImmune[BuffID.Bleeding] = true;
			player.buffImmune[BuffID.Stoned] = true;
			player.buffImmune[BuffID.BrokenArmor] = true;
			player.buffImmune[BuffID.Slow] = true;
			player.buffImmune[BuffID.Silenced] = true;
			player.buffImmune[BuffID.Cursed] = true;
			player.buffImmune[BuffID.Chilled] = true;
			player.buffImmune[BuffID.Confused] = true;
			
			player.GetModPlayer<ArtificerPlayer>().debuffRemovalAcc = 4;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Cleanser>()
				.AddIngredient(ItemID.AnkhCharm)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
