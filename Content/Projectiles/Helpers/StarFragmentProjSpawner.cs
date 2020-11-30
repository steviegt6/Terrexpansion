using System;
using Terraria;
using Terraria.ModLoader;
using Terrexpansion.Content.Projectiles.Friendly.Misc;

namespace Terrexpansion.Content.Projectiles.Helpers
{
    public class StarFragmentProjSpawner : BaseProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Star Fragment");

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            if (Main.dayTime)
            {
                projectile.Kill();
                return;
            }

            projectile.ai[0] += Main.dayRate;

            if (projectile.owner == Main.myPlayer && projectile.ai[0] >= 180f)
            {
                if (projectile.ai[1] > -1f)
                {
                    projectile.velocity.X *= 0.35f;

                    if (projectile.Center.X < Main.player[(int)projectile.ai[1]].Center.X)
                        projectile.velocity.X = Math.Abs(projectile.velocity.X);
                    else
                        projectile.velocity.X = 0f - Math.Abs(projectile.velocity.X);
                }

                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, ModContent.ProjectileType<StarFragmentProj>(), 100, 5f, Main.myPlayer);
                projectile.Kill();
            }
        }
    }
}