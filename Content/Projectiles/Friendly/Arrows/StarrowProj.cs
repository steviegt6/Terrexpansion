using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Projectiles.Friendly.Arrows
{
    public class StarrowProj : BaseProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Starrow");

        public override void SetDefaults()
        {
            projectile.arrow = true;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.DamageType = DamageClass.Ranged;
            projectile.timeLeft = 600;
            projectile.light = 0.9f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            float offsetX = projectile.position.X + Main.rand.Next(-400, 400);
            float offsetY = projectile.position.Y - Main.rand.Next(600, 900);
            float velocityX = projectile.position.X + (projectile.width / 2) - offsetX;
            float velocityY = projectile.position.Y + (projectile.height / 2) - offsetY;
            float sqrt = (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
            sqrt = 22 / sqrt;
            velocityX *= sqrt;
            velocityY *= sqrt;

            Projectile.NewProjectile(new Vector2(offsetX, offsetY), new Vector2(velocityX, velocityY), ProjectileID.Starfury, 10, 4f, Main.myPlayer);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            float offsetX = projectile.position.X + Main.rand.Next(-400, 400);
            float offsetY = projectile.position.Y - Main.rand.Next(600, 900);
            float velocityX = projectile.position.X + (projectile.width / 2) - offsetX;
            float velocityY = projectile.position.Y + (projectile.height / 2) - offsetY;
            float sqrt = (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
            sqrt = 22 / sqrt;
            velocityX *= sqrt;
            velocityY *= sqrt;

            Projectile.NewProjectile(new Vector2(offsetX, offsetY), new Vector2(velocityX, velocityY), ProjectileID.Starfury, 10, 4f, Main.myPlayer);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) => Lighting.AddLight((int)((projectile.position.X + (projectile.width / 2)) / 16f), (int)((projectile.position.Y + (projectile.height / 2)) / 16f), projectile.light * 0.9f, projectile.light * 0.8f, projectile.light * 0.1f);

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, lightColor.A - projectile.alpha);

        public override void AI()
        {
            Vector2 screenVector = new Vector2(Main.screenWidth, Main.screenHeight);

            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 80 + Main.rand.Next(20);
                SoundEngine.PlaySound(SoundID.Item9, projectile.position);
            }

            if (projectile.Hitbox.Intersects(Utils.CenteredRectangle(Main.screenPosition + screenVector / 2f, screenVector + new Vector2(400f))) && Main.rand.NextBool(6))
            {
                Gore.NewGore(projectile.position, projectile.velocity * 0.2f, Utils.SelectRandom(Main.rand, 16, 17, 17, 17));
            }

            projectile.light = 0.9f;

            if (Main.rand.NextBool(10))
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 150, Scale: 1.2f);
            }
        }

        public override void Kill(int timeLeft)
        {
            Vector2 screenVector = new Vector2(Main.screenWidth, Main.screenHeight);

            SoundEngine.PlaySound(SoundID.Item10, projectile.position);

            for (int i = 0; i < 7; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, Scale: 0.8f);
            }

            for (float l = 0f; l < 1f; l += 0.125f)
            {
                Dust.NewDustPerfect(projectile.Center, 278, Vector2.UnitY.RotatedBy(l * ((float)Math.PI * 2f) + Main.rand.NextFloat() * 0.5f) * (4f + Main.rand.NextFloat() * 4f), 150, Color.CornflowerBlue).noGravity = true;
            }

            for (float j = 0f; j < 1f; j += 0.25f)
            {
                Dust.NewDustPerfect(projectile.Center, 278, Vector2.UnitY.RotatedBy(j * ((float)Math.PI * 2f) + Main.rand.NextFloat() * 0.5f) * (2f + Main.rand.NextFloat() * 3f), 150, Color.Gold).noGravity = true;
            }

            if (projectile.Hitbox.Intersects(Utils.CenteredRectangle(Main.screenPosition + screenVector / 2f, screenVector + new Vector2(400f))))
            {
                for (int n = 0; n < 7; n++)
                {
                    Gore.NewGore(projectile.position, Main.rand.NextVector2CircularEdge(0.5f, 0.5f) * projectile.velocity.Length(), Utils.SelectRandom(Main.rand, 16, 17, 17, 17, 17, 17, 17, 17));
                }
            }
        }
    }
}