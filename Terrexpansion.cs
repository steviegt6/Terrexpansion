using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.States;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.ModLoader;
using Terrexpansion.Assets;
using Terrexpansion.Common;
using Terrexpansion.Content.Skies;

namespace Terrexpansion
{
    public partial class Terrexpansion : Mod
    {
        private static bool _hasInitiailizedPlayerMenu = false;
        private static bool _hasInitializedWorldMenu = false;

        public static bool Unloading = false;
        public static bool SetupContent = false;
        public static bool CanAutosize = false;
        public static List<string> SplashText = new List<string>();
        public static string DeathSplashText;
        public static LocalizedText CoinSplashText;
        public static Assembly TerrariaAssembly = typeof(Main).Assembly;

        public enum MessageType : byte
        {
            SyncModPlayer
        }

        private string _origVersion;

        public Terrexpansion()
        {
        }

        public override void Load()
        {
            AssetHelper.LoadAssets();
            LoadMethodSwaps();

            UICharacterSelect uiCharacterSelect = (UICharacterSelect)TerrariaAssembly.GetType("Terraria.Main").GetField("_characterSelectMenu", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            uiCharacterSelect.RemoveAllChildren();
            uiCharacterSelect.Remove();
            uiCharacterSelect.Deactivate();
            uiCharacterSelect.OnInitialize();

            UIWorldSelect uiWorldSelect = (UIWorldSelect)TerrariaAssembly.GetType("Terraria.Main").GetField("_worldSelectMenu", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            uiWorldSelect.RemoveAllChildren();
            uiWorldSelect.Remove();
            uiWorldSelect.Deactivate();
            uiWorldSelect.OnInitialize();

            SkyManager.Instance["Terrexpansion:Credits"] = new TerrexpansionCredits();

            _origVersion = Main.versionNumber;

            // Example of adding our own keys into Terraria's localization dict, you should probably never do this.
            Dictionary<string, LocalizedText> localizedTexts = TerrariaAssembly.GetType("Terraria.Localization.LanguageManager").GetField("_localizedTexts", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(LanguageManager.Instance) as Dictionary<string, LocalizedText>;
            LocalizedText terrexpansionStyle = (LocalizedText)TerrariaAssembly.GetType("Terraria.Localization.LocalizedText").GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public, null, new Type[2] { typeof(string), typeof(string) }, null).Invoke(new object[] { "UI.HealthManaStyle_TerrexpansionStyle", "Fancy-Classic" });

            localizedTexts.Add("UI.HealthManaStyle_TerrexpansionStyle", terrexpansionStyle);

            Main.PlayerResourcesSets.Add("TerrexpansionStyle", new FancyClassicPlayerResourcesDisplaySet("FancyClassic", ReLogic.Content.AssetRequestMode.ImmediateLoad));
        }

        public override void PostSetupContent()
        {
            AssetHelper.SwapAssets();

            Main.versionNumber = "Terrexpansion v1.0.0.0";
            SetupContent = true;
        }

        public override void Unload()
        {
            Unloading = true;

            UnloadMethodSwaps();
            AssetHelper.UnloadAssets();

            UICharacterSelect uiCharacterSelect = (UICharacterSelect)TerrariaAssembly.GetType("Terraria.Main").GetField("_characterSelectMenu", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            uiCharacterSelect.RemoveAllChildren();
            uiCharacterSelect.RemoveChild(filterTextBoxPlayer);
            uiCharacterSelect.Remove();
            uiCharacterSelect.Deactivate();
            uiCharacterSelect.OnInitialize();

            UIWorldSelect uiWorldSelect = (UIWorldSelect)TerrariaAssembly.GetType("Terraria.Main").GetField("_worldSelectMenu", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            uiWorldSelect.RemoveAllChildren();
            uiWorldSelect.RemoveChild(filterTextBoxWorld);
            uiWorldSelect.Remove();
            uiWorldSelect.Deactivate();
            uiWorldSelect.OnInitialize();

            (typeof(Main).Assembly.GetType("Terraria.Localization.LanguageManager").GetField("_localizedTexts", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(LanguageManager.Instance) as Dictionary<string, LocalizedText>).Remove("UI.HealthManaStyle_TerrexpansionStyle");
            Main.PlayerResourcesSets.Remove("TerrexpansionStyle");

            Main.versionNumber = _origVersion;

            CanAutosize = false;
            Unloading = false;
        }

        public override void AddRecipeGroups() => RecipeHelper.AddRecipeGroups();

        public override void AddRecipes() => RecipeHelper.AddRecipes(this);

        public override void PostAddRecipes()
        {
            for (int i = 0; i < 174; i++)
            {
                SplashText.Add(Language.GetTextValue("Mods.Terrexpansion.SplashText." + i));
            }

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