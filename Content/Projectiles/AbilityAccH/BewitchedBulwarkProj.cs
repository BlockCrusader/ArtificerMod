using ArtificerMod.Common;
using ArtificerMod.Content.Buffs.AbilityAccH;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class BewitchedBulwarkProj : ModProjectile
	{

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.tileCollide = false;
			Projectile.friendly = false; // Doesn't inflict contact damage
			Projectile.penetrate = -1;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.netImportant = true; 
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f);
		}

		int hoverSpeed = 1;
		int hoverTimer = 1;
		int attackCooldown = 30;
		bool hoverStart = false;
		bool hoverUp = true;
		bool spawnDust = false;
		Vector2 initalPos;
		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			if (!owner.GetModPlayer<ArtificerPlayer>().bBulwark && !owner.GetModPlayer<ArtificerPlayer>().fCharm)
			{
				Projectile.Kill();
			}

			if (spawnDust == false)
			{
				for (int i = 0; i < 20; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 30, 30, DustID.Shadowflame, 0f, 0f, 0, default(Color), 1.2f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 2f;
				}
				spawnDust = true;
			}

			Projectile.velocity.X *= 0f;
			hoverTimer++;
			if (hoverTimer >= hoverSpeed)
			{
				hoverTimer = 0;
				if (hoverStart == false)
				{
					initalPos = Projectile.Center; 
					Projectile.velocity.Y += 0.002f;
					hoverStart = true;
				}
				else if (hoverUp == true)
				{
					Projectile.velocity.Y += 0.002f;
					if (Projectile.velocity.Y > 0.125f)
					{
						hoverUp = false;
					}
				}
				else if (hoverUp == false)
				{
					Projectile.velocity.Y -= 0.002f;
					if (Projectile.velocity.Y < -0.125f)
					{
						hoverUp = true;
					}
				}
			}

			// Dust, representing the aura range
			if (Main.rand.NextBool(2))
			{
				for (int i = 0; i < 25; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					Dust d = Dust.NewDustPerfect(initalPos + speed * 250, DustID.Shadowflame, speed * 0, Scale: 0.9f);
					d.noGravity = true;
				}
			}

			attackCooldown--;
			if (attackCooldown <= 0)
			{
				attackCooldown = 30;

				AuraAttack();

				for (int i = 0; i < 80; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					Dust d = Dust.NewDustPerfect(initalPos + speed * 250, DustID.Shadowflame, speed * -10f, Scale: 0.9f);
					d.noGravity = true;
				}
			}

			float maxDetectDistance = 250;
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
			for (int k = 0; k < Main.maxPlayers; k++)
			{
				float sqrDistanceToTarget = Vector2.DistanceSquared(Main.player[k].Center, Projectile.Center);
				if (sqrDistanceToTarget < sqrMaxDetectDistance)
				{
					if (Main.player[k].active && !Main.player[k].dead
						&& ((!Main.player[Projectile.owner].hostile && !Main.player[k].hostile) || Main.player[Projectile.owner].team == Main.player[k].team))
					{
						Main.player[k].AddBuff(ModContent.BuffType<BewitchedBulwarkBuff>(), 2);
					}
				}
			}
		}

		public void AuraAttack()
		{
			float maxDetectDistance = 250;
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			List<NPC> targets = new List<NPC>();
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];
				if (target.CanBeChasedBy())
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, initalPos);
					if (sqrDistanceToTarget < sqrMaxDetectDistance && Main.myPlayer == Projectile.owner)
					{
						targets.Add(target);
					}
				}
			}

			// To prevent excessive damage, the length of the list of targets is used to set a maximum amount of damage and divide it amongst the enemies
			int auraDamage = Projectile.damage;
			float mercyAP = 0;
			if (targets.Count > 5) // With 1-5 enemies, all targets take full damage. Otherwise, this max of x5 base damage is 'divided' amongst targets, as performed here
			{
				auraDamage *= 5;
				auraDamage = (int)(auraDamage / targets.Count);

				// As a 'mercy rule', damage cannot be below 5, and gains up to 15 armor penetration
				if (auraDamage < 20)
				{
					mercyAP = 20 - auraDamage;
				}
				if (auraDamage < 5)
				{
					mercyAP = 15;
					auraDamage = 5;
				}
			}

			for (int j = 0; j < targets.Count; j++)
			{
				NPC target = targets[j];
				// Spawns an invisible projectile on the enemy to damage it
				// TODO: Directly damage the enemy instead of this workaround that is probably uneeded
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(target.Center.X, target.Center.Y), Projectile.velocity * 0f, ModContent.ProjectileType<BulwarkAura>(), auraDamage, Projectile.knockBack, Projectile.owner, mercyAP, Projectile.whoAmI);
			}
		}

		public override bool? CanDamage()
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.NPCDeath6, Projectile.Center);

			// Spawns dust
			for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 30, 30, DustID.Shadowflame, 0f, 0f, 0, default(Color), 1.1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 2f;
			}
		}
	}
}