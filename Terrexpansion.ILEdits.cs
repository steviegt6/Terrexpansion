using MonoMod.Cil;
using Terrexpansion.Common.Configs.ClientSide;
using Terrexpansion.Common.Utilities;
using OpCodes = Mono.Cecil.Cil.OpCodes;

namespace Terrexpansion
{
    partial class Terrexpansion
    {
        public void LoadILEdits()
        {
            IL.Terraria.Main.DrawInterface_35_YouDied += Main_DrawInterface_35_YouDied;
            IL.Terraria.Player.Update += Player_Update;
        }

        public void UnloadILEdits()
        {
            IL.Terraria.Main.DrawInterface_35_YouDied -= Main_DrawInterface_35_YouDied;
            IL.Terraria.Player.Update -= Player_Update;
        }

        private void Main_DrawInterface_35_YouDied(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchLdsfld("Terraria.Lang", "inter")))
            {
                Logger.Warn("[IL] Could not match Ldsfld: \"Terraria.Lang\", \"inter\"!");

                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
            {
                Logger.Info("[IL] Matched Ldsfld: \"Terraria.Land\", \"inter\"!");
            }

            if (!c.TryGotoNext(i => i.MatchLdloc(1)))
            {
                Logger.Warn("[IL] Could not match Ldloc: 1 (value)! (1)");

                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
            {
                Logger.Info("[IL] Matched Ldloc: 1 (value)! (1)");
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("DeathSplashText"));

            if (!c.TryGotoNext(i => i.MatchLdloc(1)))
            {
                Logger.Warn("[IL] Could not match Ldloc: 1 (value)! (2)");

                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
            {
                Logger.Info("[IL] Matched Ldloc: 1 (value)! (2)");
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("DeathSplashText"));

            if (!c.TryGotoNext(i => i.MatchLdstr("Game.DroppedCoins")))
            {
                Logger.Warn("[IL] Could not match Ldstr: \"Game.DroppedCoins\"!");

                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
            {
                Logger.Info("[IL] Matched Ldstr: \"Game.DroppedCoins\"!");
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("CoinSplashText"));
        }

        private void Player_Update(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchLdcI4(400)))
            {
                Logger.Warn("[IL] Could not match LdcI4: 400! (1)");

                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
            {
                Logger.Info("[IL] Matched LdcI4: 400! (1)");
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, int.MaxValue);

            if (!c.TryGotoNext(i => i.MatchLdcI4(400)))
            {
                Logger.Warn("[IL] Could not match LdcI4: 400! (2)");

                return;
            }
            else if (TerreConfigGenericClient.Instance.debugMode)
            {
                Logger.Info("[IL] Matched LdcI4: 400! (2)");
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, int.MaxValue);
        }
    }
}
