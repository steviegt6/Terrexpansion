using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Projectiles
{
    /// <summary>
    /// This serves as a base class for all items in the mod.
    /// </summary>
    public abstract class BaseProjectile : ModProjectile
    {
        /// <summary>
        /// Whether or not this projectile's size should be set automatically.
        /// <para>Defaults to true.</para>
        /// </summary>
        public virtual bool AutosizeProjectile => true;

        /// <summary>
        /// The projectile's <c>Texture2D</c> (wrapped in an <c>Asset</c>).
        /// </summary>
        public Asset<Texture2D> ProjectileTexture => TextureAssets.Projectile[Type];

        /// <summary>
        /// Overridable version of <c>SetDefaults()</c>. Called at the beginning of <c>SetDefaults()</c>, before Autosize logic.
        /// </summary>
        public virtual void SafeSetDefaults() { }

        public override void SetDefaults()
        {
            SafeSetDefaults();

            if (AutosizeProjectile && Terrexpansion.Instance.canAutosize)
            {
                projectile.Size = new Vector2(ProjectileTexture.Width() / Main.projFrames[Type] == 0 ? 1 : Main.projFrames[Type], ProjectileTexture.Height());
            }
        }
    }
}