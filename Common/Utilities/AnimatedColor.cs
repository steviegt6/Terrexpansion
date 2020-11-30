using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace Terrexpansion.Common.Utilities
{
    public struct AnimatedColor
    {
        public Color[] colors;
        public Color color1;
        public Color color2;
        public float speedModifier;

        /// <param name="color1">The first color.</param>
        /// <param name="color2">The second color.</param>
        /// <param name="speedModifier">A way to modifiy how fast it goes. <br />
        /// Lower is slower, higher is faster.</param>
        public AnimatedColor(Color color1, Color color2, float speedModifier = 25f)
        {
            colors = default;
            this.color1 = color1;
            this.color2 = color2;
            this.speedModifier = speedModifier;
        }

        /// <param name="colors">The colors in this AnimatedColor.</param>
        /// <param name="speedModifier">A way to modifiy how fast it goes. <br />
        /// Higher is slower, lower is faster.</param>
        public AnimatedColor(Color[] colors, float speedModifier = 25f)
        {
            this.colors = colors;
            color1 = default;
            color2 = default;
            this.speedModifier = speedModifier;
        }

        public Color GetColor() => colors == default ? Color.Lerp(color1, color2, (float)(Math.Sin(Main.GameUpdateCount / speedModifier) + 1f) / 2f) : Color.Lerp(colors[(int)(Main.GameUpdateCount / 60 % colors.Length)], colors[((int)(Main.GameUpdateCount / 60 % colors.Length) + 1) % colors.Length], Main.GameUpdateCount % 60 / speedModifier);

        public Vector3 LightingColor() => GetColor().ToVector3() / 255f;
    }
}