using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terrexpansion.Common.Configs.ClientSide;
using Terrexpansion.Common.Configs.ServerSide;
using Terrexpansion.Common.Globals.NPCs;
using Terrexpansion.Common.Players;
using Terrexpansion.Common.Utilities;
using Terrexpansion.Content.Dusts;
using Terrexpansion.Content.Skies;

namespace Terrexpansion
{
    public partial class Terrexpansion : Mod
    {
        public static Terrexpansion Instance { get; private set; }

        public static string DeathSplashText, CoinSplashText;

        private static bool _hasInitializedPlayerMenu, _hasInitializedWorldMenu = false;

        public bool SetupContent, CanAutosize;
        public List<string> SplashText;
        public Assembly TMLAssembly;

        private string _origVersion;

        public enum MessageType : byte
        {
            SyncModPlayer
        }

        public Terrexpansion() => Properties = new ModProperties { Autoload = true, AutoloadBackgrounds = true, AutoloadGores = true, AutoloadSounds = true };

        public override void Load()
        {
            Instance = this;
            TerreConfigGenericClient.Instance = ModContent.GetInstance<TerreConfigGenericClient>();
            TerreConfigTooltips.Instance = ModContent.GetInstance<TerreConfigTooltips>();
            TerreConfigGenericServer.Instance = ModContent.GetInstance<TerreConfigGenericServer>();
            SetupContent = false;
            CanAutosize = false;
            DeathSplashText = "";
            CoinSplashText = "";
            SplashText = new List<string>();
            TMLAssembly = typeof(ModLoader).Assembly;
            _origVersion = "";

            AssetHelper.LoadAssets();
            LoadMethodSwaps();
            LoadILEdits();

            SkyManager.Instance["Terrexpansion:Credits"] = new TerrexpansionCredits();
            _origVersion = Main.versionNumber;
        }

        public override void PostSetupContent()
        {
            AssetHelper.SwapAssets();

            Main.versionNumber = "Terrexpansion v1.0.0.0";
            SetupContent = true;
        }

        public override void Unload()
        {
            AssetHelper.UnloadAssets();
            UnloadMethodSwaps();
            UnloadILEdits();

            Instance = null;
            TerreConfigGenericClient.Instance = null;
            TerreConfigTooltips.Instance = null;
            TerreConfigGenericServer.Instance = null;
            DeathSplashText = null;
            CoinSplashText = null;

            Main.versionNumber = _origVersion;
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Language.FindAll(Lang.CreateDialogFilter("Mods.Terrexpansion.SplashText" + ".", null)).Length; i++)
            {
                SplashText.Add(Language.GetTextValue("Mods.Terrexpansion.SplashText." + i));
            }

            TerreNPC.InitializeBloodTypes();

            SplashText[0] = $"Home to {SplashText.Count} splash texts!";
            SplashText[1] = $"Splash Text Entry #{Main.rand.Next(1, SplashText.Count)}";
            SplashText[2] = $"{Environment.UserName.ToUpper()} IS YOU";
            SplashText[3] = new string($"{Environment.UserName}!".ToCharArray().Reverse().ToArray());
            SplashText[4] = $"Always watching, {Environment.UserName}, Always watching...";

            CanAutosize = true;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.SyncModPlayer:
                    byte player = reader.ReadByte();
                    TerrePlayer terrePlayer = Main.player[player].GetModPlayer<TerrePlayer>();
                    terrePlayer.starFruit = reader.ReadInt32();
                    terrePlayer.extendedLungs = reader.ReadBoolean();
                    break;
            }
        }
    }
}