using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion.Content.Rarities
{
    public class ZenithRarity : ModRarity
    {
        public override Color RarityColor => new AnimatedColor(new Color(0, 255, 255), new Color(60, 60, 60), 20f).GetColor();
    }
}