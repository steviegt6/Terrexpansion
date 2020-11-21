using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Terrexpansion.Common.Utilities
{
    public static class TooltipLineExtensions
    {
        public static TooltipLine GetTooltip(this List<TooltipLine> tooltips, string tooltipName, string tooltipMod) => tooltips.FirstOrDefault(t => t.Name == tooltipName && t.mod == tooltipMod);

        public static TooltipLine GetVanillaTooltip(this List<TooltipLine> tooltips, string tooltipName) => tooltips.GetTooltip(tooltipName, "Terraria");

        public static bool TryGetTooltip(this List<TooltipLine> tooltips, string tooltipName, string tooltipMod, out TooltipLine tooltip)
        {
            TooltipLine tip = tooltips.GetTooltip(tooltipName, tooltipMod);

            if (tip != null)
            {
                tooltip = tip;

                return true;
            }

            tooltip = null;

            return false;
        }

        public static bool TryGetVanillaTooltip(this List<TooltipLine> tooltips, string tooltipName, out TooltipLine tooltip)
        {
            TooltipLine tip = tooltips.GetVanillaTooltip(tooltipName);

            if (tip != null)
            {
                tooltip = tip;

                return true;
            }

            tooltip = null;

            return false;
        }
    }
}