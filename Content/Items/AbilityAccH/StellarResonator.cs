using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.Others;
using ArtificerMod.Content.Buffs;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	[AutoloadEquip(EquipType.Back)]
	public class StellarResonator : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Red;
			Item.damage = 200;
			Item.knockBack = 8f;
			Item.DamageType = DamageClass.Generic;
			Item.value = Item.buyPrice(0, 50, 0, 0);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ArtificerPlayer>().stellarResonator = true;
			player.GetModPlayer<ArtificerPlayer>().resonatorEquip = Item;
            if (player.HasBuff(ModContent.BuffType<AbilityCooldown>()))
            {
				player.GetDamage(DamageClass.Generic) += 0.04f;
				player.GetCritChance(DamageClass.Generic) += 4f;
            }
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<NovaFragment>(18)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
