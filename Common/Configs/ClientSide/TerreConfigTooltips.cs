using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Terrexpansion.Common.Configs.ClientSide
{
    [Label("Tooltip Modifications")]
    public class TerreConfigTooltips : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Tooltip Modifications")]
        [Label("Show Damage Modifications")]
        [Tooltip("Show \"(+/- added/removed damage)\" next to an item's damage.")]
        [DefaultValue(true)]
        public bool showTooltipDamageMods;

        [Label("Color Damage Modifications")]
        [Tooltip("Color the \"(+/- added/removed damage)\" text depending on the value.")]
        [DefaultValue(true)]
        public bool colorTooltipDamageMods;

        [Label("Show Crit Chance Modifications")]
        [Tooltip("Show \"(+/- added/removed crit%)\" next to an item's crit.")]
        [DefaultValue(true)]
        public bool showTooltipCritMods;

        [Label("Color Crit Chance Modifications")]
        [Tooltip("Color the \"(+/- added/removed crit%)\" text depending on the value.")]
        [DefaultValue(true)]
        public bool colorTooltipCritMods;

        [Label("Color Favorited Tooltips")]
        [Tooltip("Colors the tooltips that indicate an item is favorited.")]
        [DefaultValue(true)]
        public bool colorTooltipFavorites;

        [Label("Show Use Time")]
        [Tooltip("Show the use time of an item.")]
        [DefaultValue(true)]
        public bool showTooltipUseTime;

        [Label("Show Knockback")]
        [Tooltip("Show the knockback of an item.")]
        [DefaultValue(true)]
        public bool showTooltipKnockback;
    }
}