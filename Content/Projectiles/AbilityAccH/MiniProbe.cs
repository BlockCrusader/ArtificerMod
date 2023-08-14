using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{

	public class MiniProbe : ModProjectile
	{

		public override void SetDefaults()
		{
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.netImportant = true;
			Projectile.width = 16; 
			Projectile.height = 16;
			Projectile.friendly = false; 
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.timeLeft = 1500;
			DrawOffsetX = -4;
			DrawOriginOffsetY = -4;
		}

		readonly float orbitRadius = 30f; 
		readonly float orbitSpeed = 1f;
        readonly float orbitDirection = 1;

		int attackCooldown = Main.rand.Next(60, 181);
		NPC target = null;

        public override void OnSpawn(IEntitySource source)
        {
			// A pre-set offset in the spin radius, based on ai0
			switch (Projectile.ai[0])
			{
				case 0:
					Projectile.ai[1] = 0f;
					break;
				case 1:
					Projectile.ai[1] = 40f;
					break;
				case 2:
					Projectile.ai[1] = 80f;
					break;
				default:
					Projectile.Kill();
					return;
			}

			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TheDestroyer, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 1f;
			}
		}

        public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			bool getArtificer = Main.player[Projectile.owner].TryGetModPlayer(out ArtificerPlayer artificer);
			if (!getArtificer || (!artificer.probePack && !artificer.fCharm) || !owner.active || owner.dead)
			{
				Projectile.Kill();
				return;
			}

			DefineOrbitPos(owner, out Vector2 orbitPos);

			Projectile.Center = orbitPos;

			attackCooldown--;
			float maxDetectRadius = 800f; 
			if(attackCooldown <= 0 && Main.myPlayer == Projectile.owner)
            {
				target = GetTarget(maxDetectRadius, Projectile.ai[0]);
				if (target != null)
				{
					attackCooldown = Main.rand.Next(60, 181);
					SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);
					Vector2 projVelocity = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 20f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, projVelocity,
						ModContent.ProjectileType<ProbeLaser>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
				}
                else
                {
					attackCooldown = 60;
				}
			}

			// Rotation
			if(target != null)
            {
				Projectile.rotation = (target.Center - Projectile.Center).ToRotation() + MathHelper.Pi;
			}
            else
            {
				switch (Projectile.ai[0])
				{
					case 1:
						Projectile.rotation = (owner.Center - Projectile.Center).ToRotation();
						break;
					case 2:
						if(Main.myPlayer == Projectile.owner)
                        {
							Projectile.rotation = (Main.MouseWorld - Projectile.Center).ToRotation() + MathHelper.Pi;
						}
                        else
                        {
							Projectile.rotation = (owner.Center - Projectile.Center).ToRotation() + MathHelper.Pi;
						}
						break;
					default:
						Projectile.rotation = (owner.Center - Projectile.Center).ToRotation() + MathHelper.Pi;
						break;
				}
			}
		}

		private void DefineOrbitPos(Player owner, out Vector2 orbitPos)
		{
			Projectile.ai[1] += 1f;
			
			Vector2 projOffset = new Vector2(1f).RotatedBy((float)Math.PI * orbitSpeed * (Projectile.ai[1] / 60f) * orbitDirection);

			orbitPos = owner.Center + projOffset * orbitRadius;
		}

		public NPC GetTarget(float maxDetectDistance, float targetPrio)
		{
			NPC target = null;

			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
			float recordClosest = float.MaxValue;
			float recordHP = 0;

			Vector2 ownerPos = Main.player[Projectile.owner].Center;
			Vector2 cursorPos = Main.MouseWorld;

			switch (targetPrio) // Switch for the 3 targeting styles. All require enemies to be in-range
			{
				case 0: // Closest to owner
					for (int k = 0; k < Main.maxNPCs; k++)
					{
						NPC npc = Main.npc[k];
						if (npc.CanBeChasedBy() && Collision.CanHit(Projectile, npc))
						{
							float sqrDistanceToTarget = Vector2.DistanceSquared(npc.Center, ownerPos);
							if (sqrDistanceToTarget < sqrMaxDetectDistance)
							{
								if (sqrDistanceToTarget < recordClosest)
                                {
									target = npc;
									recordClosest = sqrDistanceToTarget;
								}
								else if (sqrDistanceToTarget == recordClosest && Main.rand.NextBool())
								{
									target = npc;
									recordClosest = sqrDistanceToTarget;
								}
							}
						}
					}
					break;
				case 1: // Most HP
					for (int k = 0; k < Main.maxNPCs; k++)
					{
						NPC npc = Main.npc[k];
						if (npc.CanBeChasedBy() && Collision.CanHit(Projectile, npc))
						{
							float sqrDistanceToTarget = Vector2.DistanceSquared(npc.Center, ownerPos);
							if (sqrDistanceToTarget < sqrMaxDetectDistance)
							{
								if (npc.life > recordHP)
								{
									target = npc;
									recordHP = npc.life;
								}
								else if (npc.life == recordHP && Main.rand.NextBool())
								{
									target = npc;
									recordHP = npc.life;
								}
							}
						}
					}
					break;
				case 2: // Closest to cursor
					for (int k = 0; k < Main.maxNPCs; k++)
					{
						NPC npc = Main.npc[k];
						if (npc.CanBeChasedBy() && Collision.CanHit(Projectile, npc))
						{
							float sqrDistanceToTarget = Vector2.DistanceSquared(npc.Center, ownerPos);
							float cursorDistToTarget = Vector2.DistanceSquared(npc.Center, cursorPos);
							if (sqrDistanceToTarget < sqrMaxDetectDistance)
							{
								if (cursorDistToTarget < recordClosest)
								{
									target = npc;
									recordClosest = cursorDistToTarget;
								}
								else if (cursorDistToTarget == recordClosest && Main.rand.NextBool())
								{
									target = npc;
									recordClosest = cursorDistToTarget;
								}
							}
						}
					}
					break;
				default:
					return null;
			}
			return target;
		}

        public override bool? CanDamage()
        {
            return false;
        }

        public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.Center);
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default(Color), 0.75f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 1f;
			}
		}
	}
}