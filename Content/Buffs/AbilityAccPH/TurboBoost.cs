using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class TurboBoost : ModBuff
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

			if (Math.Abs(player.velocity.X) > 6.5f)
            {
				for (int i = 0; i < 2; i++)
				{
					int dust = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
					Main.dust[dust].velocity.X -= player.velocity.X * 0.75f;
					Main.dust[dust].velocity.Y -= player.velocity.Y * 0.75f;
				}
			}
		}
    }
}