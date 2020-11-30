using Microsoft.Xna.Framework;
using Terraria;

namespace Terrexpansion.Content.Projectiles.Friendly.Misc
{
    public class BonkProj : BaseProjectile
    {
        public int lifeTime = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bonk!");

            Main.projFrames[Type] = 3;
        }

        public override void SafeSetDefaults()
        {
            projectile.damage = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.aiStyle = -1;
            projectile.width = 52;
            projectile.height = 64;
        }

        public override void AI()
        {
            projectile.velocity = Vector2.Zero;

            if (++lifeTime >= 60)
                projectile.Kill();
        }
    }
}
