using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Terrexpansion.Common.Utilities
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Returns a tooltip line using <c>FirstOrDefault</c>. <br />
        /// Returns null if no tooltip line is found.
        /// </summary>
        public static TooltipLine GetTooltip(this List<TooltipLine> tooltips, string tooltipName, string tooltipMod) => tooltips.FirstOrDefault(t => t.Name == tooltipName && t.mod == tooltipMod);

        /// <summary>
        /// Utilizes <see cref="GetTooltip(List{TooltipLine}, string, string)"/> to grab a vanilla tooltip line instead of having to specify the tooltip line's mod. <br />
        /// Returns null if no tooltip line is found.
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="tooltipName"></param>
        /// <returns></returns>
        public static TooltipLine GetVanillaTooltip(this List<TooltipLine> tooltips, string tooltipName) => tooltips.GetTooltip(tooltipName, "Terraria");

        /// <summary>
        /// Same as <see cref="GetTooltip(List{TooltipLine}, string, string)"/> but returns a boolean that is true or false depending on if it is found and instead has an out parameter for the tooltip line.
        /// </summary>
        public static bool TryGetTooltip(this List<TooltipLine> tooltips, string tooltipName, string tooltipMod, out TooltipLine tooltip) => (tooltip = tooltips.GetTooltip(tooltipName, tooltipMod)) != null;

        /// <summary>
        /// Utilizes <see cref="TryGetTooltip(List{TooltipLine}, string, string, out TooltipLine)"/> to get a vanilla tooltip line instead of having to specify the tooltip line's mod.
        /// </summary>
        public static bool TryGetVanillaTooltip(this List<TooltipLine> tooltips, string tooltipName, out TooltipLine tooltip) => (tooltip = tooltips.GetVanillaTooltip(tooltipName)) != null;
    }
}