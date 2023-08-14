using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Content.Items.AbilityAccH;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Shield)]
	public class TerraShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(1, 0, 0, 0);
			Item.defense = 5;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.noKnockback = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.GetModPlayer<ArtificerPlayer>().terraShield = true;
            player.fireWalk = true;

        }

        public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.AnkhShield)
				.AddIngredient(ItemID.PaladinsShield)
                .AddIngredient<LostHeroShield>()
                .AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
