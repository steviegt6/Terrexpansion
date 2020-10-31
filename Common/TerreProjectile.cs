using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Items.Materials;

namespace Terrexpansion.Common
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
                            int spawnedFragment = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ModContent.ItemType<StarFragment>(), spawnAmount);
                            Main.item[spawnedFragment].velocity.X += Main.rand.Next(-5, 5);
                            Main.item[spawnedFragment].velocity.Y -= Main.rand.Next(1, 3);

                            NetMessage.SendData(MessageID.SyncItem, number: spawnedFragment, number2: 1f);
                        }
                    }
                    break;
            }
        }
    }
}