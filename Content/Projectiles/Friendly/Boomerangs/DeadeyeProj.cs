using Terraria.ModLoader;

namespace Terrexpansion.Content.Projectiles.Friendly.Boomerangs
{
    public class DeadeyeProj : BaseProjectile
    {
        public override void SafeSetDefaults()
        {
            projectile.aiStyle = 3;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.DamageType = DamageClass.Ranged;
        }
    }
}
