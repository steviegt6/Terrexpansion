using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour.HookGen;
using ReLogic.Content;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.States;

namespace Terrexpansion
{
    // Example of creating Monomod hooks using MonoMod.RuntimeDetour.HookGen.HookEndpointManager and System.Reflection. Hopefully, as a newer modder, you should never have to mess with these.
    partial class Terrexpansion
    {
        public delegate void Orig_FancyClassicPlayerResourcesDisplaySet_PrepareFields(FancyClassicPlayerResourcesDisplaySet self, Player player);

        public delegate void Hook_FancyClassicPlayerResourcesDisplaySet_PrepareFields(Orig_FancyClassicPlayerResourcesDisplaySet_PrepareFields orig, FancyClassicPlayerResourcesDisplaySet self, Player player);

        public static event Hook_FancyClassicPlayerResourcesDisplaySet_PrepareFields On_FancyClassicPlayerResourcesDisplaySet_PrepareFields
        {
            add => HookEndpointManager.Add<Hook_FancyClassicPlayerResourcesDisplaySet_PrepareFields>(TerrariaAssembly.GetType("Terraria.GameContent.UI.FancyClassicPlayerResourcesDisplaySet").GetMethod("PrepareFields", BindingFlags.Instance | BindingFlags.NonPublic), value);

            remove => HookEndpointManager.Remove<Hook_FancyClassicPlayerResourcesDisplaySet_PrepareFields>(TerrariaAssembly.GetType("Terraria.GameContent.UI.FancyClassicPlayerResourcesDisplaySet").GetMethod("PrepareFields", BindingFlags.Instance | BindingFlags.NonPublic), value);
        }

        public delegate void Orig_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer(FancyClassicPlayerResourcesDisplaySet self, int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect);

        public delegate void Hook_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer(Orig_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer orig, FancyClassicPlayerResourcesDisplaySet self, int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect);

        public static event Hook_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer On_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer
        {
            add => HookEndpointManager.Add<Hook_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer>(TerrariaAssembly.GetType("Terraria.GameContent.UI.FancyClassicPlayerResourcesDisplaySet").GetMethod("StarFillingDrawer", BindingFlags.Instance | BindingFlags.NonPublic), value);

            remove => HookEndpointManager.Remove<Hook_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer>(TerrariaAssembly.GetType("Terraria.GameContent.UI.FancyClassicPlayerResourcesDisplaySet").GetMethod("StarFillingDrawer", BindingFlags.Instance | BindingFlags.NonPublic), value);
        }

        public delegate void Orig_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields(HorizontalBarsPlayerReosurcesDisplaySet self, Player player);

        public delegate void Hook_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields(Orig_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields orig, HorizontalBarsPlayerReosurcesDisplaySet self, Player player);

        public static event Hook_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields On_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields
        {
            add => HookEndpointManager.Add<Hook_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields>(TerrariaAssembly.GetType("Terraria.GameContent.UI.HorizontalBarsPlayerReosurcesDisplaySet").GetMethod("PrepareFields", BindingFlags.Instance | BindingFlags.NonPublic), value);

            remove => HookEndpointManager.Remove<Hook_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields>(TerrariaAssembly.GetType("Terraria.GameContent.UI.HorizontalBarsPlayerReosurcesDisplaySet").GetMethod("PrepareFields", BindingFlags.Instance | BindingFlags.NonPublic), value);
        }

        public delegate void Orig_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer(HorizontalBarsPlayerReosurcesDisplaySet self, int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect);

        public delegate void Hook_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer(Orig_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer orig, HorizontalBarsPlayerReosurcesDisplaySet self, int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect);

        public static event Hook_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer On_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer
        {
            add => HookEndpointManager.Add<Hook_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer>(TerrariaAssembly.GetType("Terraria.GameContent.UI.HorizontalBarsPlayerReosurcesDisplaySet").GetMethod("ManaFillingDrawer", BindingFlags.Instance | BindingFlags.NonPublic), value);

            remove => HookEndpointManager.Remove<Hook_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer>(TerrariaAssembly.GetType("Terraria.GameContent.UI.HorizontalBarsPlayerReosurcesDisplaySet").GetMethod("ManaFillingDrawer", BindingFlags.Instance | BindingFlags.NonPublic), value);
        }

