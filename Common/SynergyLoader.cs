using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Terrexpansion.Common
{
    public static class SynergyLoader
    {
        public static int SynergyCount => synergies.Count;
        internal static readonly List<Synergy> synergies = new List<Synergy>();

        internal static int Add(Synergy synergy)
        {
            if (ModNet.AllowVanillaClients)
            {
                throw new Exception("Added synergies breaks vanilla client compatibility. Why are you using a mod anyway?");
            }

            synergies.Add(synergy);
            return SynergyCount - 1;
        }

        internal static Synergy GetSynergy(int type) => type > 0 && type < SynergyCount ? synergies[type] : null;

        internal static void Unload() => synergies.Clear();
    }
}
