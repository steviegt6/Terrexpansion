using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terrexpansion.Common.Configs.ClientSide;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion.Common
{
    public class TerreSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int playerNamesIndex = layers.FindIndex(i => i.Name.Equals("Vanilla: MP Player Names"));
            int hotbarIndex = layers.FindIndex(x => x.Name.Equals("Vanilla: Hotbar"));

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
        }
    }
}