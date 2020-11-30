using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Dusts;

namespace Terrexpansion.Common.Data
{
    public struct NPCBloodData
    {
        /// <summary>
        /// Stores the blood data for vanilla NPCs. <br />
        /// Modded support pending.
        /// </summary>
        public static Dictionary<int, NPCBloodData> NPCBloodDict;

        public int dustType;
        public int minDust;
        public int maxDust;

        public NPCBloodData(int dustType, int minDust, int maxDust)
        {
            this.dustType = dustType;
            this.minDust = minDust;
            this.maxDust = maxDust;
        }

        internal static void InitializeBloodData()
        {
            NPCBloodDict = new Dictionary<int, NPCBloodData>();

            // TODO
            for (int i = NPCID.NegativeIDCount; i < NPCLoader.NPCCount; i++)
            {
                NPCBloodDict.Add(i, new NPCBloodData(ModContent.DustType<BaseBloodDust>(), 1, 10));
            }
        }

        /// <summary>
        /// Spawns dust with the specified randomization. Made specifically for blood/gore-style dust added by Terrexpansion. <br />
        /// There is no paramater to specify the dust type as it uses the dust in <see cref="NPCBloodDict"/>.
        /// </summary>
        /// <returns>The index of the spawned dust in the <see cref="Main.dust"/> array.</returns>
        public static int SpawnBlood(NPC npc, float speedXMin, float speedXMax, float speedYMin, float speedYMax, float scaleMin, float scaleMax)
        {
            if (NPCBloodDict[npc.netID].dustType != 0)
                return Dust.NewDust(npc.position, npc.width, npc.height, NPCBloodDict[npc.netID].dustType, Main.rand.NextFloat(speedXMin, speedXMax), Main.rand.NextFloat(speedYMin, speedYMax), Scale: Main.rand.NextFloat(scaleMin, scaleMax));

            return 0;
        }
    }
}
