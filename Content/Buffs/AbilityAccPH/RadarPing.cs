using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class RadarPing : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs, like The Destroyer.
			// Other mods may check it for different purposes.
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<radarPingNPC>().revealed = true;
		}
	}

	public class radarPingNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool revealed;

        public override void ResetEffects(NPC npc)
        {
			revealed = false;
        }

        public override void AI(NPC npc)
		{
			if (revealed)
			{
				Lighting.AddLight(npc.Center, .7f, .6f, .1f);
				if (Main.rand.NextBool(10))
				{
					int d = Dust.NewDust(npc.position, npc.width, npc.height, DustID.TreasureSparkle, 0f, 0f, 0, default(Color), 1f);
					Main.dust[d].noGravity = true;
				}
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (revealed)
			{
				drawColor.G = (byte)((float)(int)drawColor.G * 0.9f);
				drawColor.B = (byte)((float)(int)drawColor.B * 0.25f);
			}
		}
	}
}