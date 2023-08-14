using ArtificerMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AccessoriesH
{

	public class BlueShell : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 5;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
			Projectile.extraUpdates = 1;
			Projectile.alpha = 255;

			DrawOffsetX = -5;
			DrawOriginOffsetY = -5;
		}

		readonly float orbitRadius = 60f; // How far from the player the projectile will orbit the user
        readonly float orbitSpeed = 2f;
        readonly float orbitDirection = 1;
		bool killFirstPlace = false;

        public override void OnSpawn(IEntitySource source)
        {
			Projectile.ai[1] = Main.rand.Next(1, 61);
		}

        public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			ArtificerPlayer artificer = owner.GetModPlayer<ArtificerPlayer>();

			if (!owner.active || owner.dead || artificer.pShellEquip == null || (!killFirstPlace && owner.statLife > owner.statLifeMax2 / 2))
			{
				Projectile.Kill();
				return;
			}
            if (!killFirstPlace)
            {
				Projectile.timeLeft = 5; 
			}

			Projectile.rotation -= 0.1f;
			Lighting.AddLight(Projectile.Center, 0.1f, 0.2f, 0.4f);

			if(Main.rand.NextBool(2))
            {
				int dustIndex = Dust.NewDust(Projectile.position, 8, 8, DustID.IceTorch);
				Main.dust[dustIndex].noGravity = true;
			}

			if(Projectile.alpha > 0)
            {
				Projectile.alpha -= 10;
            }
			if(Projectile.alpha < 0)
            {
				Projectile.alpha = 0;
            }

            if (Projectile.ai[2] > 0f)
            {
				Projectile.ai[2]--;
            }
            if (artificer.throwBlueShell || artificer.throwBlueShellFlag)
            {
				Projectile.ai[2] = 100;
				artificer.blueShellCooldown = 50;
			}

			Projectile.ai[0]++;
			DefineOrbitPos(owner, out Vector2 orbitPos);
            if (!killFirstPlace)
            {
				Projectile.Center = orbitPos;
			}
			if((Projectile.ai[2] > 0 || killFirstPlace) && Projectile.ai[0] >= 30)
            {
				float maxDetectRadius = 500f; 
				float projSpeed = 15f; 

				NPC firstPlace = FindTarget(maxDetectRadius);
				if (firstPlace != null)
				{
					Projectile.velocity = (firstPlace.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
					if (killFirstPlace == false)
					{
						killFirstPlace = true;
						Projectile.timeLeft = 200;
						Projectile.tileCollide = true;
						Projectile.penetrate = 1;
						Projectile.damage *= 2;

						Projectile.ResetLocalNPCHitImmunity();
						Projectile.localNPCHitCooldown = -1;
					}
				}
			}

		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

			if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}

			return false;
		}

		public NPC FindTarget(float maxDetectDistance)
		{
			NPC target = null;
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
			float mostHP = 5;
			float closestNPC = sqrMaxDetectDistance;
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy())
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(npc.Center, Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance && Collision.CanHit(Projectile, npc)) 
					{
						if(npc.life > mostHP) // Aim at the NPC with the most health
                        {
							target = npc;
							mostHP = npc.life;
							closestNPC = sqrDistanceToTarget;
						}
						else if(npc.life == mostHP)
                        {
							if(sqrDistanceToTarget < closestNPC)
                            {
								target = npc;
								mostHP = npc.life;
								closestNPC = sqrDistanceToTarget;
							}
                        }
					}
				}
			}

			return target;
		}

		private void DefineOrbitPos(Player owner, out Vector2 orbitPos)
		{
            Projectile.ai[1] += 1f;

			Vector2 offsetFromPlayer = new Vector2(1f).RotatedBy((float)Math.PI * orbitSpeed * (Projectile.ai[1] / 60f) * orbitDirection);

			orbitPos = owner.MountedCenter + offsetFromPlayer * orbitRadius;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
		}

        public override void Kill(int timeLeft)
        {
            if (killFirstPlace)
            {
				Projectile.position = Projectile.Center;
				Projectile.width = (Projectile.height = 160);
				Projectile.Center = Projectile.position;
				Projectile.maxPenetrate = -1;
				Projectile.penetrate = -1;
				Projectile.Damage();
				SoundEngine.PlaySound(SoundID.Item62, Projectile.position);

				// Dust effects adapted from mini nuke
				for (int i = 0; i < 30; i++)
				{
					Dust dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, 0f, 0f, 100);
					dust1.noGravity = true;
					dust1.fadeIn = 1.4f;
					dust1.velocity = (dust1.position - Projectile.Center).SafeNormalize(Vector2.Zero);
					dust1.velocity *= 5.5f + (float)Main.rand.Next(61) * 0.1f;
					dust1.velocity.Y -= 3f * 0.5f;

					Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, 0f, 0f, 100);
					dust2.velocity = (dust2.position - Projectile.Center).SafeNormalize(Vector2.Zero);
					dust2.velocity.Y -= 3f * 0.25f;
					dust2.velocity *= 1.5f + (float)Main.rand.Next(5) * 0.1f;
					dust2.fadeIn = 0f;
					dust2.scale = 0.6f;

					Dust dust3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, 0f, 0f, 100, default(Color), 1.5f);
					dust3.noGravity = i % 2 == 0;
					dust3.velocity = (dust3.position - Projectile.Center).SafeNormalize(Vector2.Zero);
					dust3.velocity *= 3f + (float)Main.rand.Next(21) * 0.2f;
					dust3.velocity.Y -= 3f * 0.5f;
					dust3.fadeIn = 1.2f;
					if (!dust3.noGravity)
					{
						dust3.scale = 0.4f;
						dust3.fadeIn = 0f;
						continue;
					}
					dust3.velocity *= 2f + (float)Main.rand.Next(5) * 0.2f;
					dust3.velocity.Y -= 3f * 0.5f;
				}
				for (int j = 1; j <= 3; j++)
				{
					float rot = (float)Math.PI * 2f * Main.rand.NextFloat();
					for (float k = 0f; k < 1f; k += 1f / 11f)
					{
						Vector2 spinningpoint = ((float)Math.PI * 2f * k + rot).ToRotationVector2();
						spinningpoint *= new Vector2(1f, 0.4f);
						spinningpoint = spinningpoint.RotatedBy(-1f * (float)Math.PI);
						Vector2 offset = -1f * ((float)Math.PI / 2f).ToRotationVector2();
						Vector2 dustPos = Projectile.Center;

						Dust dust4 = Dust.NewDustPerfect(dustPos, DustID.IceTorch, spinningpoint);
						dust4.fadeIn = 1.8f;
						dust4.noGravity = true;
						dust4.velocity *= (float)j * (Main.rand.NextFloat() * 2f + 0.2f);
						dust4.velocity += offset * 0.8f * j;
						dust4.velocity *= 2f;
					}
				}
				Projectile.Resize(10, 10);
			}
            else
            {
				for (int i = 0; i > 20; i++)
				{
					int dustIndex = Dust.NewDust(Projectile.position, 8, 8, DustID.IceTorch);
					Main.dust[dustIndex].noGravity = true;
				}
			}

			Player owner = Main.player[Projectile.owner];
			if (!owner.TryGetModPlayer(out ArtificerPlayer artificer))
			{
				return;
			}
			if (killFirstPlace)
			{
				artificer.blueShellCooldown = 100;
			}
            else
            {
				artificer.blueShellCooldown = 50;
			}
		}
    }
}