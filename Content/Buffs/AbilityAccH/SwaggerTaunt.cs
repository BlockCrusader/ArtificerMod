using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccH
{
	public class SwaggerTaunt : ModBuff
	{
		public override void SetStaticDefaults()
		{
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<TauntNPC>().taunted = true;
		}
	}

	public class TauntNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool taunted;

		public override void ResetEffects(NPC npc)
		{
			taunted = false;
		}

		public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
			if (taunted)
			{
				modifiers.FinalDamage *= 1.05f;
			}
		}

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (taunted)
			{
				modifiers.FinalDamage *= 1.1f;
			}
		}

		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			if (taunted)
			{
				modifiers.FinalDamage *= 1.1f;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (taunted)
			{
				drawColor.G = (byte)((float)(int)drawColor.G * 0.85f);
				drawColor.B = (byte)((float)(int)drawColor.B * 0.7f);
			}
		}
	}
}