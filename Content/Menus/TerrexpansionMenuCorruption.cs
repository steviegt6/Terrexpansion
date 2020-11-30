using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Menus
{
    public class TerrexpansionMenuCorruption : StaticSplashTextMenu
    {
        public override string DisplayName => "Terrexpansion: Corruption";

        public override Asset<Texture2D> Logo => ModContent.GetTexture("Terrexpansion/Assets/TerrexpansionLogoCorruption");
    }
}