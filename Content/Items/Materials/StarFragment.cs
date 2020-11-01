using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Items.Materials
{
    public class StarFragment : BaseItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Disappears after the sunrise" +
                "\n'Shards from the heavens'");

            ItemID.Sets.ItemIconPulse[Type] = true;
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(silver: 1);
            item.maxStack = 99;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Lighting.AddLight((int)((item.position.X + item.width) / 16f), (int)((item.position.Y + item.height / 2) / 16f), 0.8f, 0.7f, 0.1f);

            if (item.timeSinceItemSpawned % 12 == 0)
            {
                Dust dust = Dust.NewDustPerfect(item.Center + new Vector2(0f, item.height * 0.2f) + Main.rand.NextVector2CircularEdge(item.width, item.height * 0.6f) * (0.3f + Main.rand.NextFloat() * 0.5f), 228, new Vector2(0f, (0f - Main.rand.NextFloat()) * 0.3f - 1.5f), 127);
                dust.scale = 0.5f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
            }

            if (Main.dayTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(item.position, item.width, item.height, 15, item.velocity.X, item.velocity.Y, 150, default, 1.2f);
                }

                for (int l = 0; l < 3; l++)
                {
                    Gore.NewGore(item.position, new Vector2(item.velocity.X, item.velocity.Y), Main.rand.Next(16, 18));
                }

                item.active = false;
                item.type = ItemID.None;
                item.stack = 0;

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncItem, number: item.whoAmI);
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.FallenStar)
                .Register();
        }
    }
}