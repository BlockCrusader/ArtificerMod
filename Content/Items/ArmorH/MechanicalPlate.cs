using ArtificerMod.Common;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Body)]
	public class MechanicalPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ)
			{
				SimpleBodyGlowPlayer.RegisterData(Item.bodySlot, () => Color.White);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 16; 
			Item.height = 16;
			Item.value = Item.buyPrice(0, 30, 0, 0); 
			Item.rare = ItemRarityID.LightPurple;
			Item.defense = 13;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.1f;
			player.GetModPlayer<ArtificerPlayer>().ammoCost95Chest = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 20)
				.AddIngredient(ItemID.SoulofMight, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
