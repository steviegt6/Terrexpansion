using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terrexpansion.Common.Players;
using Terrexpansion.Content.Dusts;
using Terrexpansion.Content.Items.Consumables.Upgrades;

namespace Terrexpansion.Common.Globals.NPCs
{
    public class TerreNPC : GlobalNPC
    {
        public static Dictionary<int, int> NPCBloodType;

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
                    {
                        goto SkipGraveCode;
                    }
                }

                npc.DropTombstoneTownNPC(networkText);
            }

        SkipGraveCode:

            for (int i = 0; i < Main.rand.Next(npc.boss ? 350 : 40, npc.boss ? 450 : 60); i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, NPCBloodType[npc.netID], Main.rand.Next(npc.boss ? -20 : -5, npc.boss ? 20 : 5), Main.rand.Next(-10, 5), Scale: Main.rand.NextFloat(1f, 1.3f));
            }

            return base.CheckDead(npc);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < Main.rand.Next(10, 20); i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, NPCBloodType[npc.netID], Main.rand.Next(-3, 10) * projectile.direction, Main.rand.Next(-3, 4), Scale: Main.rand.NextFloat(0.8f, 1.2f));
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < Main.rand.Next(10, 20); i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, NPCBloodType[npc.netID], Main.rand.Next(-3, 10) * player.direction, Main.rand.Next(-3, 4), Scale: Main.rand.NextFloat(0.8f, 1.2f));
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.marble && !Main.hardMode)
            {
                pool.Add(NPCID.Medusa, 0.1f);
            }
        }

        public static void InitializeBloodTypes()
        {
            NPCBloodType = new Dictionary<int, int>();

            for (int i = NPCID.NegativeIDCount; i < NPCLoader.NPCCount; i++)
            {
                switch (i)
                {
                    case NPCID.GreenSlime:
                    case NPCID.SlimeRibbonYellow:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeGreen>());
                        break;

                    case NPCID.BlueSlime:
                    case NPCID.KingSlime:
                    case NPCID.SlimeSpiked:
                    case NPCID.UmbrellaSlime:
                    case NPCID.SlimeMasked:
                    case NPCID.SlimeRibbonWhite:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeBlue>());
                        break;

                    case NPCID.RedSlime:
                    case NPCID.SlimeRibbonGreen:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeRed>());
                        break;

                    case NPCID.PurpleSlime:
                        NPCBloodType.Add(i, ModContent.DustType<SlimePurple>());
                        break;

                    case NPCID.YellowSlime:
                    case NPCID.SlimeRibbonRed:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeYellow>());
                        break;

                    case NPCID.BlackSlime:
                    case NPCID.MotherSlime:
                    case NPCID.BabySlime:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeBlack>());
                        break;

                    case NPCID.IceSlime:
                    case NPCID.SpikedIceSlime:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeIce>());
                        break;

                    case NPCID.SandSlime:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeSand>());
                        break;

                    case NPCID.JungleSlime:
                    case NPCID.SpikedJungleSlime:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeJungle>());
                        break;

                    case NPCID.LavaSlime:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeLava>());
                        break;

                    case NPCID.DungeonSlime:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeDungeon>());
                        break;

                    case NPCID.Pinky:
                        NPCBloodType.Add(i, ModContent.DustType<SlimePinky>());
                        break;

                    case NPCID.ToxicSludge:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeToxicSludge>());
                        break;

                    case NPCID.CorruptSlime:
                    case NPCID.Slimeling:
                    case NPCID.Slimer:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeCorrupt>());
                        break;

                    case NPCID.Crimslime:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeCrimslime>());
                        break;

                    case NPCID.Gastropod:
                    case NPCID.IlluminantSlime:
                    case NPCID.RainbowSlime:
                    case NPCID.QueenSlimeBoss:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeIlluminant>());
                        break;

                    case NPCID.QueenSlimeMinionBlue:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeCrystal>());
                        break;

                    case NPCID.QueenSlimeMinionPink:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeBouncy>());
                        break;

                    case NPCID.QueenSlimeMinionPurple:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeHeavenly>());
                        break;

                    case NPCID.HoppinJack:
                        NPCBloodType.Add(i, ModContent.DustType<SlimeHoppinJack>());
                        break;

                    case NPCID.AngryBones:
                    case NPCID.BoneSerpentBody:
                    case NPCID.BoneSerpentHead:
                    case NPCID.BoneSerpentTail:
                    case NPCID.CursedSkull:
                    case NPCID.DungeonGuardian:
                    case NPCID.GreekSkeleton:
                    case NPCID.Skeleton:
                    case NPCID.SporeSkeleton:
                    case NPCID.Tim:
                    case NPCID.UndeadMiner:
                    case NPCID.UndeadViking:
                    case NPCID.ArmoredSkeleton:
                    case NPCID.ArmoredViking:
                    case NPCID.BlueArmoredBones:
                    case NPCID.BlueArmoredBonesMace:
                    case NPCID.BlueArmoredBonesNoPants:
                    case NPCID.BlueArmoredBonesSword:
                    case NPCID.BoneLee:
                    case NPCID.DiabolistRed:
                    case NPCID.DiabolistWhite:
                    case NPCID.GiantCursedSkull:
                    case NPCID.HellArmoredBones:
                    case NPCID.HellArmoredBonesMace:
                    case NPCID.HellArmoredBonesSpikeShield:
                    case NPCID.HellArmoredBonesSword:
                    case NPCID.Necromancer:
                    case NPCID.NecromancerArmored:
                    case NPCID.RaggedCaster:
                    case NPCID.RaggedCasterOpenCoat:
                    case NPCID.RuneWizard:
                    case NPCID.RustyArmoredBonesAxe:
                    case NPCID.RustyArmoredBonesFlail:
                    case NPCID.RustyArmoredBonesSword:
                    case NPCID.RustyArmoredBonesSwordNoArmor:
                    case NPCID.SkeletonArcher:
                    case NPCID.SkeletonCommando:
                    case NPCID.SkeletonSniper:
                    case NPCID.TacticalSkeleton:
                    case NPCID.SkeletronHand:
                    case NPCID.SkeletronHead:
                        NPCBloodType.Add(i, ModContent.DustType<BoneGore>());
                        break;

                    default:
                        NPCBloodType.Add(i, ModContent.DustType<GoreBase>());
                        break;
                }
            }
        }
    }
}