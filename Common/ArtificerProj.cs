using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ArtificerMod.Common
{
	public class ElectricCharge : GlobalProjectile 
    {
        public override void AI(Projectile projectile)
        {
            if (projectile.hostile || projectile.damage <= 0 || projectile.noEnchantments
                || projectile.minion || projectile.sentry)
            {
                return;
            }
            Player owner = Main.player[projectile.owner];
            ArtificerPlayer artificer = owner.GetModPlayer<ArtificerPlayer>();

            if (artificer.electricArm)
            {
                if(Main.rand.NextBool(3 * (1 + projectile.extraUpdates)))
                {
                    if (ProjectileID.Sets.IsAWhip[projectile.type])
                    {
                        projectile.WhipPointsForCollision.Clear();
                        Projectile.FillWhipControlPoints(projectile, projectile.WhipPointsForCollision);
                        Vector2 vector = projectile.WhipPointsForCollision[projectile.WhipPointsForCollision.Count - 1];

                        int dustIndex = Dust.NewDust(vector, 0, 0,
                                                     DustID.Electric, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f,
                                                     Scale: 0.4f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].velocity *= 0.7f;
                    }
                    else
                    {
                        int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Electric, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, Scale: 0.4f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].velocity *= 0.7f;
                    }
                }
            }
        }
    }

	public class ArcanumMissiles : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		public bool arcanumAI = false;
		int timer = 60;

		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            // Actually only applies to magic missile and rainbow rod projectiles from the Arcanum ability accessories
			return entity.type == ProjectileID.MagicMissile || entity.type == ProjectileID.RainbowRodBullet;
        }

		// Stalls the AI from starting up for ~30 ticks
        public override bool PreAI(Projectile projectile)
        {
            if (arcanumAI) // Set to true by Overcharged/Prismatic Arcanum
            {
				if(timer > 0)
                {
					projectile.aiStyle = 0;
					projectile.extraUpdates = 1;
					timer--;
                }
                else
                {
					projectile.extraUpdates = 0;
					projectile.aiStyle = 9;
				}
			}
			return true;
        }

		// Protects rainbow rod projectiles from exploding on tiles during the 'stall' phase
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (arcanumAI && projectile.aiStyle == 0 && projectile.type == ProjectileID.RainbowRodBullet)
            {
				return false;
            }
            return base.OnTileCollide(projectile, oldVelocity);
        }
    }
}