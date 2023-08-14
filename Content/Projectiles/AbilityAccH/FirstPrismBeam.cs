using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class FirstPrismBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}
	

		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.extraUpdates = 24;
			Projectile.timeLeft = 75; 
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 0)
            {
				Projectile.timeLeft = 5;
				Projectile.ai[0] = 1;
			}
			
        }

        public override void AI()
		{
			Color fxColor = Color.White;
			if(Projectile.DamageType == DamageClass.Melee)
            {
				fxColor = new Color(255, 160, 5);
			}
			else if (Projectile.DamageType == DamageClass.Ranged)
			{
				fxColor = new Color(35, 255, 150);
			}
			else if (Projectile.DamageType == DamageClass.Magic)
			{
				fxColor = new Color(255, 130, 230);
			}
			else if (Projectile.DamageType == DamageClass.Summon)
			{
				fxColor = new Color(125, 230, 255);
			}

			SpawnParticleFX(fxColor);
		}

		// Achieves a particle effect like the one used by the Chlorophyte Armor set bonus's projectiles
		public void SpawnParticleFX(Color color)
		{
			int lifespan = 30;
			PrettySparkleParticle prettySparkleParticle = new PrettySparkleParticle();
			prettySparkleParticle.ColorTint = color;
			prettySparkleParticle.LocalPosition = Projectile.Center;
			prettySparkleParticle.Rotation = Projectile.velocity.ToRotation();
			prettySparkleParticle.Scale = new Vector2(4f, 1f) * 0.6f;
			prettySparkleParticle.FadeInNormalizedTime = 5E-06f;
			prettySparkleParticle.FadeOutNormalizedTime = 1f;
			prettySparkleParticle.TimeToLive = lifespan;
			prettySparkleParticle.FadeOutEnd = lifespan;
			prettySparkleParticle.FadeInEnd = lifespan / 2;
			prettySparkleParticle.FadeOutStart = lifespan / 2;
			prettySparkleParticle.AdditiveAmount = 0.5f;
			prettySparkleParticle.Velocity = Projectile.velocity;
			prettySparkleParticle.LocalPosition -= prettySparkleParticle.Velocity * 4f;
			prettySparkleParticle.DrawVerticalAxis = false;

			Main.ParticleSystem_World_OverPlayers.Add(prettySparkleParticle);
		}


	}
}