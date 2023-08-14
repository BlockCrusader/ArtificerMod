using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class FirstPrismProj : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.tileCollide = false;
			Projectile.friendly = false; // Doesn't inflict contact damage
			Projectile.penetrate = -1;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 750;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.netImportant = true;
			DrawOffsetX = -3;
			DrawOriginOffsetY = -12;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 200) * (1f - (float)Projectile.alpha / 255f);
		}

		List<NPC> oldTargets = new List<NPC>();

        public override void OnSpawn(IEntitySource source)
        {
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 20, 20, Main.rand.Next(68, 71), 0f, 0f, 0, default(Color), 1.1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 2f;
			}
		}

        public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			Projectile.Center = new Vector2(owner.Center.X, (owner.Center.Y - 55f));

			if (!owner.GetModPlayer<ArtificerPlayer>().prism && !owner.GetModPlayer<ArtificerPlayer>().fCharm)
			{
				Projectile.Kill();
			}

			Projectile.localAI[0]++;
			if (Projectile.localAI[0] >= 60)
			{
				Projectile.localAI[0] = 0;
				{
					if (Main.myPlayer == Projectile.owner)
					{
						float projSpeed = 10f;
						NPC target = FindClosestNPC(750f, oldTargets);
						if(target != null)
                        {
							SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);
							oldTargets.Add(target);
							Vector2 projVelocity = (target.Center - new Vector2(Projectile.Center.X, Projectile.Center.Y)).SafeNormalize(Vector2.Zero) * projSpeed;
							int beam = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, projVelocity, ModContent.ProjectileType<FirstPrismBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
							
							// Randomly set a damage type
							int beamType = Main.rand.Next(4);
							if (beamType == 0)
							{
								Main.projectile[beam].DamageType = DamageClass.Melee;
							}
							else if(beamType == 1)
                            {
								Main.projectile[beam].DamageType = DamageClass.Ranged;
							}
							else if(beamType == 3)
                            {
								Main.projectile[beam].DamageType = DamageClass.Magic;
							}
                            else
                            {
								Main.projectile[beam].DamageType = DamageClass.Summon;
								Main.projectile[beam].originalDamage = Projectile.damage;
							}
						}
					}
				}
			}
		}

		public override bool? CanCutTiles()
		{
			return false;
		}

		public override bool? CanDamage()
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Shatter, Projectile.Center);
			for (int i = 0; i < 15; i++) 
			{
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Main.rand.Next(68, 71), 0f, 0f, 0, default, 1.05f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 1.5f;
			}
		}

		public NPC FindClosestNPC(float maxDetectionDistance, List<NPC> lowPriority)
		{
			NPC closestNPC = null;
			float sqrMaxDetectDistance = maxDetectionDistance * maxDetectionDistance;
			for (int i = 0; i < 200; i++)
			{
				NPC target = Main.npc[i];
				if (target.CanBeChasedBy())
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
                        if (!lowPriority.Contains(target)) // Exclude previous targets for now
                        {
							sqrMaxDetectDistance = sqrDistanceToTarget;
							closestNPC = target;
						}
					}
				}
			}
			if(closestNPC == null) // If there's still no (new) target, then it's ok to target old ones
            {
				for (int i = 0; i < lowPriority.Count; i++)
				{
					NPC target = lowPriority[i];
					if (target.CanBeChasedBy())
					{
						float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
						if (sqrDistanceToTarget < sqrMaxDetectDistance)
						{
							sqrMaxDetectDistance = sqrDistanceToTarget;
							closestNPC = target;
						}
					}
				}
			}
			return closestNPC;
		}
	}
}