using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ArtificerMod.Content.Items.AccessoriesPH
{
	public class DeftPouch : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 30;
			Item.height = 34;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<DeftPouchPlayer>().equipped = true;
		}
	}

	public class DeftPouchPlayer : ModPlayer
	{
		public bool equipped;

		public override void ResetEffects()
		{
			equipped = false;
		}

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (equipped == true && item.CountsAsClass<RangedDamageClass>() && item.useAmmo == AmmoID.Dart)
			{
				velocity *= 1.05f;
			}
		}

		public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
			if (equipped == true && weapon.CountsAsClass<RangedDamageClass>() && weapon.useAmmo == AmmoID.Dart)
			{
				return !Main.rand.NextBool(10);
			}
            else
            {
				return true;
            }
		}
    }
}
