using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class ProstheticsBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetAttackSpeed(DamageClass.Melee) += 0.3f;

			player.accRunSpeed *= 3f;
			if (player.accRunSpeed < 19.125f)
			{
				player.accRunSpeed = 19.125f;
			}
			player.runAcceleration *= 2f;
			player.runSlowdown *= 2f;
			if (!player.HasBuff(BuffID.Panic))
			{
				player.maxRunSpeed *= 3f;
			}
			else // Nerf stacking max run speed with Panic Necklace
			{
				player.maxRunSpeed *= 1.75f;
			}

			player.jumpSpeedBoost *= 3f;
			if (player.jumpSpeedBoost < 7.2f)
			{
				player.jumpSpeedBoost = 7.2f;
			}

			if (player.velocity.Length() > 6.5f)
			{
				for (int i = 0; i < 3; i++)
				{
					int dustID;
                    switch (Main.rand.Next(4))
                    {
						case 0:
							dustID = DustID.YellowStarDust;
							break;
						case 1:
							dustID = DustID.FireworkFountain_Yellow;
							break;
						case 2:
							dustID = DustID.Firework_Yellow;
							break;
						default:
							dustID = DustID.YellowTorch;
							break;
                    }

					int dust = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, dustID, 0f, 0f, 150, default(Color), 0.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
					Main.dust[dust].velocity.X -= player.velocity.X * 0.8f;
					Main.dust[dust].velocity.Y -= player.velocity.Y * 0.8f;
				}
			}

			if (Main.dedServ)
			{
				return;
			}
			Vector2 handOffset = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
			if (player.direction != 1)
			{
				handOffset.X = (float)player.bodyFrame.Width - handOffset.X;
			}
			if (player.gravDir != 1f)
			{
				handOffset.Y = (float)player.bodyFrame.Height - handOffset.Y;
			}
			handOffset -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
			Vector2 rotatedHandPosition = player.RotatedRelativePoint(player.MountedCenter - new Vector2(20f, 42f) / 2f + handOffset) - player.velocity;
			for (int i = 0; i < 2; i++)
			{
				int dustID;
				switch (Main.rand.Next(4))
				{
					case 0:
						dustID = DustID.YellowStarDust;
						break;
					case 1:
						dustID = DustID.FireworkFountain_Yellow;
						break;
					case 2:
						dustID = DustID.Firework_Yellow;
						break;
					default:
						dustID = DustID.YellowTorch;
						break;
				}

				Dust dust = Dust.NewDustDirect(player.Center, 0, 0, dustID, 0f, 0f, 150, default(Color), 0.25f);
				dust.position = rotatedHandPosition;
				dust.velocity = Vector2.Zero;
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.velocity += player.velocity;
				if (Main.rand.NextBool(2))
				{
					dust.position += Utils.RandomVector2(Main.rand, -4f, 4f);
					dust.scale += Main.rand.NextFloat() * 0.5f;
				}
			}
		}
    }
}