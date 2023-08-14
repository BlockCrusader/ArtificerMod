using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using ArtificerMod.Common;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Head)]
	public class ChampionsHeadpiece : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; 
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16; 
			Item.value = Item.buyPrice(0, 60, 0, 0); 
			Item.defense = 12;
			Item.rare = ItemRarityID.Yellow;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.03f;
			player.GetCritChance(DamageClass.Generic) += 4f;
			player.GetModPlayer<ArtificerPlayer>().ammoCost95Helm = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ChampionsCuirass>() && legs.type == ModContent.ItemType<ChampionsBoots>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+2 Artificer Accessory Slots" +
				"\nTerraria itself grants you increased damage or increased survivability, based on your health"; 
			player.GetModPlayer<ArtificerPlayer>().championSetBonus = true;
		}
	}
}
