using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terrexpansion.Common
{
    public class TerrePlayer : ModPlayer
    {
        public static int MaxStarFruit = 10;
        public static int ManaPerStar = Main.LocalPlayer.statManaMax2 <= 200 ? 20 : Main.LocalPlayer.statManaMax2 / 10;
        public static int ManaStarCount = Main.LocalPlayer.statManaMax2 / ManaPerStar;

        public bool cactusSetBonus = false;
        public bool vileVial = false;
        public bool extendedLungs = false;
        public bool currentlyRotated = false;
        public bool currentlyRotatedByToRotation = false;
        public bool wasAirborn = false;
        public bool lerpingToRotation = false;
        public float correctToRotation = 0f;
        public int starFruit = 0;
        public int timeAirborne = 0;
        public int remainingDeadeyeBullets = 6;

        private int _origBreath;

        public override void ResetEffects()
        {
            _origBreath = player.breathMax - (extendedLungs ? 100 : 0);

            cactusSetBonus = false;

            vileVial = false;

            player.statManaMax2 += starFruit * 10;
            player.breathMax = _origBreath + (extendedLungs ? 100 : 0);
        }

        public override void clientClone(ModPlayer clientClone)
        {
            TerrePlayer clone = (TerrePlayer)clientClone;

            clone.starFruit = starFruit;
            clone.extendedLungs = extendedLungs;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            TerrePlayer clone = (TerrePlayer)clientPlayer;
            ModPacket packet = Mod.GetPacket();

            packet.Write(clone.starFruit);
            packet.Write(clone.extendedLungs);
            packet.Send();
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();

            packet.Write((byte)Terrexpansion.MessageType.SyncModPlayer);
            packet.Write((byte)player.whoAmI);
            packet.Send();
        }

        public override TagCompound Save() => new TagCompound
        {
            { "starFruit", starFruit },
            { "extendedLungs", extendedLungs }
        };

        public override void Load(TagCompound tag)
        {
            starFruit = tag.GetInt("starFruit");
            extendedLungs = tag.GetBool("extendedLungs");
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (cactusSetBonus)
            {
                player.ApplyDamageToNPC(npc, damage / 5, 5f, npc.position.X + npc.width / 2 < player.position.X + (player.width / 2) ? -1 : 1, crit: false);
            }
        }

        public override void ModifyWeaponDamage(Item item, ref Modifier damage, ref float flat)
        {
            if (item.DamageType == DamageClass.Summon)
            {
                if (vileVial)
                {
                    flat += 3f;
                }
            }
        }

        public override void OnRespawn(Player player)
        {
            player.statLife = player.statLifeMax2;
            player.statMana = player.statManaMax2;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            player.fullRotation = 0;
            Terrexpansion.DeathSplashText = Language.GetTextValue("Mods.Terrexpansion.DeathSplash." + Main.rand.Next(18), player.name);
            Terrexpansion.CoinSplashText = "Mods.Terrexpansion.CoinSplash." + Main.rand.Next(6);
        }

        public override void ModifyDrawLayers(IReadOnlyDictionary<string, IReadOnlyList<PlayerDrawLayer>> layers, PlayerDrawSet drawInfo)
        {
            if (!Main.gameMenu)
            {
                if (drawInfo.drawPlayer.fullRotation < MathHelper.ToRadians(90) && drawInfo.drawPlayer.fullRotation > MathHelper.ToRadians(-90))
                {
                    if (drawInfo.drawPlayer.direction == 1 && Main.MouseWorld.X > drawInfo.drawPlayer.position.X)
                    {
                        drawInfo.drawPlayer.headRotation = Utils.Clamp((Main.MouseWorld - player.Center).ToRotation(), -0.5f, 0.5f);
                    }
                    else if (drawInfo.drawPlayer.direction == -1 && Main.MouseWorld.X < drawInfo.drawPlayer.position.X)
                    {
                        drawInfo.drawPlayer.headRotation = Utils.Clamp((player.Center - Main.MouseWorld).ToRotation(), -0.5f, 0.5f);
                    }
                }
            }
        }

        public override void PostUpdate()
        {
            if (player.fullRotation % MathHelper.ToRadians(-360f) < 1 && player.fullRotation % MathHelper.ToRadians(-360f) > -1 && !lerpingToRotation)
            {
                player.fullRotation = 0;
                wasAirborn = false;
            }

            if (player.mount.Type == -1)
            {
                player.fullRotationOrigin = new Vector2(player.width / 2, player.height / 2);

                if (player.fullRotation != 0)
                {
                    currentlyRotated = true;
                }

                if (player.velocity.X != 0 && player.velocity.Y != 0)
                {
                    timeAirborne++;

                    if (timeAirborne > 60)
                    {
                        lerpingToRotation = true;
                        player.fullRotation = player.fullRotation.AngleLerp(player.velocity.ToRotation() + (float)Math.PI / 2f, 0.1f);
                        wasAirborn = true;
                    }
                    else
                    {
                        lerpingToRotation = false;
                        wasAirborn = false;
                    }
                }
                else
                {
                    lerpingToRotation = false;

                    if (player.direction == -1)
                    {
                        if (wasAirborn)
                        {
                            player.fullRotation = MathHelper.Lerp(player.fullRotation, 0f, -0.1f);
                        }
                        else
                        {
                            player.fullRotation = 0;
                            timeAirborne = 0;
                        }
                    }
                    else
                    {
                        if (wasAirborn)
                        {
                            player.fullRotation = MathHelper.Lerp(player.fullRotation, 0f, -0.1f);
                        }
                        else
                        {
                            player.fullRotation = 0;
                            timeAirborne = 0;
                        }
                    }

                    if (player.fullRotation == 0)
                    {
                        player.fullRotation += player.velocity.X / 7f;

                        if (player.fullRotation > MathHelper.ToRadians(player.velocity.X))
                        {
                            player.fullRotation = MathHelper.ToRadians(player.velocity.X);
                        }

                        if (player.fullRotation < MathHelper.ToRadians(-player.velocity.X))
                        {
                            player.fullRotation = -MathHelper.ToRadians(-player.velocity.X);
                        }
                    }
                }
            }
            else
            {
                if (currentlyRotated)
                {
                    player.fullRotation = 0f;
                    currentlyRotated = false;
                    wasAirborn = false;
                    lerpingToRotation = false;
                }
            }
        }

        public void DoModdedBootsEffect(Utils.TileActionAttempt theEffectMethod)
        {
            if (player.miscCounter % 2 == 0 && player.velocity.Y == 0f && player.grappling[0] == -1 && player.velocity.X != 0f)
            {
                theEffectMethod((int)player.Center.X / 16, (int)(player.position.Y + player.height - 1f) / 16);
            }
        }

        public bool PlaceMoreFlamesOnTiles(int x, int y)
        {
            Tile tile = Main.tile[x, y + 1];

            if (tile != null && tile.active() && tile.liquid <= 0 && WorldGen.SolidTileAllowBottomSlope(x, y + 1))
            {
                ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.FlameWaders, new ParticleOrchestraSettings
                {
                    PositionInWorld = new Vector2(x * 16 + 8, y * 16 + 16)
                }, player.whoAmI);
                ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.FlameWaders, new ParticleOrchestraSettings
                {
                    PositionInWorld = new Vector2(x * 16 + 8, y * 16 + 16)
                }, player.whoAmI);

                return true;
            }

            return false;
        }

        public bool PlaceMoreFlowersOnTiles(int x, int y)
        {
            Tile tile = Main.tile[x, y + 1];

            if (tile != null && tile.active() && tile.liquid <= 0 && WorldGen.SolidTileAllowBottomSlope(x, y + 1))
            {
                ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.FlameWaders, new ParticleOrchestraSettings
                {
                    PositionInWorld = new Vector2(x * 16 + 8, y * 16 + 16)
                }, player.whoAmI);
                ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.FlameWaders, new ParticleOrchestraSettings
                {
                    PositionInWorld = new Vector2(x * 16 + 8, y * 16 + 16)
                }, player.whoAmI);

                return true;
            }

            return false;
        }
    }
}