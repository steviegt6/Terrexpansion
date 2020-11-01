using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terrexpansion.Common.Configs.ClientSide;

namespace Terrexpansion.Common
{
    public class TerreSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int hotbarIndex = layers.FindIndex(x => x.Name.Equals("Vanilla: Hotbar"));

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