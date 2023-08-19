using ArtificerMod.Common;
using ArtificerMod.Content.Glowmasks;
using ArtificerMod.Content.Items.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.ArmorH
{
	[AutoloadEquip(EquipType.Body)]
	public class ChampionsCuirass : ModItem
	{
		public static int IncreasedMaxMana = 20;
		public static int IncreasedDmgCritChance = 3;
		public static int MaxMinion = 1;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMaxMana, IncreasedDmgCritChance, MaxMinion);

		private int cape = -1;

		public override void Load()
		{
			if (!Main.dedServ) 
			{
				cape = EquipLoader.AddEquipTexture(Mod, Texture + "_Back", EquipType.Back, null, Name + "_Back");
			}
		}

		public override void SetStaticDefaults()
		{
			ArmorIDs.Body.Sets.IncludedCapeBack[Item.bodySlot] = cape;
			ArmorIDs.Body.Sets.IncludedCapeBackFemale[Item.bodySlot] = cape; // Oddly enough, female players have a seperate variable for capes

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
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 14; 
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.03f;
			player.GetCritChance(DamageClass.Generic) += 3f;
			player.maxMinions++;
			player.statManaMax2 += 20;
		}
	}
}
