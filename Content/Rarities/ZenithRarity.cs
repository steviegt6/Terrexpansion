using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion.Content.Rarities
{
    public class ZenithRarity : ModRarity
    {
        public override Color RarityColor => new AnimatedColor(Color.Black, Colors.RarityDarkPurple, 25f).GetColor();
    }
}