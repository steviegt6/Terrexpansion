using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Rarities;

namespace Terrexpansion.Content.Items.Accessories.Misc
{
    public class Aedificate : BaseItem
    {
        public override string Texture => "Terrexpansion/Content/Items/MysteryItem";

        public override void SetStaticDefaults() => Tooltip.SetDefault("Increases block placement & tool range by 8" +
            "\nIncreases block & wall placement speed" +
            "\nAutomatically paints placed objects" +
            "\nAutomatically places actuators on placed objects" +
            "\nIncreases mining speed by 25%" +
            "\nIncreases coin pickup range and shop prices lowered by 20%" +
            "\nHitting enemies will sometimes drop extra coins" +
            "\nAllows the collection of Vine Ropes from vines" +
            "\nEnables Echo Sight, showing hidden blocks");

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.rare = ModContent.RarityType<ZenithRarity>();
            item.value = Item.sellPrice(gold: 50, silver: 50);
        }

        public override void UpdateEquip(Player player)
        {
            player.CanSeeInvisibleBlocks = true;
            player.treasureMagnet = true;
            player.cordage = true;
            player.goldRing = true;
            player.hasLuckyCoin = true;
            player.discount = true;
            player.pickSpeed -= 0.25f;
            player.autoActuator = true;
            player.autoPaint = true;
            player.equippedAnyWallSpeedAcc = true;
            player.equippedAnyTileSpeedAcc = true;
            player.equippedAnyTileRangeAcc = true;

            if (player.whoAmI == Main.myPlayer)
            {
                Player.tileRangeX += 5;
                Player.tileRangeY += 5;
            }
        }

        public override void AddRecipes() => CreateRecipe()
                .AddIngredient(ItemID.SpectreGoggles)
                .AddIngredient(ItemID.CordageGuide)
                .AddIngredient(ItemID.GreedyRing)
                .AddIngredient(ItemID.AncientChisel)
                .AddIngredient(ItemID.ActuationAccessory)
                .AddIngredient(ItemID.ArchitectGizmoPack)
                .AddIngredient(ItemID.Toolbox)
                .AddIngredient(ItemID.Toolbelt)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
    }
}