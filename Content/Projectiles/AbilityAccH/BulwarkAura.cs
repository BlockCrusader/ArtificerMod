using Terraria;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class BulwarkAura : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.timeLeft = 1; 
			Projectile.penetrate = 1;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

        public override void AI()
        {
			if (Projectile.ai[0] > 0)
            {
				Projectile.ArmorPenetration = (int)Projectile.ai[0];
			}
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			Projectile bulwark = Main.projectile[(int)Projectile.ai[1]];
			if(bulwark != null)
            {
				// This makes it so that attacks always hit enemies towards its parent projectile
				modifiers.HitDirectionOverride = (bulwark.Center.X > target.Center.X) ? 1 : (-1);
			}
            else
            {
				// This makes it so that attacks always hit enemies away from the player
				modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
			}
		}
	}
}