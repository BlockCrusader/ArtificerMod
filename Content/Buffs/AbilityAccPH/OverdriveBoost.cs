using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class OverdriveBoost : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.accRunSpeed *= 3.5f;
			if (player.accRunSpeed < 22.3125f)
			{
				player.accRunSpeed = 22.3125f;
			}
			player.runAcceleration *= 2f;
			player.runSlowdown *= 2f;
			if (!player.HasBuff(BuffID.Panic)) 
			{
				player.maxRunSpeed *= 3.5f;
			}
			else // Nerf stacking max run speed with Panic Necklace
			{
				player.maxRunSpeed *= 2f;
			}

			player.jumpSpeedBoost *= 4f;
			if(player.jumpSpeedBoost < 9.6f)
            {
				player.jumpSpeedBoost = 9.6f;
			}

			// Dust FX:
			if (player.velocity.Length() > 6.5f)
			{
				for (int i = 0; i < 3; i++)
				{
					int num21 = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, DustID.Electric, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[num21].noGravity = true;
					Main.dust[num21].noLight = true;
					Main.dust[num21].velocity.X -= player.velocity.X * 0.8f;
					Main.dust[num21].velocity.Y -= player.velocity.Y * 0.8f;
				}
			}
		}
    }
}