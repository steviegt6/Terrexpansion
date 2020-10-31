using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Items.Materials;
using Terrexpansion.Content.Projectiles.Friendly.Arrows;

namespace Terrexpansion.Content.Items.Ammo.Arrows
{
    public class Starrow : BaseItem
    {
        public override void SafeSetDefaults()
        {
            item.shootSpeed = 2f;
            item.shoot = ModContent.ProjectileType<StarrowProj>();
            item.damage = 7;
            item.maxStack = 999;
            item.consumable = true;
            item.ammo = AmmoID.Arrow;
            item.knockBack = 3.5f;
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(copper: 5);
            item.DamageType = DamageClass.Ranged;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ModContent.ItemType<StarFragment>())
                .AddIngredient(ItemID.WoodenArrow, 5)
                .Register();
        }
    }
}