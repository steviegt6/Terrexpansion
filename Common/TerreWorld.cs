using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Items.Weapons.Bows;
using Terrexpansion.Content.Projectiles.Helpers;

namespace Terrexpansion.Common
{
    public class TerreWorld : ModWorld
    {
        public override void PostUpdate() => SpawnStarFragments();

        public override void PostWorldGen() => ReplaceStarfuryInChests();

        public static void SpawnStarFragments()
        {
            for (int l = 0; l < Main.dayRate; l++)
            {
                if (!(Main.rand.Next(8000) < 10f * (Main.maxTilesX / 4200f)))
                {
                    continue;
                }

                Vector2 position = new Vector2((Main.rand.Next(Main.maxTilesX - 50) + 100) * 16, Main.rand.Next((int)(Main.maxTilesY * 0.05)));
                int selectedPlayer = -1;

                if (Main.expertMode && Main.rand.NextBool(15))
                {
                    int closestPlayer = Player.FindClosest(position, 1, 1);

                    if (Main.player[closestPlayer].position.Y < Main.worldSurface * 16.0 && Main.player[closestPlayer].afkCounter < 3600)
                    {
                        int num = Main.rand.Next(1, 640);
                        position.X = Main.player[closestPlayer].position.X + Main.rand.Next(-num, num + 1);
                        selectedPlayer = closestPlayer;
                    }
                }

                if (!Collision.SolidCollision(position, 16, 16))
                {
                    float velocityX = Main.rand.Next(-100, 101);
                    float velocityY = Main.rand.Next(200) + 100;
                    float velocityMultiplier = 12 / (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
                    velocityX *= velocityMultiplier;
                    velocityY *= velocityMultiplier;

                    Projectile.NewProjectile(position, new Vector2(velocityX, velocityY), ModContent.ProjectileType<StarFragmentProjSpawner>(), 0, 0f, Main.myPlayer, ai1: selectedPlayer);
                }
            }
        }

        public static void ReplaceStarfuryInChests()
        {
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];

                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 13 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.Starfury)
                        {
                            chest.item[inventoryIndex].SetDefaults(Main.rand.Next(new int[] { ItemID.Starfury, ModContent.ItemType<Starshot>() }));
                        }
                    }
                }
            }
        }
    }
}