using Terraria;
using Terraria.ID;
using Terrexpansion.Common;

namespace Terrexpansion.Content.Items.Consumables.Upgrades
{
    public class LungExtensionCard : BaseItem
    {
        public override void SetStaticDefaults() => Tooltip.SetDefault("Permanently increases your lung size" +
            "\n'This procedure is not legal, but I will do it for you.'");

        public override void SafeSetDefaults()
        {
            item.maxStack = 99;
            item.useAnimation = 30;
            item.useTime = 30;
            item.value = 0;
            item.useStyle = ItemUseStyleID.HoldUp;
            item.UseSound = SoundID.Item4;
            item.rare = ItemRarityID.Green;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player) => !player.GetModPlayer<TerrePlayer>().extendedLungs;

        public override bool UseItem(Player player)
        {
            player.breath += 100;
            player.breathMax += 100;
            player.GetModPlayer<TerrePlayer>().extendedLungs = true;

            return true;
        }
    }
}