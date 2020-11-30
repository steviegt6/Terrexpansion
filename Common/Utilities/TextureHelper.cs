using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Terrexpansion.Common.Utilities
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Creates a copy of the Texture2D with all non-transparent pixels converted to the specified color. <br />
        /// If no graphics device is specified, it will default to <c>Main.spriteBatch.GraphicsDevice</c>.
        /// </summary>
        /// <param name="color">The flat color you want to turn this texture to.</param>
        /// <returns></returns>
        public static Texture2D ToFlatColor(this Texture2D texture, Color color, GraphicsDevice graphicsDevice = null)
        {
            graphicsDevice = graphicsDevice ?? Main.spriteBatch.GraphicsDevice;

            Texture2D newTexture = new Texture2D(graphicsDevice, texture.Width, texture.Height);
            Color[] data = new Color[texture.Width * texture.Height];
            int dataIndex = 0;

            texture.GetData(data);

            foreach (Color pixel in data)
            {
                if (pixel.A != 0)
                {
                    data[dataIndex] = new Color(color.R, color.G, color.B);
                }

                dataIndex++;
            }

            newTexture.SetData(data);
            return newTexture;
        }
    }
}