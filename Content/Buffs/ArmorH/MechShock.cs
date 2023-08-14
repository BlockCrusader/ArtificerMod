using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.ArmorH
{
	public class MechShock : ModBuff
	{
		public override void SetStaticDefaults()
		{
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<mechShockNPC>().shocked = true;
		}
	}

	public class mechShockNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool shocked;

		public override void ResetEffects(NPC npc) {
			shocked = false;
		}

		public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
			if (shocked && target.GetModPlayer<ArtificerPlayer>() != null)
			{
				if (target.GetModPlayer<ArtificerPlayer>().mechSetBonus)
				{
					modifiers.FinalDamage *= 0.9f;
				}
			}
		}

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(shocked && Main.player[projectile.owner] != null)
            {
				Player player = Main.player[projectile.owner];
				if (player.GetModPlayer<ArtificerPlayer>() != null)
				{
					if (player.GetModPlayer<ArtificerPlayer>().mechSetBonus)
					{
						modifiers.FinalDamage *= 1.05f;
					}
				}
			}
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if(shocked && player.GetModPlayer<ArtificerPlayer>() != null)
            {
                if (player.GetModPlayer<ArtificerPlayer>().mechSetBonus)
                {
					modifiers.FinalDamage *= 1.05f;
				}
            }
        }

        public override void AI(NPC npc)
		{
			if (shocked)
			{
				if (Main.rand.NextBool(3))
				{
					int num554 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.TheDestroyer, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num554].noGravity = true;
				}
			}
		}
	}
}