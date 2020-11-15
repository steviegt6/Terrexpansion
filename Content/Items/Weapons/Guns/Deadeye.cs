using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Common;
using Terrexpansion.Common.Players;
using Terrexpansion.Content.Projectiles.Friendly.Boomerangs;

namespace Terrexpansion.Content.Items.Weapons.Guns
{
    public class Deadeye : BaseItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dead-eye");
            Tooltip.SetDefault("Can fire 6 shots before being thrown like a boomerang" +
                "\n'Cleans everything up nicely!'");
        }

        public override void SafeSetDefaults()
        {
            item.useAmmo = AmmoID.Bullet;
            item.autoReuse = false;
            item.useStyle = ItemUseStyleID.Shoot;
            item.useAnimation = item.useTime = 10;
            item.shoot = ProjectileID.Bullet;
            item.knockBack = 5f;
            item.UseSound = SoundID.Item40;
            item.damage = 20;
            item.shootSpeed = 11f;
            item.noMelee = true;
            item.value = 1;
            item.scale = 0.9f;
            item.rare = ItemRarityID.Green;
            item.DamageType = DamageClass.Ranged;
            item.noUseGraphic = false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<TerrePlayer>().remainingDeadeyeBullets > 0)
            {
                player.GetModPlayer<TerrePlayer>().remainingDeadeyeBullets--;
            }
            else if (player.GetModPlayer<TerrePlayer>().remainingDeadeyeBullets <= 0)
            {
                player.GetModPlayer<TerrePlayer>().remainingDeadeyeBullets = 6;
                type = ModContent.ProjectileType<DeadeyeProj>();
            }

            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<TerrePlayer>().remainingDeadeyeBullets > 0)
            {
                item.useAmmo = AmmoID.Bullet;
                item.autoReuse = false;
                item.useStyle = ItemUseStyleID.Shoot;
                item.useAnimation = item.useTime = 10;
                item.shoot = ProjectileID.Bullet;
                item.knockBack = 5f;
                item.UseSound = SoundID.Item40;
                item.damage = 20;
                item.shootSpeed = 11f;
                item.noMelee = true;
                item.value = 1;
                item.scale = 0.9f;
                item.rare = ItemRarityID.Green;
                item.DamageType = DamageClass.Ranged;
                item.noUseGraphic = false;
            }
            else if (player.GetModPlayer<TerrePlayer>().remainingDeadeyeBullets <= 0)
            {
                item.useAmmo = AmmoID.None;
                item.noMelee = true;
                item.useStyle = ItemUseStyleID.Swing;
                item.shootSpeed = 7f;
                item.shoot = ModContent.ProjectileType<DeadeyeProj>();
                item.damage = 32;
                item.knockBack = 6f;
                item.UseSound = SoundID.Item1;
                item.noUseGraphic = true;
            }
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<DeadeyeProj>()] < 1;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bone, 20)
                .AddRecipeGroup("Terrexpansion:RottenChunkVertebrae", 10)
                .AddIngredient(ItemID.Handgun)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}