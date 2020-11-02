using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Items.Accessories.Misc
{
    //[AutoloadEquip(new EquipType[3] { EquipType.Shoes, EquipType.HandsOn, EquipType.HandsOff })]
    public class AmphibianGear : BaseItem
    {
        public override string Texture => "Terrexpansion/Content/Items/MysteryItem";

        public override void SetStaticDefaults() => Tooltip.SetDefault("Grants the ability to swim" +
            "\nAllows the ability to climb walls" +
            "\nIncreases jump speed and allows auto-jump" +
            "\nIncreases fall resistance" +
            "\nThe wearer can run super fast" +
            "\n'It's 'p easy being green'");

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.rare = ItemRarityID.Pink;
            item.value = Item.sellPrice(gold: 7);
        }

        public override void UpdateEquip(Player player)
        {
            player.accRunSpeed = 6f;
            player.autoJump = true;
            player.jumpSpeedBoost += 2.4f;
            player.extraFall += 15;
            player.sailDash = true;
            player.frogLegJumpBoost = true;
            player.accFlipper = true;
            player.spikedBoots += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AmphibianBoots)
                .AddIngredient(ItemID.FrogGear)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}