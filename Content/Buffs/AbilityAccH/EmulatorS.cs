using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class EmulatorS : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetCritChance(DamageClass.Generic) += 10f;
			player.runAcceleration *= 1.1f;
			player.maxRunSpeed *= 1.1f;
			player.accRunSpeed *= 1.1f;
			player.runSlowdown *= 1.1f;

			if (Main.rand.NextBool(3))
			{
				Dust dust;
				dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.GreenTorch, 0f, 0f, 0, default(Color), 1f)];
				dust.noGravity = true;
			}
		}
    }
}