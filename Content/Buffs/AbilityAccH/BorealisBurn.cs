using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class BorealisBurn : ModBuff
	{
		public override void SetStaticDefaults()
		{
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<BorealisBurnNPC>().debuffed = true;
		}
	}

	public class BorealisBurnNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool debuffed;

		public override void ResetEffects(NPC npc)
		{
			debuffed = false;
		}

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (debuffed)
			{
				modifiers.FinalDamage *= 1.3f;
			}
		}

		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			if (debuffed)
			{
				modifiers.FinalDamage *= 1.3f;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (debuffed)
			{
				drawColor.R = (byte)((float)(int)drawColor.R * 0.7f);
				drawColor.G = (byte)((float)(int)drawColor.G * 0.95f);
			}
		}
	}
}