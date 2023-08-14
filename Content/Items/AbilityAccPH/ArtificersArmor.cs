using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.ArmorPH;

namespace ArtificerMod.Content.Items.AbilityAccPH
{
	public class ArtificersArmor : ModItem
	{
		public override void Load()
		{
			if (Main.netMode == NetmodeID.Server)
				return;

			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Head}", EquipType.Head, this);
			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Body}", EquipType.Body, this);
			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);
		}

		private void SetupDrawing()
		{
			if (Main.netMode == NetmodeID.Server)
				return;

			int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
			int equipSlotBody = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
			int equipSlotLegs = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
			ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
			ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
			ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
			ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
		}

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			SetupDrawing(); 
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 4;
			Item.value = Item.buyPrice(0, 35, 0, 0);
			Item.hasVanityEffects = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			
			player.GetModPlayer<ArtificerPlayer>().artificerArmor = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<OrnateHelmet>()
				.AddIngredient<OrnateBreastplate>()
				.AddIngredient<OrnateLeggings>()
				.AddIngredient(ItemID.MeteoriteBar, 10)
				.AddIngredient(ItemID.HellstoneBar, 10)
				.AddRecipeGroup("ArtificerMod:MetalBarsEvil", 10)
				.AddIngredient(ItemID.IronskinPotion, 3)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}
