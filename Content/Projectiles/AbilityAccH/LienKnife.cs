using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	public class LienKnife : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.VampireKnife);
			Projectile.aiStyle = -1;
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1;
			Projectile.alpha = 0;
			Projectile.friendly = false;
			DrawOffsetX = 6;
		}

		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 5 && Projectile.ai[1] < 1)
            {
				Projectile.friendly = true;
				
				// Homing
				float maxDetectionRadius = 300f;
				NPC closestNPC = FindClosestNPC(maxDetectionRadius);
				if (closestNPC != null)
				{
					Vector2 vectToTarget = (closestNPC.Center - Projectile.Center).SafeNormalize(-Vector2.UnitY) * 20f;
					Projectile.velocity = Vector2.Lerp(Projectile.velocity, vectToTarget, 0.05f);
				}
			}
			if (Projectile.ai[0] >= 40f)
			{
				Projectile.ai[1] = 1;
				Projectile.alpha += 20;
				Projectile.damage = (int)((double)Projectile.damage * 0.8);
				Projectile.knockBack = (int)((double)Projectile.knockBack * 0.8);
				if (Projectile.alpha >= 255)
				{
					impactDust = false;
					Projectile.Kill();
				}
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (target.lifeMax > 5 && target.canGhostHeal)
			{
				Lifesteal(hit.Damage, target);
			}
		}

		public void Lifesteal(int damage, NPC target)
		{
			float lifesteal = 3f;
			if ((int)lifesteal != 0 && !(Main.player[Main.myPlayer].lifeSteal <= 0f) && Projectile.owner == Main.myPlayer)
			{
				Player owner = Main.player[Projectile.owner];
				int healTargetID = Projectile.owner; // By default, heal the user

				bool fullHP = owner.statLife >= owner.statLifeMax2;

                if (fullHP) // Try to heal another player if the owner has full HP
                {
					float hpTracker = 0f;
					for (int i = 0; i < 255; i++)
					{
						if (Main.player[i].active && !Main.player[i].dead && 
							((!Main.player[Projectile.owner].hostile && !Main.player[i].hostile) 
							|| Main.player[Projectile.owner].team == Main.player[i].team) 
							&& 
							Vector2.Distance(Main.player[i].Center, Projectile.Center) < 1200f
							&& (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > hpTracker)
						{
							hpTracker = Main.player[i].statLifeMax2 - Main.player[i].statLife;
							healTargetID = i;
						}
					}
					if(healTargetID != Projectile.owner)
                    {
						lifesteal = Main.rand.NextBool() ? 2f : 1f; // Reduces lifesteal when healing allies
					}
				}
				
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Projectile.velocity * 0f, ModContent.ProjectileType<LienLifesteal>(), 0, 0f, Projectile.owner, healTargetID, lifesteal);
			}
		}

		public NPC FindClosestNPC(float maxDetectionDistance)
		{
			NPC closestNPC = null;
			float sqrMaxDetectDistance = maxDetectionDistance * maxDetectionDistance;
			for (int i = 0; i < 200; i++)
			{
				NPC target = Main.npc[i];
				if (target.CanBeChasedBy())
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, base.Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}
			return closestNPC;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
			width = height = 6;
            return true;
        }

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f);
		}

		bool impactDust = true;
		public override void Kill(int timeLeft)
        {
			if (impactDust)
			{
				for (int i = 0; i < 4; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Phantasmal, 0f, 0f, 100, default(Color), 0.8f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 1.2f;
					Main.dust[dustIndex].velocity -= Projectile.oldVelocity * 0.3f;
				}
			}
		}
    }
}