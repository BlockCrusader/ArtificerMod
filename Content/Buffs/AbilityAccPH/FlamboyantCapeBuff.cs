using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class FlamboyantCapeBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			if (player.GetModPlayer<ArtificerPlayer>().flamboyantCapeFlag == true)
			{
				player.aggro += 400;
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
		}
    }
}