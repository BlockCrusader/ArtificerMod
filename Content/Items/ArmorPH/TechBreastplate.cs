using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.ArmorPH
{
	[AutoloadEquip(EquipType.Body)]
	public class TechBreastplate : ModItem
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
			Item.value = Item.buyPrice(0, 5, 0, 0); 
			Item.rare = ItemRarityID.Orange;
			Item.defense = 6; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.02f;
			player.GetCritChance(DamageClass.Generic) += 1f;
		}
	}
}
