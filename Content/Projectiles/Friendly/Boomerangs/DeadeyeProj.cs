using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terrexpansion.Content.Projectiles.Friendly.Misc;

namespace Terrexpansion.Content.Projectiles.Friendly.Boomerangs
{
    public class DeadeyeProj : BaseProjectile
    {
        public override bool AutosizeProjectile => false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dead-eye");

            Main.projFrames[Type] = 4;

        }

        public override void SafeSetDefaults()
        {
            projectile.aiStyle = 3;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.DamageType = DamageClass.Ranged;
            projectile.width = 42;
            projectile.height = 44;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => SpawnBonkProj();

        public override void OnHitPlayer(Player target, int damage, bool crit) => SpawnBonkProj();

        public override void OnHitPvp(Player target, int damage, bool crit) => SpawnBonkProj();

        public void SpawnBonkProj() => Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<BonkProj>(), 0, 0f);

        public override void AI()
        {
            projectile.rotation = 0f;
        }
    }
}