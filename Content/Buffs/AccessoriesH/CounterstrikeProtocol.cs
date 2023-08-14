using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AccessoriesH
{
	public class CounterstrikeProtocol : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			player.GetDamage(DamageClass.Generic) += 0.12f;
			player.aggro -= 1000;

			if (player.immuneAlpha < 80)
			{
				player.immuneAlpha = 80;
			}
		}
    }
}