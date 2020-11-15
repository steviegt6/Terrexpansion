using Terraria;
using Terraria.ID;

namespace Terrexpansion.Content.Items.Accessories.Misc
{
    public class FlyingStepTube : BaseItem
    {
        public override string Texture => "Terrexpansion/Content/Items/MysteryItem";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flying Step-Tube");
            Tooltip.SetDefault("Hold UP to reach higher" +
                "\nAllows the owner to float for a few seconds" +
                "\nGrants the ability to float in water");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.rare = ItemRarityID.Green;
            item.value = Item.sellPrice(gold: 3);
        }

        public override void UpdateEquip(Player player)
        {
            player.carpet = true;
            player.hasFloatingTube = true;
            player.canFloatInWater = true;
            player.portableStoolInfo.SetStats(26, 26, 26);
        }
    }
}