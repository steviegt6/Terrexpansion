using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion.Common.Configs.ClientSide
{
    [Label("General Client-Side")]
    public class TerreConfigClientSide : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Client-Side")]
        [Label("Force Minion Counter")]
        [Tooltip("Forces the minion counter that displays when holding summoner weapons to always be displayed.")]
        [DefaultValue(false)]
        public bool forceMinionCounter;

        [Label("Item Pick-up Blacklist")]
        [Tooltip("Allows you to choose what items you don't want to pick up.")]
        public List<int> blacklistItemsList = new List<int>();
    }
}