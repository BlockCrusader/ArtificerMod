using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArtificerMod.Content.Projectiles.AbilityAccH
{
	class PlasmaCrossbones : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
		{
            Projectile.ArmorPenetration = 25;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            DrawOffsetX = -3;
            DrawOriginOffsetY = -3;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.rotation += 0.15f * (float)Projectile.direction;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f);
        }

        // Tile bouncing
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.ai[0] < 2)
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
                Projectile.ai[0]++;
                return false;
            }
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Adapted from Bone Glove's projectiles
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 drawOrigin = rect.Size() / 2f;

            for (int i = 1; i < 10; i++)
            {
                if (i >= Projectile.oldPos.Length)
                {
                    continue;
                }
                Color drawColor = new Color(255, 255, 255, 255) * (1f - i / 10f);
                SpriteEffects fx = ((Projectile.oldSpriteDirection[i] == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

                if (Projectile.oldPos[i] == Vector2.Zero)
                {
                    continue;
                }

                Vector2 drawPos = Projectile.oldPos[i] + Vector2.Zero + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, drawPos, rect, drawColor, Projectile.oldRot[i] +
                    Projectile.rotation * 0.2f * (float)(i - 1)
                    * (float)(-((System.Enum)fx).HasFlag((System.Enum)SpriteEffects.FlipHorizontally).ToDirectionInt()),
                    drawOrigin, MathHelper.Lerp(Projectile.scale, 0.7f, (float)i / 15f), fx);

                Color color2 = Projectile.GetAlpha(default);
                Main.EntitySpriteDraw(texture, Projectile.Center + Vector2.Zero - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    rect, color2, Projectile.rotation, drawOrigin, Projectile.scale, fx);
            }

            return true;
        }
    }
}