using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.ModLoader;
using Terrexpansion.Common.Data;
using Terrexpansion.Common.Systems.Globals.NPCs;
using Terrexpansion.Common.Utilities;
using Terrexpansion.Content.Skies;

namespace Terrexpansion
{
    public partial class Terrexpansion : Mod
    {
        public static Terrexpansion Instance { get; private set; }

        public static string DeathSplashText, CoinSplashText;
        private static bool _hasInitializedPlayerMenu, _hasInitializedWorldMenu = false;

        public bool setupContent, canAutosize;
        public List<string> splashText;
        public Assembly tmlAssembly;

        public Terrexpansion()
        {
            Properties = new ModProperties
            {
                Autoload = true,
                AutoloadBackgrounds = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

            Instance = this;
        }

        public override void Load()
        {
            DeathSplashText = "";
            CoinSplashText = "";
            setupContent = false;
            canAutosize = false;
            splashText = new List<string>();
            tmlAssembly = typeof(ModLoader).Assembly;

            AssetHelper.LoadAssets();
            LoadMethodSwaps();
            LoadILEdits();

            SkyManager.Instance["Terrexpansion:Credits"] = new TerrexpansionCredits();
            Main.SettingDontScaleMainMenuUp = false;
        }

        public override void PostSetupContent()
        {
            AssetHelper.SwapAssets();

            Main.versionNumber = $"Terrexpansion v{Version}";
            setupContent = true;
        }

        public override void Unload()
        {
            AssetHelper.UnloadAssets();
            UnloadMethodSwaps();
            UnloadILEdits();

            Instance = null;
            DeathSplashText = null;
            CoinSplashText = null;

            Main.versionNumber = "1.4.1.2";
        }

        public override void PostAddRecipes()
        {
            EditRecipes();

            for (int i = 0; i < Language.FindAll(Lang.CreateDialogFilter("Mods.Terrexpansion.SplashText" + ".", null)).Length; i++)
                splashText.Add(Language.GetTextValue("Mods.Terrexpansion.SplashText." + i, splashText.Count, Main.rand.Next(splashText.Count), Environment.UserName.ToUpper(), new string($"{Environment.UserName}!".ToCharArray().Reverse().ToArray()), Environment.UserName));

            NPCBloodData.InitializeBloodData();

            canAutosize = true;
        }
    }
}