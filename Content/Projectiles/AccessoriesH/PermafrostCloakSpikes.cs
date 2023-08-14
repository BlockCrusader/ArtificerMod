using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AccessoriesH
{

	public class PermafrostCloakSpikes : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.timeLeft = 100;
			DrawOffsetX = -2;
		}

        public override void AI()
        {
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2; 

			// Sets appearance based on ai
			Projectile.frame = (int)Projectile.ai[0];

			Projectile.ai[1] += 1f;

			if (Projectile.ai[1] < 30f)
			{
				Projectile.velocity *= 0.95f;
			}
			else if(Projectile.ai[1] == 30) 
            {
				// homing
				float maxDetectRadius = 300f; 
				float projSpeed = 20f;

				NPC closestNPC = FindClosestNPC(maxDetectRadius);
				if (closestNPC != null)
				{
					Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                    if (Projectile.ai[2] != 1)
                    {
						Projectile.ai[2] = 1;
						Projectile.timeLeft = 200;
                    }
				}
			}
		}

		public NPC FindClosestNPC(float maxDetectDistance)
		{
			NPC closestNPC = null;

			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];
				if (target.CanBeChasedBy())
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance && Collision.CanHit(Projectile, target))
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}

        public override void Kill(int timeLeft)
        {
			SoundEngine.PlaySound(SoundID.Shatter, Projectile.Center);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SnowflakeIce, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 1.1f;
			}
		}
    }
}