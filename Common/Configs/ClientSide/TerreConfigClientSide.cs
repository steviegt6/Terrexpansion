using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Terrexpansion.Common.Configs.ClientSide
{
    [Label("General Client-Side")]
    public class TerreConfigClientSide : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Misc. Client-Side")]
        [Label("Force Minion Counter")]
        [Tooltip("Forces the minion counter that displays when holding summoner weapons to always be displayed.")]
        [DefaultValue(false)]
        public bool forceMinionCounter;

        [Label("Item Pick-up Blacklist")]
        [Tooltip("Allows you to choose what items you don't want to pick up.")]
        public List<int> blacklistItemsList = new List<int>();

        [Header("Sonar Potion Changes")]
        [Label("Item Appears Above Sonar Text")]
        [Tooltip("Makes the item you're going to catch also appear right above the text the sonar potion creates.")]
        public bool sonarPotionItem;

        [Label("Sonar Potion Item Outline")]
        [Tooltip("Draws a white outline around the item that appears if \"Item Appears Above Sonar Text\" is enabled.")]
        public bool sonarPotionItemOutline;
    }
}