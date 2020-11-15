using Terraria;
using Terraria.ID;

namespace Terrexpansion.Content.Items.Accessories.Misc
{
    //[AutoloadEquip(EquipType.Balloon)]
    public class BundleofBundleofBalloons : BaseItem
    {
        public override string Texture => "Terrexpansion/Content/Items/MysteryItem";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bundle of Bundle of Balloons");
            Tooltip.SetDefault("Allows the holde to quintuple jump" +
                "\nReleases bees and douses the user in honey when damaged" +
                "\nIncreases jump height" +
                "\nNegates fall damage");
        }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Yellow;
            item.value = Item.buyPrice(platinum: 1, gold: 5);
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.hasJumpOption_Cloud = true;
            player.hasJumpOption_Blizzard = true;
            player.hasJumpOption_Fart = true;
            player.hasJumpOption_Sail = true;
            player.hasJumpOption_Sandstorm = true;
            player.releaseBeesWhenHurt = true;
            player.jumpBoost = true;
            player.noFallDmg = true;
        }

        public override void AddRecipes() => CreateRecipe()
                .AddIngredient(ItemID.BundleofBalloons)
                .AddIngredient(ItemID.BlueHorseshoeBalloon)
                .AddIngredient(ItemID.BalloonHorseshoeHoney)
                .AddIngredient(ItemID.BalloonHorseshoeFart)
                .AddIngredient(ItemID.BalloonHorseshoeSharkron)
                .AddIngredient(ItemID.YellowHorseshoeBalloon)
                .AddIngredient(ItemID.WhiteHorseshoeBalloon)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
    }
}