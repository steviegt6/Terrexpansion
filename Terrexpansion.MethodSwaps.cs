using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terrexpansion.Common;
using Terrexpansion.Common.Players;
using Terrexpansion.Common.UI.Elements;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion
{
    partial class Terrexpansion
    {
        public UIInputTextField filterTextBoxPlayer, filterTextBoxWorld;

        public void LoadMethodSwaps()
        {
            On.Terraria.GameContent.UI.ClassicPlayerResourcesDisplaySet.DrawMana += ClassicPlayerResourcesDisplaySet_DrawMana;
            Hooks.On_FancyClassicPlayerResourcesDisplaySet_PrepareFields += Terrexpansion_On_FancyClassicPlayerResourcesDisplaySet_PrepareFields;
            Hooks.On_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer += Terrexpansion_On_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer;
            Hooks.On_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields += Terrexpansion_On_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields;
            Hooks.On_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer += Terrexpansion_On_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer;
            Hooks.On_Main_DrawInterface_Resources_Breath += Terrexpansion_On_Main_DrawInterface_Resources_Breath;
            On.Terraria.GameContent.UI.States.UICharacterSelect.OnInitialize += UICharacterSelect_OnInitialize;
            Hooks.On_UICharacterSelect_UpdatePlayersList += Terrexpansion_On_UICharacterSelect_UpdatePlayersList;
            On.Terraria.GameContent.UI.States.UIWorldSelect.OnInitialize += UIWorldSelect_OnInitialize;
            Hooks.On_WorldSelect_UpdateWorldsList += Terrexpansion_On_WorldSelect_UpdateWorldsList;
            On.Terraria.Main.PreDrawMenu += Main_PreDrawMenu;
            On.Terraria.Lang.CreateDeathMessage += Lang_CreateDeathMessage;

            UICharacterSelect uiCharacterSelect = (UICharacterSelect)TMLAssembly.GetCachedType("Terraria.Main").GetStaticField("_characterSelectMenu").GetValue(null);
            uiCharacterSelect.RemoveAllChildren();
            uiCharacterSelect.Remove();
            uiCharacterSelect.Deactivate();
            uiCharacterSelect.OnInitialize();

            UIWorldSelect uiWorldSelect = (UIWorldSelect)TMLAssembly.GetCachedType("Terraria.Main").GetStaticField("_worldSelectMenu").GetValue(null);
            uiWorldSelect.RemoveAllChildren();
            uiWorldSelect.Remove();
            uiWorldSelect.Deactivate();
            uiWorldSelect.OnInitialize();
        }

        public void UnloadMethodSwaps()
        {
            Hooks.On_FancyClassicPlayerResourcesDisplaySet_PrepareFields -= Terrexpansion_On_FancyClassicPlayerResourcesDisplaySet_PrepareFields;
            Hooks.On_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer -= Terrexpansion_On_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer;
            Hooks.On_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields -= Terrexpansion_On_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields;
            Hooks.On_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer -= Terrexpansion_On_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer;
            Hooks.On_UICharacterSelect_UpdatePlayersList -= Terrexpansion_On_UICharacterSelect_UpdatePlayersList;
            Hooks.On_WorldSelect_UpdateWorldsList -= Terrexpansion_On_WorldSelect_UpdateWorldsList;
            Hooks.On_Main_DrawInterface_Resources_Breath -= Terrexpansion_On_Main_DrawInterface_Resources_Breath;
            On.Terraria.Main.PreDrawMenu -= Main_PreDrawMenu;

            UICharacterSelect uiCharacterSelect = (UICharacterSelect)TMLAssembly.GetCachedType("Terraria.Main").GetStaticField("_characterSelectMenu").GetValue(null);
            uiCharacterSelect.RemoveAllChildren();
            if (filterTextBoxPlayer != null)
            {
                uiCharacterSelect.RemoveChild(filterTextBoxPlayer);
            }
            uiCharacterSelect.Remove();
            uiCharacterSelect.Deactivate();
            uiCharacterSelect.OnInitialize();

            UIWorldSelect uiWorldSelect = (UIWorldSelect)TMLAssembly.GetCachedType("Terraria.Main").GetStaticField("_worldSelectMenu").GetValue(null);
            uiWorldSelect.RemoveAllChildren();
            if (filterTextBoxWorld != null)
            {
                uiWorldSelect.RemoveChild(filterTextBoxWorld);
            }
            uiWorldSelect.Remove();
            uiWorldSelect.Deactivate();
            uiWorldSelect.OnInitialize();
        }

        public void ClassicPlayerResourcesDisplaySet_DrawMana(On.Terraria.GameContent.UI.ClassicPlayerResourcesDisplaySet.orig_DrawMana orig, ClassicPlayerResourcesDisplaySet self)
        {
            Player localPlayer = Main.LocalPlayer;
            SpriteBatch spriteBatch = Main.spriteBatch;
            Color mouseTextColor = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
            int UI_ScreenAnchorX = Main.screenWidth - 800;
            int totalStarFruit = localPlayer.GetModPlayer<TerrePlayer>().starFruit;
            int UIDisplay_ManaPerStar = localPlayer.statManaMax2 <= 200 ? 20 : localPlayer.statManaMax2 / 10;

            if (!localPlayer.ghost && localPlayer.statManaMax2 > 0)
            {
                Vector2 vector = FontAssets.MouseText.Value.MeasureString(Language.GetTextValue("LegacyInterface.2"));
                int posOffset = vector.X >= 45 ? (int)vector.X + 5 : 50;

                spriteBatch.DrawString(FontAssets.MouseText.Value, Language.GetTextValue("LegacyInterface.2"), new Vector2(800 - posOffset + UI_ScreenAnchorX, 6f), mouseTextColor, 0f, default, 1f, SpriteEffects.None, 0f);

                for (int i = 1; i < localPlayer.statManaMax2 / UIDisplay_ManaPerStar + 1; i++)
                {
                    int color;
                    bool isLast = false;
                    float scale = 1f;

                    if (localPlayer.statMana >= i * UIDisplay_ManaPerStar)
                    {
                        color = 255;

                        if (localPlayer.statMana == i * UIDisplay_ManaPerStar)
                        {
                            isLast = true;
                        }
                    }
                    else
                    {
                        float num4 = (localPlayer.statMana - (i - 1) * UIDisplay_ManaPerStar) / (float)UIDisplay_ManaPerStar;
                        color = (int)(30f + 225f * num4);

                        if (color < 30)
                        {
                            color = 30;
                        }

                        scale = num4 / 4f + 0.75f;

                        if (scale < 0.75)
                        {
                            scale = 0.75f;
                        }

                        if (num4 > 0f)
                        {
                            isLast = true;
                        }
                    }

                    if (isLast)
                    {
                        scale += Main.cursorScale - 1f;
                    }

                    if (i <= totalStarFruit && totalStarFruit != 0)
                    {
                        spriteBatch.Draw(AssetHelper.ClassicMana2Texture.Value, new Vector2(775 + UI_ScreenAnchorX, 30 + AssetHelper.ClassicMana2Texture.Height() / 2 + (AssetHelper.ClassicMana2Texture.Height() - AssetHelper.ClassicMana2Texture.Height() * scale) / 2f + (28 * (i - 1))), new Rectangle(0, 0, AssetHelper.ClassicMana2Texture.Width(), AssetHelper.ClassicMana2Texture.Height()), new Color(color, color, color, (int)(color * 0.9)), 0f, new Vector2(AssetHelper.ClassicMana2Texture.Width() / 2, AssetHelper.ClassicMana2Texture.Height() / 2), scale, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(TextureAssets.Mana.Value, new Vector2(775 + UI_ScreenAnchorX, 30 + TextureAssets.Mana.Height() / 2 + (TextureAssets.Mana.Height() - TextureAssets.Mana.Height() * scale) / 2f + (28 * (i - 1))), new Rectangle(0, 0, TextureAssets.Mana.Width(), TextureAssets.Mana.Height()), new Color(color, color, color, (int)(color * 0.9)), 0f, new Vector2(TextureAssets.Mana.Width() / 2, TextureAssets.Mana.Height() / 2), scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public void Terrexpansion_On_FancyClassicPlayerResourcesDisplaySet_PrepareFields(Hooks.Orig_FancyClassicPlayerResourcesDisplaySet_PrepareFields orig, FancyClassicPlayerResourcesDisplaySet self, Player player)
        {
            orig(self, player);

            Type resourcesDisplaySet = TMLAssembly.GetType("Terraria.GameContent.UI.FancyClassicPlayerResourcesDisplaySet");
            PlayerStatsSnapshot playerStatsSnapshot = new PlayerStatsSnapshot(player);
            FieldInfo manaPerStar = resourcesDisplaySet.GetInstanceField("_manaPerStar");
            FieldInfo starCount = resourcesDisplaySet.GetInstanceField("_starCount");
            FieldInfo currentPlayerMana = resourcesDisplaySet.GetInstanceField("_currentPlayerMana");
            FieldInfo lastStarFillingIndex = resourcesDisplaySet.GetInstanceField("_lastStarFillingIndex");

            manaPerStar.SetValue(self, playerStatsSnapshot.ManaMax <= 200 ? playerStatsSnapshot.ManaPerSegment : (Main.LocalPlayer.statManaMax2 / 10));
            starCount.SetValue(self, (int)(playerStatsSnapshot.ManaMax / (float)manaPerStar.GetValue(self)));
            currentPlayerMana.SetValue(self, playerStatsSnapshot.Mana);
            lastStarFillingIndex.SetValue(self, (int)((float)currentPlayerMana.GetValue(self) / (float)manaPerStar.GetValue(self)));
        }

        public void Terrexpansion_On_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer(Hooks.Orig_FancyClassicPlayerResourcesDisplaySet_StarFillingDrawer orig, FancyClassicPlayerResourcesDisplaySet self, int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
        {
            orig(self, elementIndex, firstElementIndex, lastElementIndex, out sprite, out offset, out drawScale, out sourceRect);

            if (Main.LocalPlayer.GetModPlayer<TerrePlayer>().starFruit >= elementIndex + 1 && Main.LocalPlayer.GetModPlayer<TerrePlayer>().starFruit != 0)
            {
                sprite = AssetHelper.ClassicMana2Texture;
            }
        }

        public void Terrexpansion_On_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields(Hooks.Orig_HorizontalBarsPlayerReosurcesDisplaySet_PrepareFields orig, HorizontalBarsPlayerReosurcesDisplaySet self, Player player)
        {
            orig(self, player);

            TMLAssembly.GetCachedType("Terraria.GameContent.UI.HorizontalBarsPlayerReosurcesDisplaySet").GetInstanceField("_mpSegmentsCount").SetValue(self, player.statManaMax2 <= 200 ? (player.statManaMax2 / 20) : (player.statManaMax2 / (player.statManaMax2 / 10)));
        }

        public void Terrexpansion_On_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer(Hooks.Orig_HorizontalBarsPlayerReosurcesDisplaySet_ManaFillingDrawer orig, HorizontalBarsPlayerReosurcesDisplaySet self, int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
        {
            orig(self, elementIndex, firstElementIndex, lastElementIndex, out sprite, out offset, out drawScale, out sourceRect);

            if (elementIndex + 1 <= Main.LocalPlayer.GetModPlayer<TerrePlayer>().starFruit && Main.LocalPlayer.GetModPlayer<TerrePlayer>().starFruit != 0)
            {
                sprite = AssetHelper.BarManaTexture;
            }

            Type resourcesDisplaySet = TMLAssembly.GetCachedType("Terraria.GameContent.UI.HorizontalBarsPlayerReosurcesDisplaySet");
            resourcesDisplaySet.GetStaticMethod("FillBarByValues").Invoke(null, new object[] { elementIndex, sprite, resourcesDisplaySet.GetInstanceField("_mpSegmentsCount").GetValue(self), resourcesDisplaySet.GetInstanceField("_mpPercent").GetValue(self), offset, drawScale, sourceRect });
        }

        public void Terrexpansion_On_Main_DrawInterface_Resources_Breath(Hooks.Orig_Main_DrawInterface_Resources_Breath self)
        {
            if (!Main.LocalPlayer.dead)
            {
                bool showLavaBubbles = false;
                Vector2 value = Main.LocalPlayer.Top + new Vector2(0f, Main.LocalPlayer.gfxOffY);

                if ((Main.LocalPlayer.lavaTime < Main.LocalPlayer.lavaMax && Main.LocalPlayer.lavaWet) || Main.LocalPlayer.lavaTime < Main.LocalPlayer.lavaMax && Main.LocalPlayer.breath == Main.LocalPlayer.breathMax)
                {
                    showLavaBubbles = true;
                }

                if (Main.playerInventory && Main.screenHeight < 1000)
                {
                    value.Y += Main.LocalPlayer.height - 20;
                }

                value = Vector2.Transform(value - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix);

                if (!Main.playerInventory || Main.screenHeight >= 1000)
                {
                    value.Y -= 100f;
                }

                value /= Main.UIScale;

                if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible)
                {
                    value = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 + 236);

                    if (Main.InGameUI.IsVisible)
                    {
                        value.Y = Main.screenHeight - 64;
                    }
                }

                if (Main.LocalPlayer.breath < Main.LocalPlayer.breathMax && !Main.LocalPlayer.ghost && !showLavaBubbles)
                {
                    int divisor = (Main.LocalPlayer.breathMax <= 200) ? 20 : Main.LocalPlayer.breathMax / 10;

                    for (int i = 1; i < Main.LocalPlayer.breathMax / divisor + 1; i++)
                    {
                        int color;
                        float scale = 1f;

                        if (Main.LocalPlayer.breath >= i * divisor)
                        {
                            color = 255;
                        }
                        else
                        {
                            float castedBreath = (Main.LocalPlayer.breath - (i - 1) * divisor) / (float)divisor;
                            color = (int)(30f + 225f * castedBreath);

                            if (color < 30)
                            {
                                color = 30;
                            }

                            scale = castedBreath / 4f + 0.75f;

                            if (scale < 0.75)
                            {
                                scale = 0.75f;
                            }
                        }

                        int offsetX = 0;
                        int offsetY = 0;

                        if (i > 10)
                        {
                            offsetX -= 260;
                            offsetY += 26;
                        }

                        Main.spriteBatch.Draw(Main.LocalPlayer.breathMax <= 200 ? TextureAssets.Bubble.Value : AssetHelper.LungExtendedBubble.Value, value + new Vector2(26 * (i - 1) + offsetX - 125f, 32f + (TextureAssets.Bubble.Height() - TextureAssets.Bubble.Height() * scale) / 2f + offsetY), new Rectangle(0, 0, TextureAssets.Bubble.Width(), TextureAssets.Bubble.Height()), new Color(color, color, color, color), 0f, default, scale, SpriteEffects.None, 0f);
                    }
                }

                if (Main.LocalPlayer.lavaTime < Main.LocalPlayer.lavaMax && !Main.LocalPlayer.ghost && showLavaBubbles)
                {
                    int divisor = Main.LocalPlayer.lavaMax / 10;

                    for (int j = 1; j < Main.LocalPlayer.lavaMax / divisor + 1; j++)
                    {
                        int color;
                        float scale = 1f;

                        if (Main.LocalPlayer.lavaTime >= j * divisor)
                        {
                            color = 255;
                        }
                        else
                        {
                            float num10 = (Main.LocalPlayer.lavaTime - (j - 1) * divisor) / (float)divisor;
                            color = (int)(30f + 225f * num10);

                            if (color < 30)
                            {
                                color = 30;
                            }

                            scale = num10 / 4f + 0.75f;

                            if (scale < 0.75)
                            {
                                scale = 0.75f;
                            }
                        }

                        int offsetX = 0;
                        int offsetY = 0;

                        if (j > 10)
                        {
                            offsetX -= 260;
                            offsetY += 26;
                        }

                        Main.spriteBatch.Draw(TextureAssets.Flame.Value, value + new Vector2(26 * (j - 1) + offsetX - 125f, 32f + (TextureAssets.Flame.Height() - TextureAssets.Flame.Height() * scale) / 2f + offsetY), new Rectangle(0, 0, TextureAssets.Bubble.Width(), TextureAssets.Bubble.Height()), new Color(color, color, color, color), 0f, default, scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public void UICharacterSelect_OnInitialize(On.Terraria.GameContent.UI.States.UICharacterSelect.orig_OnInitialize orig, UICharacterSelect self)
        {
            if (!_hasInitializedPlayerMenu)
            {
                _hasInitializedPlayerMenu = true;
                self.Initialize();

                return;
            }

            Type uiCharacterSelect = TMLAssembly.GetCachedType("Terraria.GameContent.UI.States.UICharacterSelect");
            FieldInfo playerList = uiCharacterSelect.GetInstanceField("_playerList");
            FieldInfo scrollbar = uiCharacterSelect.GetInstanceField("_scrollbar");
            MethodInfo fadedMouseOver = uiCharacterSelect.GetInstanceMethod("FadedMouseOver");
            MethodInfo fadedMouseOut = uiCharacterSelect.GetInstanceMethod("FadedMouseOut");
            MethodInfo goBackClick = uiCharacterSelect.GetInstanceMethod("GoBackClick");
            MethodInfo newCharacterClick = uiCharacterSelect.GetInstanceMethod("NewCharacterClick");

            UIElement uIElement = new UIElement();
            uIElement.Width.Set(0f, 0.8f);
            uIElement.MaxWidth.Set(650f, 0f);
            uIElement.Top.Set(220f, 0f);
            uIElement.Height.Set(-220f, 1f);
            uIElement.HAlign = 0.5f;

            UIPanel uIPanel = new UIPanel();
            uIPanel.Width.Set(0f, 1f);
            uIPanel.Height.Set(-110f, 1f);
            uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
            uiCharacterSelect.GetInstanceField("_containerPanel").SetValue(self, uIPanel);
            uIElement.Append(uIPanel);

            UIList uiPlayerList = (UIList)playerList.GetValue(self);
            uiPlayerList = new UIList();
            uiPlayerList.Width.Set(0f, 1f);
            uiPlayerList.Height.Set(0f, 1f);
            uiPlayerList.ListPadding = 5f;
            uIPanel.Append(uiPlayerList);

            UIScrollbar uiScrollbar = (UIScrollbar)scrollbar.GetValue(self);
            uiScrollbar = new UIScrollbar();
            uiScrollbar.SetView(100f, 1000f);
            uiScrollbar.Height.Set(0f, 1f);
            uiScrollbar.HAlign = 1f;
            uiPlayerList.SetScrollbar(uiScrollbar);
            scrollbar.SetValue(self, uiScrollbar);
            playerList.SetValue(self, uiPlayerList);

            UITextPanel<LocalizedText> uISelectPlayerText = new UITextPanel<LocalizedText>(Language.GetText("UI.SelectPlayer"), 0.8f, large: true)
            {
                HAlign = 0.05f
            };
            uISelectPlayerText.Width.Set(100f, 0f);
            uISelectPlayerText.Top.Set(-40f, 0f);
            uISelectPlayerText.SetPadding(15f);
            uISelectPlayerText.BackgroundColor = new Color(73, 94, 171);
            uIElement.Append(uISelectPlayerText);

            UITextPanel<LocalizedText> uIBackText = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, large: true);
            uIBackText.Width.Set(-10f, 0.5f);
            uIBackText.Height.Set(50f, 0f);
            uIBackText.VAlign = 1f;
            uIBackText.Top.Set(-45f, 0f);
            uIBackText.OnMouseOver += (evt, listeningElement) => { fadedMouseOver.Invoke(self, new object[] { evt, listeningElement }); };
            uIBackText.OnMouseOut += (evt, listeningElement) => { fadedMouseOut.Invoke(self, new object[] { evt, listeningElement }); };
            uIBackText.OnClick += (evt, listeningElement) => { goBackClick.Invoke(self, new object[] { evt, listeningElement }); };
            uIBackText.SetSnapPoint("Back", 0);
            uIElement.Append(uIBackText);
            uiCharacterSelect.GetField("_backPanel", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(self, uIBackText);

            UITextPanel<LocalizedText> uINewText = new UITextPanel<LocalizedText>(Language.GetText("UI.New"), 0.7f, large: true);
            uINewText.CopyStyle(uIBackText);
            uINewText.HAlign = 1f;
            uINewText.OnMouseOver += (evt, listeningElement) => { fadedMouseOver.Invoke(self, new object[] { evt, listeningElement }); };
            uINewText.OnMouseOut += (evt, listeningElement) => { fadedMouseOut.Invoke(self, new object[] { evt, listeningElement }); };
            uINewText.OnClick += (evt, listeningElement) => { newCharacterClick.Invoke(self, new object[] { evt, listeningElement }); };
            uIElement.Append(uINewText);
            uIBackText.SetSnapPoint("New", 0);
            uiCharacterSelect.GetField("_newPanel", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(self, uINewText);

            UIElement upperMenuContainer = new UIElement
            {
                Width = { Percent = 1f },
                Height = { Pixels = 32 },
                Top = { Pixels = -10 }
            };

            UIPanel filterTextBoxBackground = new UIPanel
            {
                Top = { Pixels = -20 },
                Left = { Pixels = -395, Percent = 1f },
                Width = { Pixels = 370 },
                Height = { Pixels = 40 }
            };

            filterTextBoxBackground.OnRightClick += (a, b) => filterTextBoxPlayer.Text = "";
            upperMenuContainer.Append(filterTextBoxBackground);

            filterTextBoxPlayer = new UIInputTextField(Language.GetTextValue("tModLoader.ModsTypeToSearch"))
            {
                Top = { Pixels = -15 },
                Left = { Pixels = -385, Percent = 1f },
                Width = { Pixels = 360 },
                Height = { Pixels = 20 }
            };

            filterTextBoxPlayer.OnTextChange += (a, b) => TMLAssembly.GetCachedType("Terraria.GameContent.UI.States.UICharacterSelect").GetInstanceMethod("UpdatePlayersList").Invoke(self, null);

            upperMenuContainer.Append(filterTextBoxPlayer);
            uIElement.Append(upperMenuContainer);
            self.Append(uIElement);
        }

        public void Terrexpansion_On_UICharacterSelect_UpdatePlayersList(Hooks.Orig_UICharacterSelect_UpdatePlayersList orig, UICharacterSelect self)
        {
            if (filterTextBoxPlayer == null)
            {
                filterTextBoxPlayer = new UIInputTextField("");
            }

            FieldInfo playerListInfo = TMLAssembly.GetCachedType("Terraria.GameContent.UI.States.UICharacterSelect").GetInstanceField("_playerList");
            FieldInfo currentlyMigratingFilesInfo = TMLAssembly.GetCachedType("Terraria.GameContent.UI.States.UICharacterSelect").GetStaticField("_currentlyMigratingFiles");
            bool currentlyMigratingFiles = (bool)currentlyMigratingFilesInfo.GetValue(null);
            UIList playerList = (UIList)playerListInfo.GetValue(self);

            playerList.Clear();

            List<PlayerFileData> list = new List<PlayerFileData>(Main.PlayerList);

            list.Sort(delegate (PlayerFileData x, PlayerFileData y)
            {
                return (x.IsFavorite && !y.IsFavorite || !x.IsFavorite && y.IsFavorite) ? -1 : (x.Name.CompareTo(y.Name) != 0) ? x.Name.CompareTo(y.Name) : x.GetFileName().CompareTo(y.GetFileName());
            });

            int snapPointIndex = 0;
            string filter = filterTextBoxPlayer.Text;

            foreach (PlayerFileData item in list)
            {
                if (item.Player.name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) != -1 && filter.Length > 0)
                {
                    playerList.Add(new UICharacterListItem(item, snapPointIndex++));
                }
                else if (filter.Length <= 0)
                {
                    playerList.Add(new UICharacterListItem(item, snapPointIndex++));
                }
            }

            if (list.Count == 0)
            {
                string vanillaPlayersPath = Path.Combine(ReLogic.OS.Platform.Get<ReLogic.OS.IPathService>().GetStoragePath("Terraria"), "Players");

                if (Directory.Exists(vanillaPlayersPath) && Directory.GetFiles(vanillaPlayersPath, "*.plr").Any())
                {
                    UIPanel autoMigrateButton = new UIPanel();
                    autoMigrateButton.Width.Set(0, 1);
                    autoMigrateButton.Height.Set(50, 0);

                    UIText migrateText = new UIText(!currentlyMigratingFiles ? Language.GetText("tModLoader.MigratePlayersText") : Language.GetText("tModLoader.MigratingWorldsText"));

                    autoMigrateButton.OnClick += (a, b) =>
                    {
                        if (!currentlyMigratingFiles)
                        {
                            currentlyMigratingFiles = true;
                            migrateText.SetText(Language.GetText("tModLoader.MigratingWorldsText"));

                            Task.Factory.StartNew(() =>
                            {
                                IEnumerable<string> vanillaPlayerFiles = Directory.GetFiles(vanillaPlayersPath, "*.*").Where(s => s.EndsWith(".plr") || s.EndsWith(".tplr") || s.EndsWith(".bak"));

                                foreach (string file in vanillaPlayerFiles)
                                {
                                    File.Copy(file, Path.Combine(Main.PlayerPath, Path.GetFileName(file)), true);
                                }

                                foreach (string mapDir in Directory.GetDirectories(vanillaPlayersPath))
                                {
                                    IEnumerable<string> mapFiles = Directory.GetFiles(mapDir, "*.*").Where(s => s.EndsWith(".map") || s.EndsWith(".tmap"));

                                    try
                                    {
                                        foreach (string mapFile in mapFiles)
                                        {
                                            string mapFileDir = Path.Combine(Main.PlayerPath, Directory.GetParent(mapFile).Name);

                                            Directory.CreateDirectory(mapFileDir);
                                            File.Copy(mapFile, Path.Combine(mapFileDir, Path.GetFileName(mapFile)), true);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        ModContent.GetInstance<Terrexpansion>().Logger.Error("tModLoader Error: " + Language.GetText("tModLoader.MigratePlayersException"), e);
                                    }
                                }

                                currentlyMigratingFiles = false;
                                Main.menuMode = 1;
                            }, TaskCreationOptions.PreferFairness);
                        }
                    };

                    autoMigrateButton.Append(migrateText);
                    playerList.Add(autoMigrateButton);

                    UIText noPlayersMessage = new UIText(Language.GetText("tModLoader.MigratePlayersMessage"));
                    noPlayersMessage.Width.Set(0, 1);
                    noPlayersMessage.Height.Set(200, 0);
                    noPlayersMessage.MarginTop = 20f;
                    noPlayersMessage.OnClick += (a, b) =>
                    {
                        Utils.OpenFolder(Main.PlayerPath);
                        Utils.OpenFolder(vanillaPlayersPath);
                    };

                    playerList.Add(noPlayersMessage);
                }
            }

            playerListInfo.SetValue(self, playerList);
            currentlyMigratingFilesInfo.SetValue(null, currentlyMigratingFiles);
        }

        public void UIWorldSelect_OnInitialize(On.Terraria.GameContent.UI.States.UIWorldSelect.orig_OnInitialize orig, UIWorldSelect self)
        {
            if (!_hasInitializedWorldMenu)
            {
                _hasInitializedWorldMenu = true;
                self.Initialize();
                return;
            }

            Type uiWorldSelect = TMLAssembly.GetCachedType("Terraria.GameContent.UI.States.UIWorldSelect");
            FieldInfo scrollbarInfo = uiWorldSelect.GetInstanceField("_scrollbar");
            FieldInfo worldListInfo = uiWorldSelect.GetInstanceField("_worldList");
            MethodInfo fadedMouseOverInfo = uiWorldSelect.GetInstanceMethod("FadedMouseOver");
            MethodInfo fadedMouseOutInfo = uiWorldSelect.GetInstanceMethod("FadedMouseOut");

            UIElement uIElement = new UIElement();
            uIElement.Width.Set(0f, 0.8f);
            uIElement.MaxWidth.Set(650f, 0f);
            uIElement.Top.Set(220f, 0f);
            uIElement.Height.Set(-220f, 1f);
            uIElement.HAlign = 0.5f;

            UIPanel uIPanel = new UIPanel();
            uIPanel.Width.Set(0f, 1f);
            uIPanel.Height.Set(-110f, 1f);
            uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
            uIElement.Append(uIPanel);
            uiWorldSelect.GetInstanceField("_containerPanel").SetValue(self, uIPanel);

            UIList _worldList = new UIList();
            _worldList.Width.Set(0f, 1f);
            _worldList.Height.Set(0f, 1f);
            _worldList.ListPadding = 5f;
            uIPanel.Append(_worldList);

            UIScrollbar scrollbar = new UIScrollbar();
            scrollbar.SetView(100f, 1000f);
            scrollbar.Height.Set(0f, 1f);
            scrollbar.HAlign = 1f;
            scrollbarInfo.SetValue(self, scrollbar);
            _worldList.SetScrollbar(scrollbar);
            worldListInfo.SetValue(self, _worldList);

            UITextPanel<LocalizedText> uISelectWorldText = new UITextPanel<LocalizedText>(Language.GetText("UI.SelectWorld"), 0.8f, large: true)
            {
                HAlign = 0.05f
            };
            uISelectWorldText.Top.Set(-40f, 0f);
            uISelectWorldText.SetPadding(15f);
            uISelectWorldText.BackgroundColor = new Color(73, 94, 171);
            uIElement.Append(uISelectWorldText);

            UITextPanel<LocalizedText> uIBackText = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, large: true);
            uIBackText.Width.Set(-10f, 0.5f);
            uIBackText.Height.Set(50f, 0f);
            uIBackText.VAlign = 1f;
            uIBackText.Top.Set(-45f, 0f);
            uIBackText.OnMouseOver += (a, b) => { fadedMouseOverInfo.Invoke(self, new object[] { a, b }); };
            uIBackText.OnMouseOut += (a, b) => { fadedMouseOutInfo.Invoke(self, new object[] { a, b }); };
            uIBackText.OnClick += (a, b) => { uiWorldSelect.GetInstanceMethod("GoBackClick").Invoke(self, new object[] { a, b }); };
            uIElement.Append(uIBackText);
            uiWorldSelect.GetField("_backPanel", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(self, uIBackText);

            UITextPanel<LocalizedText> uINewText = new UITextPanel<LocalizedText>(Language.GetText("UI.New"), 0.7f, large: true);
            uINewText.CopyStyle(uIBackText);
            uINewText.HAlign = 1f;
            uINewText.OnMouseOver += (a, b) => { fadedMouseOverInfo.Invoke(self, new object[] { a, b }); };
            uINewText.OnMouseOut += (a, b) => { fadedMouseOutInfo.Invoke(self, new object[] { a, b }); };
            uINewText.OnClick += (a, b) => { uiWorldSelect.GetInstanceMethod("NewWorldClick").Invoke(self, new object[] { a, b }); };
            uIElement.Append(uINewText);
            uiWorldSelect.GetField("_newPanel", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(self, uINewText);

            UIElement upperMenuContainer = new UIElement
            {
                Width = { Percent = 1f },
                Height = { Pixels = 32 },
                Top = { Pixels = -10 }
            };

            UIPanel filterTextBoxBackground = new UIPanel
            {
                Top = { Pixels = -20 },
                Left = { Pixels = -400, Percent = 1f },
                Width = { Pixels = 375 },
                Height = { Pixels = 40 }
            };

            filterTextBoxBackground.OnRightClick += (a, b) => filterTextBoxWorld.Text = "";
            upperMenuContainer.Append(filterTextBoxBackground);

            filterTextBoxWorld = new UIInputTextField(Language.GetTextValue("tModLoader.ModsTypeToSearch"))
            {
                Top = { Pixels = -15 },
                Left = { Pixels = -390, Percent = 1f },
                Width = { Pixels = 365 },
                Height = { Pixels = 20 }
            };

            filterTextBoxWorld.OnTextChange += (a, b) => TMLAssembly.GetCachedType("Terraria.GameContent.UI.States.UIWorldSelect").GetInstanceMethod("UpdateWorldsList").Invoke(self, null);

            upperMenuContainer.Append(filterTextBoxWorld);
            uIElement.Append(upperMenuContainer);
            self.Append(uIElement);
        }

        public void Terrexpansion_On_WorldSelect_UpdateWorldsList(Hooks.Orig_UIWorldSelect_UpdateWorldsList orig, UIWorldSelect self)
        {
            if (filterTextBoxWorld == null)
            {
                filterTextBoxWorld = new UIInputTextField("");
            }

            FieldInfo worldListInfo = TMLAssembly.GetCachedType("Terraria.GameContent.UI.States.UIWorldSelect").GetInstanceField("_worldList");
            FieldInfo currentlyMigratingFilesInfo = TMLAssembly.GetCachedType("Terraria.GameContent.UI.States.UIWorldSelect").GetStaticField("_currentlyMigratingFiles");
            bool currentlyMigratingFiles = (bool)currentlyMigratingFilesInfo.GetValue(null);
            UIList worldList = (UIList)worldListInfo.GetValue(self);

            worldList.Clear();

            IOrderedEnumerable<WorldFileData> orderedEnumerable = new List<WorldFileData>(Main.WorldList).OrderByDescending(CanWorldBePlayed).ThenByDescending((WorldFileData x) => x.IsFavorite).ThenBy((WorldFileData x) => x.Name).ThenBy((WorldFileData x) => x.GetFileName());

            string filter = filterTextBoxWorld.Text;
            int snapPointIndex = 0;

            foreach (WorldFileData item in orderedEnumerable)
            {
                if (item.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) != -1 && filter.Length > 0)
                {
                    worldList.Add(new UIWorldListItem(item, snapPointIndex++, CanWorldBePlayed(item)));
                }
                else if (filter.Length <= 0)
                {
                    worldList.Add(new UIWorldListItem(item, snapPointIndex++, CanWorldBePlayed(item)));
                }
            }

            if (!orderedEnumerable.Any())
            {
                string vanillaWorldsPath = Path.Combine(ReLogic.OS.Platform.Get<ReLogic.OS.IPathService>().GetStoragePath("Terraria"), "Worlds");

                if (Directory.Exists(vanillaWorldsPath) && Directory.GetFiles(vanillaWorldsPath, "*.wld").Any())
                {
                    UIPanel autoMigrateButton = new UIPanel();
                    autoMigrateButton.Width.Set(0, 1);
                    autoMigrateButton.Height.Set(50, 0);
                    UIText migrateText = new UIText(!currentlyMigratingFiles ? Language.GetText("tModLoader.MigrateWorldsText") : Language.GetText("tModLoader.MigratingWorldsText"));

                    autoMigrateButton.OnClick += (a, b) =>
                    {
                        if (!currentlyMigratingFiles)
                        {
                            currentlyMigratingFiles = true;
                            migrateText.SetText(Language.GetText("tModLoader.MigratingWorldsText"));

                            Task.Factory.StartNew(() =>
                            {
                                IEnumerable<string> vanillaWorldFiles = Directory.GetFiles(vanillaWorldsPath, "*.*").Where(s => s.EndsWith(".wld") || s.EndsWith(".twld") || s.EndsWith(".bak"));

                                foreach (string file in vanillaWorldFiles)
                                {
                                    File.Copy(file, Path.Combine(Main.WorldPath, Path.GetFileName(file)), true);
                                }

                                currentlyMigratingFiles = false;
                                Main.menuMode = 6;
                            }, TaskCreationOptions.PreferFairness);
                        }
                    };

                    autoMigrateButton.Append(migrateText);
                    worldList.Add(autoMigrateButton);

                    UIText noWorldsMessage = new UIText(Language.GetText("tModLoader.MigrateWorldsMessage"));
                    noWorldsMessage.Width.Set(0, 1);
                    noWorldsMessage.Height.Set(300, 0);
                    noWorldsMessage.MarginTop = 20f;
                    noWorldsMessage.OnClick += (a, b) =>
                    {
                        Utils.OpenFolder(Main.WorldPath);
                        Utils.OpenFolder(vanillaWorldsPath);
                    };

                    worldList.Add(noWorldsMessage);
                }
            }

            currentlyMigratingFilesInfo.SetValue(self, currentlyMigratingFiles);
            worldListInfo.SetValue(self, worldList);
        }

        public bool CanWorldBePlayed(WorldFileData file) => Main.ActivePlayerFileData.Player.difficulty == 3 == (file.GameMode == 3);

        public void Main_PreDrawMenu(On.Terraria.Main.orig_PreDrawMenu orig, Main self, out Point screenSizeCache, out Point screenSizeCacheAfterScaling)
        {
            orig(self, out screenSizeCache, out screenSizeCacheAfterScaling);

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.SamplerStateForCursor, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);

            string switchText = SkyManager.Instance["CreditsRoll"].IsActive() ? "View Terrexpansion Credits" : "View Vanilla Credits";
            Vector2 size = FontAssets.MouseText.Value.MeasureString(switchText);
            Rectangle switchTextRect = Main.menuMode == MenuID.CreditsRoll ? new Rectangle((int)(Main.screenWidth / 2 - (size.X / 2)), (int)(Main.screenHeight - 2 - size.Y), (int)size.X, (int)size.Y) : Rectangle.Empty;

            if (switchTextRect.Contains(Main.mouseX, Main.mouseY) && Main.mouseLeftRelease && Main.mouseLeft)
            {
                SoundEngine.PlaySound(SoundID.MenuTick);

                if (SkyManager.Instance["CreditsRoll"].IsActive())
                {
                    SkyManager.Instance.Deactivate("CreditsRoll");
                    SkyManager.Instance.Activate("Terrexpansion:Credits");
                }
                else
                {
                    SkyManager.Instance.Deactivate("Terrexpansion:Credits");
                    SkyManager.Instance.Activate("CreditsRoll");
                }
            }

            if (Main.menuMode == MenuID.CreditsRoll)
            {
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, switchText, new Vector2(switchTextRect.X, switchTextRect.Y),
                    switchTextRect.Contains(Main.mouseX, Main.mouseY) ? Main.OurFavoriteColor : new Color(120, 120, 120, 76), 0, Vector2.Zero, Vector2.One);
            }

            Main.spriteBatch.End();
        }

        public NetworkText Lang_CreateDeathMessage(On.Terraria.Lang.orig_CreateDeathMessage orig, string deadPlayerName, int plr, int npc, int proj, int other, int projType, int plrItemType)
        {
            NetworkText projectileName = NetworkText.Empty;
            NetworkText npcName = NetworkText.Empty;
            NetworkText pvpPlayerName = NetworkText.Empty;
            NetworkText pvpPlayerItemName = NetworkText.Empty;
            Projectile killerProj = Main.projectile[0];
            NPC killerNPC = Main.npc[0];
            Player pvpPlayer = Main.player[0];

            if (proj >= 0)
            {
                killerProj = Main.projectile[proj];
                projectileName = NetworkText.FromKey(Lang.GetProjectileName(projType).Key);
            }

            if (npc >= 0)
            {
                killerNPC = Main.npc[npc];
                npcName = killerNPC.GetGivenOrTypeNetName();
            }

            if (plr >= 0 && plr < 255)
            {
                pvpPlayer = Main.player[plr];
                pvpPlayerName = NetworkText.FromLiteral(pvpPlayer.name);
            }

            if (plrItemType >= 0)
            {
                pvpPlayerItemName = NetworkText.FromKey(Lang.GetItemName(plrItemType).Key);
            }

            bool slainByProjectile = projectileName != NetworkText.Empty;
            bool slainByPlayer = plr >= 0 && plr < 255;
            bool slainByNPC = npcName != NetworkText.Empty;
            NetworkText result = NetworkText.Empty;
            NetworkText empty = NetworkText.FromKey(Language.RandomFromCategory("DeathTextGeneric").Key, deadPlayerName, Main.worldName);

            if (Main.rand.NextBool(26))
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        empty = NetworkText.FromKey("Mods.Terrexpansion.DeathTextGeneric.Defleshed", deadPlayerName);
                        break;

                    case 1:
                        empty = NetworkText.FromKey("Mods.Terrexpansion.DeathTextGeneric.TornApart", deadPlayerName);
                        break;
                }
            }

            if (slainByPlayer)
            {
                result = NetworkText.FromKey("DeathSource.Player", empty, pvpPlayerName, slainByProjectile ? projectileName : pvpPlayerItemName);

                if (pvpPlayer.HeldItem.type == ItemID.KOCannon)
                {
                    result = NetworkText.FromKey("Mods.Terrexpansion.DeathText.KOd", deadPlayerName, pvpPlayerName);
                }
            }
            else if (Main.getGoodWorld && Main.rand.NextBool(30))
            {
                result = NetworkText.FromKey("Mods.Terrexpansion.DeathText.FTW", deadPlayerName);
            }
            else if (slainByNPC)
            {
                result = NetworkText.FromKey("DeathSource.NPC", empty, npcName);

                if (Main.rand.NextBool(2) && killerNPC.type == NPCID.Plantera || killerNPC.type == NPCID.Dandelion)
                {
                    result = NetworkText.FromKey("Mods.Terrexpansion.DeathText.FlowerPower", deadPlayerName);
                }

                if (killerNPC.type == NPCID.Ghost || killerNPC.type == NPCID.Wraith)
                {
                    result = NetworkText.FromKey("Mods.Terrexpansion.DeathText.Possessed", deadPlayerName, npcName);
                }

                if (Main.rand.NextBool(3) && NPCID.Sets.Zombies[killerNPC.type])
                {
                    result = NetworkText.FromKey("Mods.Terrexpansion.DeathText.Zombified", deadPlayerName);
                }

                if (killerNPC.type == NPCID.HallowBoss && Main.dayTime)
                {
                    result = NetworkText.FromKey("Mods.Terrexpansion.DeathText.EmpressBitch", deadPlayerName);
                }
            }
            else if (slainByProjectile)
            {
                result = NetworkText.FromKey("DeathSource.Projectile", empty, projectileName);

                if (Main.dayTime && killerProj.type == ProjectileID.HallowBossSplitShotCore || killerProj.type == ProjectileID.HallowBossRainbowStreak || killerProj.type == ProjectileID.HallowBossLastingRainbow || killerProj.type == ProjectileID.HallowBossDeathAurora || killerProj.type == ProjectileID.FairyQueenSunDance || killerProj.type == ProjectileID.FairyQueenLance)
                {
                    result = NetworkText.FromKey("Mods.Terrexpansion.DeathText.EmpressBitch", deadPlayerName);
                }
            }
            else
            {
                switch (other)
                {
                    case 0:
                        result = NetworkText.FromKey("DeathText.Fell_" + (Main.rand.Next(2) + 1), deadPlayerName);
                        break;

                    case 1:
                        result = NetworkText.FromKey("DeathText.Drowned_" + (Main.rand.Next(4) + 1), deadPlayerName);
                        break;

                    case 2:
                        result = NetworkText.FromKey("DeathText.Lava_" + (Main.rand.Next(4) + 1), deadPlayerName);
                        break;

                    case 3:
                        result = NetworkText.FromKey("DeathText.Default", empty);
                        break;

                    case 4:
                        result = NetworkText.FromKey("DeathText.Slain", deadPlayerName);
                        break;

                    case 5:
                        result = NetworkText.FromKey("DeathText.Petrified_" + (Main.rand.Next(4) + 1), deadPlayerName);
                        break;

                    case 6:
                        result = NetworkText.FromKey("DeathText.Stabbed", deadPlayerName);
                        break;

                    case 7:
                        result = NetworkText.FromKey("DeathText.Suffocated", deadPlayerName);
                        break;

                    case 8:
                        result = NetworkText.FromKey("DeathText.Burned", deadPlayerName);
                        break;

                    case 9:
                        result = NetworkText.FromKey("DeathText.Poisoned", deadPlayerName);
                        break;

                    case 10:
                        result = NetworkText.FromKey("DeathText.Electrocuted", deadPlayerName);
                        break;

                    case 11:
                        result = NetworkText.FromKey("DeathText.TriedToEscape", deadPlayerName);
                        break;

                    case 12:
                        result = NetworkText.FromKey("DeathText.WasLicked", deadPlayerName);
                        break;

                    case 13:
                        result = NetworkText.FromKey("DeathText.Teleport_1", deadPlayerName);
                        break;

                    case 14:
                        result = NetworkText.FromKey("DeathText.Teleport_2_Male", deadPlayerName);
                        break;

                    case 15:
                        result = NetworkText.FromKey("DeathText.Teleport_2_Female", deadPlayerName);
                        break;

                    case 16:
                        result = NetworkText.FromKey("DeathText.Inferno", deadPlayerName);
                        break;

                    case 254:
                        result = NetworkText.Empty;
                        break;

                    case 255:
                        result = NetworkText.FromKey("DeathText.Slain", deadPlayerName);
                        break;
                }
            }

            return result;
        }
    }
}