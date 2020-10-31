using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
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
    }
}
