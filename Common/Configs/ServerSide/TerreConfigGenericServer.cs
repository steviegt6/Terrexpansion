using Terraria;
using Terraria.ModLoader.Config;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion.Common.Configs.ServerSide
{
    [Label("General Server-Side")]
    public class TerreConfigGenericServer : ModConfig
    {
        public static TerreConfigGenericServer Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message) => Main.player[whoAmI].IsServerHost(ref message);
    }
}