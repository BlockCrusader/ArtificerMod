using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class PrimeRage : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			if (player.GetModPlayer<ArtificerPlayer>().primeBoneCooldown > 0)
			{
                player.GetModPlayer<ArtificerPlayer>().primeBoneCooldown--;
            }

			// This code creates an effect like that of the Nebula blaze's, spawning dust on the player's hand for a "magic glove" effect
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
				Dust dust = Dust.NewDustDirect(player.Center, 0, 0, DustID.TheDestroyer, 0f, 0f, 150, default(Color), 0.25f);
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