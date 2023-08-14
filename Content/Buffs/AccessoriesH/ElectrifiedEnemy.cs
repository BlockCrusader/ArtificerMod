using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AccessoriesH
{
	public class ElectrifiedEnemy : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ElectrifiedNPC>().electricDoT = true;
		}
	}

	internal class ElectrifiedNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool electricDoT;

		public override void ResetEffects(NPC npc)
		{
			electricDoT = false;
		}

        public override void AI(NPC npc)
        {
            if (electricDoT)
            {
				Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.3f, 0.8f, 1.1f);

				if (Main.rand.NextBool(3))
				{
					int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, Scale: 0.5f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 0.7f;
				}
			}

        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (electricDoT)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}

				npc.lifeRegen -= 32;
				if (damage < 16)
				{
					damage = 16;
				}
			}
		}
	}
}