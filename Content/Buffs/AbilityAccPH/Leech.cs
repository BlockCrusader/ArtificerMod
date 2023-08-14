using ArtificerMod.Content.Projectiles.AbilityAccPH;
using ArtificerMod.Content.Projectiles.AbilityAccH;
using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Buffs.AbilityAccPH
{
	public class Leech : ModBuff
	{
        public override void Update(NPC npc, ref int buffIndex)
        {
			npc.GetGlobalNPC<LeechNPC>().leeched = true;
		}
	}
}

public class LeechNPC : GlobalNPC
{
	public override bool InstancePerEntity => true;

	public bool leeched;

	public override void ResetEffects(NPC npc)
	{
		leeched = false;
	}

	public override void UpdateLifeRegen(NPC npc, ref int damage)
	{
		if (leeched)
		{
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}
			int dmgOverTime = 0;
			for (int i = 0; i < 1000; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.active 
					&& (proj.type == ModContent.ProjectileType<DarkLeechHook>() || proj.type == ModContent.ProjectileType<LeechingTetherHook>()
					|| proj.type == ModContent.ProjectileType<LifelineHook>())
					&& proj.ai[0] == 1f && proj.ai[1] == npc.whoAmI)
				{
                    if (proj.type == ModContent.ProjectileType<LifelineHook>())
                    {
						dmgOverTime += 50;
					}
                    else
                    {
						dmgOverTime += 10;
					}
					
				}
			}
			npc.lifeRegen -=  2 * dmgOverTime;
			if (damage < dmgOverTime)
			{
				damage = dmgOverTime;
			}
		}
	}
}