using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Terrexpansion.Common.Utilities
{
    public static class TooltipLineExtensions
    {
        public static TooltipLine GetVanillaTooltip(this List<TooltipLine> tooltips, string tooltipName) => tooltips.GetTooltip(tooltipName, "Terraria");

        public static TooltipLine GetTooltip(this List<TooltipLine> tooltips, string tooltipName, string tooltipMod) => tooltips.FirstOrDefault(t => t.Name == tooltipName && t.mod == tooltipMod);
    }
}
