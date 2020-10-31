﻿using Microsoft.Xna.Framework;
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
        public Asset<Texture2D> projectileTexture => TextureAssets.Projectile[Type];

        /// <summary>
        /// Overridable version of <c>SetDefaults()</c>. Called at the beginning of <c>SetDefaults()</c>, before Autosize logic.
        /// </summary>
        public virtual void SafeSetDefaults() { }

        public override void SetDefaults()
        {
            SafeSetDefaults();

            if (AutosizeProjectile && Terrexpansion.CanAutosize)
            {
                Vector2 projectileSize = projectileTexture.Size();

                if (Main.projFrames[Type] > 1)
                {
                    projectileSize = new Vector2(projectileTexture.Width() / Main.projFrames[Type], projectileTexture.Height());
                }

                projectile.Size = projectileSize;
            }
        }
    }
}