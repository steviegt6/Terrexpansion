using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Items.Materials;

namespace Terrexpansion.Common.Systems.Globals.Projectiles
{
    public class TerreProjectile : GlobalProjectile
    {
        public override void Kill(Projectile projectile, int timeLeft)
        {
            switch (projectile.type)
            {
                case ProjectileID.FallingStar:
                    if (projectile.damage > 500)
                    {
                        int spawnAmount = Main.rand.Next(3);

                        if (spawnAmount > 0)
                        {
                            int spawnedFragmentItem = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ModContent.ItemType<StarFragment>(), spawnAmount);
                            Main.item[spawnedFragmentItem].velocity += new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-3, -1));

                            NetMessage.SendData(MessageID.SyncItem, number: spawnedFragmentItem, number2: 1f);
                        }
                    }
                    break;
            }
        }
    }
}