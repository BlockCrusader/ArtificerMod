using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class SwaggerCapeBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			if (player.GetModPlayer<ArtificerPlayer>().swaggerCapeFlag == true)
			{
				player.aggro += 500;
			}
			
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}

			if (Main.rand.NextBool())
			{
				Dust dust;
				dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.GoldCoin, 0f, 0f, 0, default(Color), 0.1f)];
				dust.noGravity = true;
			}
			if (Main.rand.NextBool())
			{
				Dust dust;
				dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 0, default(Color), 1f)];
				dust.noGravity = true;
			}
		}
    }
}