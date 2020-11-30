using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terrexpansion.Common.Configs.ClientSide;

namespace Terrexpansion.Common.Systems.Players
{
    public partial class TerrePlayer
    {
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (!Main.gameMenu)
                if (TerreConfigGenericClient.Instance.dynamicMovement)
                {
                    if (player.fullRotation < MathHelper.ToRadians(90) && player.fullRotation > MathHelper.ToRadians(-90))
                    {
                        if (player.direction == 1 && Main.MouseWorld.X > player.position.X)
                            player.headRotation = Utils.Clamp((Main.MouseWorld - player.Center).ToRotation(), -0.5f, 0.5f);
                        else if (player.direction == -1 && Main.MouseWorld.X < player.position.X)
                            player.headRotation = Utils.Clamp((player.Center - Main.MouseWorld).ToRotation(), -0.5f, 0.5f);
                    }
                }
        }

        public override void PostUpdate()
        {
            if (TerreConfigGenericClient.Instance.dynamicMovement)
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
                        currentlyRotated = true;

                    if ((player.velocity.X != 0 && player.velocity.Y != 0) || (player.velocity.Y != 0 && timeAirborne > 60))
                    {
                        timeAirborne++;

                        if (timeAirborne > 60)
                            player.fullRotation = player.fullRotation.AngleLerp(player.velocity.ToRotation() + (float)Math.PI / 2f, 0.05f);

                        lerpingToRotation = timeAirborne > 60;
                        wasAirborn = timeAirborne > 60;
                    }
                    else
                    {
                        timeNotAirborne++;
                        lerpingToRotation = false;

                        if (player.direction == -1)
                        {
                            if (wasAirborn)
                                player.fullRotation = MathHelper.Lerp(player.fullRotation, 0f, -0.085f);
                            else
                                player.fullRotation = timeAirborne = 0;
                        }
                        else
                        {
                            if (wasAirborn)
                                player.fullRotation = MathHelper.Lerp(player.fullRotation, 0f, -0.085f);
                            else
                                player.fullRotation = timeAirborne = 0;
                        }

                        if (player.fullRotation == 0)
                        {
                            player.fullRotation += player.velocity.X / 7f;

                            if (player.fullRotation > MathHelper.ToRadians(player.velocity.X))
                                player.fullRotation = MathHelper.ToRadians(player.velocity.X);

                            if (player.fullRotation < MathHelper.ToRadians(-player.velocity.X))
                                player.fullRotation = -MathHelper.ToRadians(-player.velocity.X);
                        }
                    }
                }
                else
                {
                    if (currentlyRotated)
                    {
                        player.fullRotation = 0f;
                        timeAirborne = 0;
                        timeNotAirborne = 0;
                        currentlyRotated = false;
                        wasAirborn = false;
                        lerpingToRotation = false;
                    }
                }
            }
        }
    }
}