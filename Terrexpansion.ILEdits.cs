using MonoMod.Cil;
using Terraria.ModLoader;
using Terrexpansion.Common;
using Terrexpansion.Common.Utilities;
using OpCodes = Mono.Cecil.Cil.OpCodes;

namespace Terrexpansion
{
    partial class Terrexpansion
    {
        public void LoadILEdits()
        {
            IL.Terraria.Main.DrawInterface_35_YouDied += Main_DrawInterface_35_YouDied;
        }

        public void UnloadILEdits()
        {
            IL.Terraria.Main.DrawInterface_35_YouDied -= Main_DrawInterface_35_YouDied;
        }

        private void Main_DrawInterface_35_YouDied(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchLdsfld("Terraria.Lang", "inter")))
            {
                Logger.Warn("[IL] Could not match Ldsfld: \"Terraria.Lang\", \"inter\"!");

                return;
            }

            if (!c.TryGotoNext(i => i.MatchLdloc(1)))
            {
                Logger.Warn("[IL] Could not match Ldloc: 1 (value)! (1)");

                return;
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("DeathSplashText"));

            if (!c.TryGotoNext(i => i.MatchLdloc(1)))
            {
                Logger.Warn("[IL] Could not match Ldloc: 1 (value)! (2)");

                return;
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("DeathSplashText"));

            if (!c.TryGotoNext(i => i.MatchLdstr("Game.DroppedCoins")))
            {
                Logger.Warn("[IL] Could not match Ldstr: \"Game.DroppedCoins\"!");

                return;
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldsfld, typeof(Terrexpansion).GetStaticField("CoinSplashText"));
        }
    }
}
