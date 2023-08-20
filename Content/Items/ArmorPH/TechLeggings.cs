using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorPH
{
	[AutoloadEquip(EquipType.Legs)]
	public class TechLeggings : ModItem
	{
		public static int IncreasedDmgCritChance = 2;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDmgCritChance);

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/ArmorPH/TechLeggings_Glow")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 5, 0, 0); 
			Item.rare = ItemRarityID.Orange;
			Item.defense = 5; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.02f;
			player.GetCritChance(DamageClass.Generic) += 2f;
		}
	}
}
