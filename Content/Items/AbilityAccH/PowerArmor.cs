using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ArtificerMod.Common;
using ArtificerMod.Content.Items.Others;
using ArtificerMod.Content.Items.AbilityAccPH;
using ArtificerMod.Content.Glowmasks;
using Microsoft.Xna.Framework.Graphics;

namespace ArtificerMod.Content.Items.AbilityAccH
{
	public class PowerArmor : ModItem
	{
		public override void Load()
		{
			if (Main.netMode == NetmodeID.Server)
				return;

			// Add equip textures
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

			if (!Main.dedServ)
			{
				SimpleBodyGlowPlayer.RegisterData(equipSlotBody, () => Color.White);

				HeadLayer.RegisterData(equipSlotHead, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/AbilityAccH/PowerArmor_HeadG")
				});

				LegsLayer.RegisterData(equipSlotLegs, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>("ArtificerMod/Content/Glowmasks/AbilityAccH/PowerArmor_LegsG")
				});
			}
		}

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			SetupDrawing(); // For 'costume' effect of ability
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 8;
			Item.value = Item.buyPrice(0, 50, 0, 0);
			Item.hasVanityEffects = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			
			player.GetModPlayer<ArtificerPlayer>().powerArmor = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<ArtificersArmor>()
				.AddIngredient<MartianScrap>(5)
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddIngredient(ItemID.ChlorophyteBar, 10)
				.AddRecipeGroup("ArtificerMod:MetalBarsHM3", 10)
				.AddIngredient(ItemID.IronskinPotion, 3)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
