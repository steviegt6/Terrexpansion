using Microsoft.Xna.Framework;
using MonoMod.Cil;
using ReLogic.Graphics;
using System;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terrexpansion.Common.Configs.ClientSide;
using Terrexpansion.Common.Utilities;
using OpCodes = Mono.Cecil.Cil.OpCodes;

// This code will remain more or less uncommented.
namespace Terrexpansion
{
    public partial class Terrexpansion
    {
        public void LoadILEdits()
        {
            IL.Terraria.Main.DrawInterface_35_YouDied += Main_DrawInterface_35_YouDied;
            IL.Terraria.Player.Update += Player_Update;
            IL.Terraria.Main.PreDrawMenu += Main_PreDrawMenu;
            IL.Terraria.GameContent.UI.BigProgressBar.BigProgressBarSystem.Draw += BigProgressBarSystem_Draw;
        }

        public void UnloadILEdits()
        {
            IL.Terraria.Main.DrawInterface_35_YouDied -= Main_DrawInterface_35_YouDied;
            IL.Terraria.Player.Update -= Player_Update;
            IL.Terraria.Main.PreDrawMenu -= Main_PreDrawMenu;
            IL.Terraria.GameContent.UI.BigProgressBar.BigProgressBarSystem.Draw -= BigProgressBarSystem_Draw;
        }

        private void Main_DrawInterface_35_YouDied(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchLdsfld("Terraria.Lang", "inter")))
            {
                Logger.Warn("[IL] Main.DrawInterface_35_YouDied: Could not match Ldsfld: \"Terraria.Lang\", \"inter\"!");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Main.DrawInterface_35_YouDied: Matched Ldsfld: \"Terraria.Lang\", \"inter\"!");

            if (!c.TryGotoNext(i => i.MatchLdloc(1)))
            {
                Logger.Warn("[IL] Main.DrawInterface_35_YouDied: Could not match Ldloc: 1 (value)! (1)");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Main.DrawInterface_35_YouDied: Matched Ldloc: 1 (value)! (1)");

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("DeathSplashText"));

            if (!c.TryGotoNext(i => i.MatchLdloc(1)))
            {
                Logger.Warn("[IL] Main.DrawInterface_35_YouDied: Could not match Ldloc: 1 (value)! (2)");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Main.DrawInterface_35_YouDied: Matched Ldloc: 1 (value)! (2)");

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("DeathSplashText"));

            if (!c.TryGotoNext(i => i.MatchLdstr("Game.DroppedCoins")))
            {
                Logger.Warn("[IL] Main.DrawInterface_35_YouDied: Could not match Ldstr: \"Game.DroppedCoins\"!");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Main.DrawInterface_35_YouDied: Matched Ldstr: \"Game.DroppedCoins\"!");

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("CoinSplashText"));

            if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Successfully done editing Main.DrawInterface_35_YouDied!");
        }

        private void Player_Update(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchLdcI4(400)))
            {
                Logger.Warn("[IL] Player.Update: Could not match LdcI4: 400! (1)");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Player.Update: Matched LdcI4: 400! (1)");

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, int.MaxValue);

            if (!c.TryGotoNext(i => i.MatchLdcI4(400)))
            {
                Logger.Warn("[IL] Player.Update: Could not match LdcI4: 400! (2)");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Player.Update: Matched LdcI4: 400! (2)");

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, int.MaxValue);

            if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Successfully done editing Player.Update!");
        }

        private void Main_PreDrawMenu(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchLdloc(0)))
            {
                Logger.Warn("[IL] Main.PreDrawMenu: Could not match Ldloc: 0! (1)");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Main.PreDrawMenu: Matched Ldloc: 0! (1)");

            if (!c.TryGotoNext(i => i.MatchLdloc(0)))
            {
                Logger.Warn("[IL] Main.PreDrawMenu: Could not match Ldloc: 0! (2)");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Main.PreDrawMenu: Matched Ldloc: 0! (2)");

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_R4, 1f);

            if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Successfully done editing Main.PreDrawMenu!");
        }

        private void BigProgressBarSystem_Draw(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchCallvirt("Terraria.GameContent.UI.BigProgressBar.IBigProgressBar", "Draw")))
            {
                Logger.Warn("[IL] BigProgressBarSystem.Draw: Could not match Callvirt \"Terraria.GameContent.UI.BigProgressBar.IBigProgressBar\", \"Draw\"!");
                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] BigProgressBarSystem.Draw: Matched Callvirt \"Terraria.GameContent.UI.BigProgressBar.IBigProgressBar\", \"Draw\"!");

            c.Index += 2;

            c.Emit(OpCodes.Pop);
            c.EmitDelegate<Action>(() =>
            {
                Type bigProgressBarInfoType = tmlAssembly.GetCachedType("Terraria.GameContent.UI.BigProgressBar.BigProgressBarInfo.BigBossBarInfo");
                Object info = tmlAssembly.GetCachedType("Terraria.GameContent.UI.BigProgressBar.BigProgressBarSystem").GetInstanceField("_info").GetValue(Main.BigBossProgressBar);
                NPC npc = Main.npc[(int)bigProgressBarInfoType.GetInstanceField("npcIndexToAimAt").GetValue(info)];


                Main.spriteBatch.DrawString(FontAssets.ItemStack.Value, $"{npc.life}/{npc.lifeMax}", Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), Color.White);
                Main.NewText($"{npc.life}/{npc.lifeMax}");
            });
            c.Emit(OpCodes.Ret);

            if (TerreConfigGenericClient.Instance.debugMode)
                Logger.Info("[IL] Successfully done editing BigProgressBarSystem.Draw!");
        }
    }
}