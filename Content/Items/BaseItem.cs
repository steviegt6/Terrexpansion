using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Items
{
    /// <summary>
    /// This serves as a base class for all items in this mod.
    /// </summary>
    public abstract class BaseItem : ModItem
    {
        /// <summary>
        /// Whether or not this item's size should be set automatically.
        /// <para>Defaults to true.</para>
        /// </summary>
        public virtual bool AutosizeItem => true;

        /// <summary>
        /// Where or not this item's <c>axe</c> power in <c>SafeSetDefaults()</c> should make sense readability-wise.
        /// <para>Returns false by default.</para>
        /// </summary>
        public virtual bool UseImprovedAxePower => false;

        /// <summary>
        /// The item's <c>Texture2D</c> (wrapped in an <c>Asset</c>).
        /// </summary>
        public Asset<Texture2D> ItemTexture => TextureAssets.Item[Type];

        /// <summary>
        /// Overridable version of <c>SetDefaults()</c>. Called at the beginning of <c>SetDefaults()</c>, before Autosize and Axe Power recalculation logic.
        /// </summary>
        public virtual void SafeSetDefaults() { }

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();

            if (AutosizeItem && Terrexpansion.Instance.canAutosize)
            {
                Vector2 itemSize = ItemTexture.Size();

                if (Main.itemAnimationsRegistered.Contains(Type))
                {
                    itemSize = new Vector2(ItemTexture.Width(), ItemTexture.Height() / ((DrawAnimationVertical)Main.itemAnimations[Type]).FrameCount);
                }

                item.Size = itemSize;
            }

            if (UseImprovedAxePower && item.axe != 0)
            {
                item.axe /= 5;
            }
        }
    }
}