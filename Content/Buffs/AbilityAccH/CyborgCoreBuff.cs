using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class CyborgCoreBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 28;
			player.lifeRegen += 28;
			player.runAcceleration *= 0.5f;
			player.maxRunSpeed *= 0.5f;
			player.accRunSpeed *= 0.5f;
			player.runSlowdown *= 2f;


			// Dust FX
			Dust dust;
			dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.HealingPlus)];
			dust.noGravity = true;
			dust.shader = GameShaders.Armor.GetSecondaryShader(86, Main.LocalPlayer);
		}
	}
}