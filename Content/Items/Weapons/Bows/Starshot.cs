using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Projectiles.Friendly.Arrows;

namespace Terrexpansion.Content.Items.Weapons.Bows
{
    public class Starshot : BaseItem
    {
        public override void SetStaticDefaults() => Tooltip.SetDefault("Wooden arrows turn into starrows" +
            "\n'Star-struck!'");

        public override void SafeSetDefaults()
        {
            item.useStyle = ItemUseStyleID.Shoot;
            item.useTime = item.useAnimation = 20;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.damage = 11;
            item.shootSpeed = 7.4f;
            item.noMelee = true;
            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Green;
            item.DamageType = DamageClass.Ranged;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<StarrowProj>();
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBow)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddIngredient(ItemID.SunplateBlock, 15)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.GoldBow)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddIngredient(ItemID.SunplateBlock, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}