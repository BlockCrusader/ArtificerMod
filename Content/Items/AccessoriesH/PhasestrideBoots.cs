using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ArtificerMod.Content.Items.AbilityAccH;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace ArtificerMod.Content.Items.AccessoriesH
{
	[AutoloadEquip(EquipType.Shoes)]
	public class PhasestrideBoots : ModItem
	{
		public static int IncreasedMovementSpeed = 12;
 		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMovementSpeed);

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.buyPrice(0, 20, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.dashType = 0;
			player.dash = 0;
			player.accRunSpeed = 6.9375f; 
			player.moveSpeed += 0.12f;
			player.GetModPlayer<PhasestrideWarpPlayer>().phasestrideEquip = true;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (equippedItem.type == ModContent.ItemType<ShadowSneakers>())
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<ShadowSneakers>()
				.AddIngredient<ChaosGem>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
			
		}
	}


	public class PhasestrideWarpPlayer : ModPlayer
	{
		public const int dashRight = 0;
		public const int dashLeft = 1;

		public int dashDir = -1;

		public bool phasestrideEquip;
		public int dashDelay = 0; 

		public override void ResetEffects()
		{
			phasestrideEquip = false;

			if (dashDelay <= 0 && Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[2] < 15)
			{
				dashDir = dashRight;
				
			}
			else if (dashDelay <= 0 && Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[3] < 15)
			{
				dashDir = dashLeft;
				
			}
			else
			{
				dashDir = -1;
			}
		}

		public override void PreUpdateMovement()
		{
			if (CanUseDash() && dashDir != -1 && dashDelay == 0)
			{
				PhasestrideDash(dashDir == dashLeft);
				dashDelay = 60;
			}

			if (dashDelay > 0)
				dashDelay--;
		}

		public void PhasestrideDash(bool left)
       	 	{
			if(Player != Main.LocalPlayer)
			{
				return;
			}
			Vector2 playerPos = Player.position;
			Vector2 teleportDest = playerPos;
			Vector2 oldVelocity = Player.velocity;

			for (int i = 350; i > 0; i--)
			{
				bool validTeleport = true; 
				int displace = i;
    
                		if (left)
                		{
					displace *= -1;
                		}
				Vector2 targetPosition = new Vector2(playerPos.X + displace, playerPos.Y); // The target destination for this attempt
				targetPosition.X -= Player.width / 2;

				// Run the intended destination by the RoD's teleport conditions
				// World border
				if (!(targetPosition.X > 50f) || !(targetPosition.X < (float)(Main.maxTilesX * 16 - 50)) || !(targetPosition.Y > 50f) || !(targetPosition.Y < (float)(Main.maxTilesY * 16 - 50)))
				{
					validTeleport = false;
				}
				int num = (int)(targetPosition.X / 16f);
				int num2 = (int)(targetPosition.Y / 16f);
				// Prevents teleporting into the Jungle Temple, etc. early
				if ((Main.tile[num, num2].WallType == 87 && !NPC.downedPlantBoss && (Main.remixWorld || (double)num2 > Main.worldSurface)) || Collision.SolidCollision(targetPosition, Player.width, Player.height))
				{
					validTeleport = false;
				}

                		if (validTeleport) // RoD teleport conditions passed; the teleport can be gone through with
                		{
					teleportDest = targetPosition;
					break;
				}
			}

			Player.Teleport(teleportDest, 10);
			NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, teleportDest.X, teleportDest.Y, 10);

			Player.velocity.X = oldVelocity.X;
			// Reverses player velocity if they dash in the opposite direction, better perserving their speed upon turning
			if ((Player.velocity.X > 0 && left) || (Player.velocity.X < 0 && !left))
            		{
				Player.velocity.X *= -1f;
			}
		}

		private bool CanUseDash()
		{
			return phasestrideEquip
				&& Player.dashType == 0 
				&& !Player.mount.Active;
		}
	}
}

