using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terrexpansion.Common
{
    public class TerrePlayer : ModPlayer
    {
        public static int MaxStarFruit = 10;

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
        public int remainingDeadeyeBullets = 0;

        private int _origBreath;

        public override void ResetEffects()
        {
            _origBreath = player.breathMax - (extendedLungs ? 100 : 0);

            #region Set Bonuses

            cactusSetBonus = false;

            #endregion Set Bonuses

            #region Accessories

            vileVial = false;

            #endregion Accessories

            #region Others

            player.statManaMax2 += starFruit * 10;
            player.breathMax = _origBreath + (extendedLungs ? 100 : 0);

            #endregion Others
        }

        #region Multiplayer, Saving & Other Syncing

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

        #endregion Multiplayer, Saving & Other Syncing

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
                    damage += 3;
                }
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            player.fullRotation = 0;
            Terrexpansion.DeathSplashText = Language.GetTextValue("Mods.Terrexpansion.DeathSplash." + Main.rand.Next(9));
            Terrexpansion.CoinSplashText = Language.GetText("Mods.Terrexpansion.CoinSplash." + Main.rand.Next(6));
        }

        public override void ModifyDrawLayers(IReadOnlyDictionary<string, IReadOnlyList<PlayerDrawLayer>> layers, PlayerDrawSet drawInfo)
        {
            if (!Main.gameMenu)
            {
                if (drawInfo.drawPlayer.fullRotation < MathHelper.ToRadians(90) && drawInfo.drawPlayer.fullRotation > MathHelper.ToRadians(-90) && !lerpingToRotation)
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
            if (player.fullRotation % MathHelper.ToRadians(-360f) < 1 && player.fullRotation % MathHelper.ToRadians(-360f) > -1)
            {
                player.fullRotation = 0;
                wasAirborn = false;
                lerpingToRotation = false;
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
                    lerpingToRotation = true;
                    timeAirborne++;

                    if (timeAirborne > 10)
                    {
                        player.fullRotation = player.velocity.ToRotation() + (float)Math.PI / 2f;

                        wasAirborn = true;
                    }
                    else
                    {
                        wasAirborn = false;
                    }
                }
                else
                {
                    if (player.direction == -1)
                    {
                        if (wasAirborn)
                        {
                            player.fullRotation = MathHelper.Lerp(player.fullRotation, 0f, 0.1f);
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
    }

    public static class PlayerExtensions
    {
        /// <summary>
        /// This extension method is made specifically for configs. If you're looking for the normal, non-config method, use <c>IsServerHost(this Player player)</c>.
        /// <para>Can be used to check whether or not a player is the host of a server.</para>
        /// <para>Set <c>changeMessageText</c> to <c>true</c> to set the config's message text to <c>Mods.Terrexpansion.NotServerHostError</c>.</para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="message"></param>
        /// <param name="changeMessageText"></param>
        /// <returns></returns>
        public static bool IsServerHost(this Player player, ref string message, bool changeMessageText = true)
        {
            if (IsServerHost(player))
            {
                return true;
            }

            message = changeMessageText ? Language.GetTextValue("Mods.Terrexpansion.NotServerHostError") : message;
            return false;
        }

        /// <summary>
        /// Can be used to check whether or not a player is the host of a server.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool IsServerHost(this Player player)
        {
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (Netplay.Clients[i].State == 10 && Main.player[i] == player && Netplay.Clients[i].Socket.GetRemoteAddress().IsLocalHost())
                {
                    return true;
                }
            }

            return true;
        }
    }
}