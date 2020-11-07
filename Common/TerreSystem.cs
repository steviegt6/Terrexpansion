using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terrexpansion.Assets;
using Terrexpansion.Common.Configs.ClientSide;
using Terrexpansion.Common.UI.States;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion.Common
{
    public class TerreSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int playerNamesIndex = layers.FindIndex(i => i.Name.Equals("Vanilla: MP Player Names"));
            int hotbarIndex = layers.FindIndex(x => x.Name.Equals("Vanilla: Hotbar"));
            int inventoryIndex = layers.FindIndex(l => l.Name.Equals("Vanilla: Inventory"));

            if (playerNamesIndex != -1)
            {
                layers.Insert(playerNamesIndex, new LegacyGameInterfaceLayer("Terrexpansion: Sonar Potion Item", delegate
                {
                    for (int i = 0; i < 20; i++)
                    {
                        PopupText text = Main.popupText[i];

                        if (text.sonar && text.active && text.context == PopupTextContext.SonarAlert)
                        {
                            ItemID.Search.TryGetId(text.name, out int itemID);

                            if (itemID <= 0)
                            {
                                for (int l = 0; l < ItemLoader.ItemCount; l++)
                                {
                                    Item item = new Item(l);
                                    if (item.AffixName() == text.name)
                                    {
                                        itemID = item.type;
                                        break;
                                    }
                                }
                            }

                            if (ModContent.GetInstance<TerreConfigClientSide>().sonarPotionItem)
                            {
                                if (ModContent.GetInstance<TerreConfigClientSide>().sonarPotionItemOutline)
                                {
                                    for (int k = 0; k < 4; k++)
                                    {
                                        Vector2 offsetPos = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * k) * 2;
                                        Main.spriteBatch.Draw(TextureAssets.Item[itemID].Value.ToFlatColor(Color.White, Main.spriteBatch.GraphicsDevice), new Vector2(text.position.X - Main.screenPosition.X + (int)(FontAssets.ItemStack.Value.MeasureString(text.name).X / 2f), text.position.Y - Main.screenPosition.Y - 5 - (TextureAssets.Item[itemID].Width() / 2f)) + offsetPos, null, Color.White, 0f, TextureAssets.Item[itemID].Size() / 2f, text.scale, SpriteEffects.None, 0f);
                                    }
                                }

                                Main.spriteBatch.Draw(TextureAssets.Item[itemID].Value, new Vector2(text.position.X - Main.screenPosition.X + (int)(FontAssets.ItemStack.Value.MeasureString(text.name).X / 2f), text.position.Y - Main.screenPosition.Y - 5 - (TextureAssets.Item[itemID].Width() / 2f)), null, Color.White, 0f, TextureAssets.Item[itemID].Size() / 2f, text.scale, SpriteEffects.None, 0f);
                            }
                        }
                    }

                    return true;
                }, InterfaceScaleType.UI));
            }

            if (hotbarIndex != -1)
            {
                layers.Insert(hotbarIndex, new LegacyGameInterfaceLayer("Terrexpansion: Minion Count", delegate
                {
                    if (!Main.playerInventory && !Main.LocalPlayer.ghost && (Main.LocalPlayer.HeldItem.DamageType == DamageClass.Summon || ModContent.GetInstance<TerreConfigClientSide>().forceMinionCounter))
                    {
                        string minionText = $"{Main.LocalPlayer.numMinions}/{Main.LocalPlayer.maxMinions} minions";

                        Main.spriteBatch.DrawString(FontAssets.MouseText.Value, minionText, new Vector2(5f, 0f), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default, 1f, SpriteEffects.None, 0f);
                    }

                    return true;
                }, InterfaceScaleType.UI));
            }

            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer("Terrexpansion: Synergy Menu Button", delegate
                {
                    if (Main.playerInventory)
                    {
                        Main.inventoryScale = 0.85f;

                        int num = 448 + AssetHelper.SynergyButton.Width() + 10;
                        int num2 = 258;

                        if ((Main.LocalPlayer.chest != -1 || Main.npcShop > 0) && !Main.recBigList)
                        {
                            num2 += 168;
                            num += 5;
                            Main.inventoryScale = 0.755f;
                        }

                        Rectangle rectangle = new Rectangle(num, num2, 30, 30);
                        bool flag = false;

                        if (rectangle.Contains(new Point(Main.mouseX, Main.mouseY)) && !PlayerInput.IgnoreMouseInterface)
                        {
                            Main.LocalPlayer.mouseInterface = true;
                            flag = true;

                            if (Main.mouseLeft && Main.mouseLeftRelease)
                            {
                                Main.LocalPlayer.SetTalkNPC(-1);
                                Main.npcChatCornerItem = 0;
                                Main.npcChatText = "";
                                Main.mouseLeftRelease = false;
                                SoundEngine.PlaySound(SoundID.MenuTick);
                                IngameFancyUI.OpenUIState(new UISynergiesMenu());
                            }
                        }

                        Vector2 position = rectangle.Center.ToVector2();
                        Rectangle rectangle2 = AssetHelper.SynergyButton.Frame(2, 1, flag ? 1 : 0);
                        rectangle.Width -= 2;
                        rectangle.Height -= 2;
                        Vector2 origin = rectangle2.Size() / 2f;
                        Main.spriteBatch.Draw(AssetHelper.SynergyButton.Value, position, rectangle2, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

                        if (!Main.mouseText && flag)
                        {
                            Main.instance.MouseText(Language.GetTextValue("Mods.Terrexpansion.GameUI.SynergyButton"));
                        }
                    }

                    return true;
                }, InterfaceScaleType.UI));
            }
        }
    }
}