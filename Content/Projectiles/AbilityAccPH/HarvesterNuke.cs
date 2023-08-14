using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccPH
{
	public class HarvesterNuke : ModProjectile
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

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			// This makes it so that attacks always hit enemies away from the player
			modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			float velocityScalar = Projectile.ai[0] / 70f;
			velocityScalar = Math.Clamp(velocityScalar, 0.65f, 2f);

			Vector2 extraVelocity = target.Center - Main.player[Projectile.owner].Center;
			extraVelocity = extraVelocity.SafeNormalize(Vector2.Zero) * (16f * velocityScalar);

			int dustNum = (int)(Projectile.ai[0] / 5f + Main.rand.Next(-3, 4));
			dustNum = Math.Clamp(dustNum, 5, 40);
			for (int j = 0; j < dustNum; j++)
			{
				int dust2 = Dust.NewDust(Projectile.Center, 0, 0, DustID.DungeonSpirit, 0f, 0f, 0, default, 1.25f);
				Main.dust[dust2].velocity *= 1.6f;
				Main.dust[dust2].velocity.Y -= 1f;
				Main.dust[dust2].velocity += extraVelocity;
				Main.dust[dust2].noGravity = true;
			}
		}
    }
}