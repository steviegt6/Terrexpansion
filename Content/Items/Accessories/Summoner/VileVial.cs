using Terraria;
using Terraria.ID;
using Terrexpansion.Common.Systems.Players;

namespace Terrexpansion.Content.Items.Accessories.Summoner
{
    public class VileVial : BaseItem
    {
        public override void SetStaticDefaults() => Tooltip.SetDefault("+3 summon damage" +
            "\nSummon weapons become corrosive");

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(gold: 1);
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<TerrePlayer>().vileVial = true;
    }
}