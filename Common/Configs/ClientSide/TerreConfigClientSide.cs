using System.ComponentModel;
using Terraria;
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
    }
}