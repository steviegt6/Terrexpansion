using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terrexpansion.Common.Data;
using Terrexpansion.Common.Systems.Players;
using Terrexpansion.Content.Items.Consumables.Upgrades;

namespace Terrexpansion.Common.Systems.Globals.NPCs
{
    public class TerreNPC : GlobalNPC
    {
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
            switch (npc.type)
            {
                case NPCID.Medusa:
                    if (!Main.hardMode)
                    {
                        npc.damage /= 2;
                        npc.life /= 2;
                    }
                    break;
            }
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
                        goto SkipGraveCode;
                }

                npc.DropTombstoneTownNPC(networkText);
            }

        SkipGraveCode:
            for (int i = 0; i < Main.rand.Next(NPCBloodData.NPCBloodDict[npc.netID].minDust, NPCBloodData.NPCBloodDict[npc.netID].maxDust) * (npc.boss ? 2 : 1); i++)
                NPCBloodData.SpawnBlood(npc, npc.boss ? -20 : -5, npc.boss ? 20 : 5, -10, 5, 1f, 1.3f);

            return base.CheckDead(npc);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < Main.rand.Next(NPCBloodData.NPCBloodDict[npc.netID].minDust, NPCBloodData.NPCBloodDict[npc.netID].maxDust); i++)
                NPCBloodData.SpawnBlood(npc, -3 * projectile.direction, 10 * projectile.direction, -3, 4, 0.8f, 1.2f);
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < Main.rand.Next(NPCBloodData.NPCBloodDict[npc.netID].minDust, NPCBloodData.NPCBloodDict[npc.netID].maxDust); i++)
                NPCBloodData.SpawnBlood(npc, -3, 10, -3, 4, 0.8f, 1.2f);
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.marble && !Main.hardMode)
                pool.Add(NPCID.Medusa, 0.1f);
        }

        public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
        {
            Main.spriteBatch.DrawString(FontAssets.MouseText.Value, $"{npc.life}/{npc.lifeMax}", npc.Center, Color.White, 0f, npc.Center / 2f, 10f, SpriteEffects.None, 1f);

            return base.DrawHealthBar(npc, hbPosition, ref scale, ref position);
        }
    }
}