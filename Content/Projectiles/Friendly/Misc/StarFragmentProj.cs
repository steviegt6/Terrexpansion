using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Items.Materials;

namespace Terrexpansion.Content.Projectiles.Friendly.Misc
{
    public class StarFragmentProj : BaseProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Star Fragment");

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) => Lighting.AddLight((int)((projectile.position.X + (projectile.width / 2)) / 16f), (int)((projectile.position.Y + (projectile.height / 2)) / 16f), projectile.light * 0.9f, projectile.light * 0.8f, projectile.light * 0.1f);

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, lightColor.A - projectile.alpha);

        public override void AI()
        {
            if (Main.dayTime && projectile.damage == 100)
                projectile.Kill();

            if (projectile.ai[1] == 0f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.ai[1] = 1f;
                projectile.netUpdate = true;
            }

            if (projectile.ai[1] != 0f)
                projectile.tileCollide = true;

            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 40 + Main.rand.Next(30);

                SoundEngine.PlaySound(SoundID.Item9, projectile.position);
            }

            if (projectile.localAI[0] == 0f)
                projectile.localAI[0] = 1f;

            projectile.alpha += (int)(25f * projectile.localAI[0]);

            if (projectile.alpha > 200)
            {
                projectile.alpha = 200;
                projectile.localAI[0] = 1f;
            }

            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
                projectile.localAI[0] = 1f;
            }

            projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f * projectile.direction;

            Vector2 screenVector = new Vector2(Main.screenWidth, Main.screenHeight);

            if (projectile.Hitbox.Intersects(Utils.CenteredRectangle(Main.screenPosition + screenVector / 2f, screenVector + new Vector2(400f))) && Main.rand.NextBool(6))
                Gore.NewGore(projectile.position, projectile.velocity * 0.2f, Utils.SelectRandom(Main.rand, 16, 17, 17, 17));

            projectile.light = 0.9f;

            if (Main.rand.NextBool(20))
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 150, Scale: 1.2f);
        }

        public override void Kill(int timeLeft)
        {
            Vector2 screenVector = new Vector2(Main.screenWidth, Main.screenHeight);

            SoundEngine.PlaySound(SoundID.Item10, projectile.position);

            for (int i = 0; i < 7; i++)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, Scale: 0.8f);

            for (float l = 0f; l < 1f; l += 0.125f)
                Dust.NewDustPerfect(projectile.Center, 278, Vector2.UnitY.RotatedBy(l * ((float)Math.PI * 2f) + Main.rand.NextFloat() * 0.5f) * (4f + Main.rand.NextFloat() * 4f), 150, Color.CornflowerBlue).noGravity = true;

            for (float j = 0f; j < 1f; j += 0.25f)
                Dust.NewDustPerfect(projectile.Center, 278, Vector2.UnitY.RotatedBy(j * ((float)Math.PI * 2f) + Main.rand.NextFloat() * 0.5f) * (2f + Main.rand.NextFloat() * 3f), 150, Color.Gold).noGravity = true;

            if (projectile.Hitbox.Intersects(Utils.CenteredRectangle(Main.screenPosition + screenVector / 2f, screenVector + new Vector2(400f))))
                for (int n = 0; n < 7; n++)
                    Gore.NewGore(projectile.position, Main.rand.NextVector2CircularEdge(0.5f, 0.5f) * projectile.velocity.Length(), Utils.SelectRandom(Main.rand, 16, 17, 17, 17, 17, 17, 17, 17));

            if (projectile.damage == 100)
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ModContent.ItemType<StarFragment>());
        }
    }
}