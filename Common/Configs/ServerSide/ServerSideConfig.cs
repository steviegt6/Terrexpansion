using Terraria;
using Terraria.ModLoader.Config;

namespace Terrexpansion.Common.Configs.ServerSide
{
    [Label("General Server-Side")]
    public class ServerSideConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Server-Side")]
        [Label("Testing Mode")]
        [Tooltip("Enables testing mode.\nThis gives you access to the Journey's End panel with non-journey characters,\n a new panel with other useful features for testing (and cheating :p),\n and various helpful commands.")]
        public bool testingMode;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message) => Main.player[whoAmI].IsServerHost(ref message);
    }
}
