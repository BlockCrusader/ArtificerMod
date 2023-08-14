using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class EmulatorM : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetDamage(DamageClass.Generic) += 0.1f;
			player.lifeRegen += 2;

			if (Main.rand.NextBool(3))
			{
				Dust dust;
				dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.BlueTorch, 0f, 0f, 0, default(Color), 1f)];
				dust.noGravity = true;
			}
		}
    }
}