        public delegate void Orig_UICharacterSelect_UpdatePlayersList(UICharacterSelect self);

        public delegate void Hook_UICharacterSelect_UpdatePlayersList(Orig_UICharacterSelect_UpdatePlayersList orig, UICharacterSelect self);

        public static event Hook_UICharacterSelect_UpdatePlayersList On_UICharacterSelect_UpdatePlayersList
        {
            add => HookEndpointManager.Add<Hook_UICharacterSelect_UpdatePlayersList>(TerrariaAssembly.GetType("Terraria.GameContent.UI.States.UICharacterSelect").GetMethod("UpdatePlayersList", BindingFlags.Instance | BindingFlags.NonPublic), value);

            remove => HookEndpointManager.Remove<Hook_UICharacterSelect_UpdatePlayersList>(TerrariaAssembly.GetType("Terraria.GameContent.UI.States.UICharacterSelect").GetMethod("UpdatePlayersList", BindingFlags.Instance | BindingFlags.NonPublic), value);
        }

        public delegate void Orig_UIWorldSelect_UpdateWorldsList(UIWorldSelect self);

        public delegate void Hook_UIWorldSelect_UpdateWorldsList(Orig_UIWorldSelect_UpdateWorldsList orig, UIWorldSelect self);

        public static event Hook_UIWorldSelect_UpdateWorldsList On_WorldSelect_UpdateWorldsList
        {
            add => HookEndpointManager.Add<Hook_UIWorldSelect_UpdateWorldsList>(TerrariaAssembly.GetType("Terraria.GameContent.UI.States.UIWorldSelect").GetMethod("UpdateWorldsList", BindingFlags.Instance | BindingFlags.NonPublic), value);

            remove => HookEndpointManager.Remove<Hook_UIWorldSelect_UpdateWorldsList>(TerrariaAssembly.GetType("Terraria.GameContent.UI.States.UIWorldSelect").GetMethod("UpdateWorldsList", BindingFlags.Instance | BindingFlags.NonPublic), value);
        }

        public delegate void Orig_Main_DrawInterface_Resources_Breath();

        public delegate void Hook_Main_DrawInterface_Resources_Breath(Orig_Main_DrawInterface_Resources_Breath self);

        public static event Hook_Main_DrawInterface_Resources_Breath On_Main_DrawInterface_Resources_Breath
        {
            add => HookEndpointManager.Add<Hook_UIWorldSelect_UpdateWorldsList>(TerrariaAssembly.GetType("Terraria.Main").GetMethod("DrawInterface_Resources_Breath", BindingFlags.Static | BindingFlags.NonPublic), value);

            remove => HookEndpointManager.Remove<Hook_UIWorldSelect_UpdateWorldsList>(TerrariaAssembly.GetType("Terraria.Main").GetMethod("DrawInterface_Resources_Breath", BindingFlags.Static | BindingFlags.NonPublic), value);
        }

        public delegate void Orig_Main_DrawInterface_35_YouDied();

        public delegate void Hook_Main_DrawInterface_35_YouDied(Orig_Main_DrawInterface_35_YouDied self);

        public static event Hook_Main_DrawInterface_35_YouDied On_Main_DrawInterface_35_YouDied
        {
            add => HookEndpointManager.Add<Hook_Main_DrawInterface_35_YouDied>(TerrariaAssembly.GetType("Terraria.Main").GetMethod("DrawInterface_35_YouDied", BindingFlags.Static | BindingFlags.NonPublic), value);

            remove => HookEndpointManager.Remove<Hook_Main_DrawInterface_35_YouDied>(TerrariaAssembly.GetType("Terraria.Main").GetMethod("DrawInterface_35_YouDied", BindingFlags.Static | BindingFlags.NonPublic), value);
        }
    }
}