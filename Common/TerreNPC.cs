using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terrexpansion.Content.Items.Consumables.Upgrades;

namespace Terrexpansion.Common
{
    public class TerreNPC : GlobalNPC
    {
        //todo change medusa head drop change to 10%

        public override void GetChat(NPC npc, ref string chat)
        {
            Player player = Main.player[npc.FindClosestPlayer()];

            switch (npc.type)
            {
                case NPCID.Nurse:
                    if (Main.rand.NextBool(75) && !player.GetModPlayer<TerrePlayer>().extendedLungs && !player.HasItem(ModContent.ItemType<LungExtensionCard>()))
                    {
                        Item.NewItem(player.position, ModContent.ItemType<LungExtensionCard>(), noGrabDelay: true);
                        chat = "Shh! Don't tell anyone I handed you this card.";
                    }
                    break;
            }

            CombatText.NewText(npc.Hitbox, Color.White, chat);
        }

        public override void SetDefaults(NPC npc)
        {
            /*Main.npcFrameCount[NPCID.EaterofSouls] = 4;

            switch (npc.type)
            {
                case NPCID.EaterofSouls:
                    npc.width = 46;
                    npc.height = 80;
                    break;
            }*/
        }

        public override bool CheckDead(NPC npc)
        {
            if (npc.townNPC && npc.type != NPCID.OldMan && npc.type != NPCID.SkeletonMerchant && npc.type != NPCID.Angler && npc.type != 663 && !NPCID.Sets.IsTownPet[npc.type])
            {
                NetworkText networkText = NetworkText.FromKey(Language.GetText("LegacyMisc.36").Key, npc.GetFullNetName());

                for (int i = 0; i < 255; i++)
                {
                    Player player = Main.player[i];

                    if (player != null && player.active && player.difficulty == 2)
                    {
                        return base.CheckDead(npc);;
                    }
                }

                npc.DropTombstoneTownNPC(networkText);
            }

            return base.CheckDead(npc);
        }
    }
}
