using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Menus
{
    public class TerrexpansionMenuCrimson : StaticSplashTextMenu
    {
        public override string DisplayName => "Terrexpansion: Crimson";

        public override Asset<Texture2D> Logo => ModContent.GetTexture("Terrexpansion/Assets/TerrexpansionLogoCrimson");
    }
}