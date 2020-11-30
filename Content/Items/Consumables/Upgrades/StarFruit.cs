using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terrexpansion.Common.Systems.Players;

namespace Terrexpansion.Content.Items.Consumables.Upgrades
{
    public class StarFruit : BaseItem
    {
        public override bool AutosizeItem => false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Fruit");
            Tooltip.SetDefault("Permanently increases maximum mana by 10");

            ItemID.Sets.SortingPriorityBossSpawns[Type] = 17;
            ItemID.Sets.CanBeQuickusedOnGamepad[Type] = true;
            ItemID.Sets.IsFood[Type] = true;
            ItemID.Sets.FoodParticleColors[(short)Type] = new Color[3] { Color.LightSteelBlue, Color.Purple, Color.MediumPurple };

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = item.height = 18;
            item.useStyle = ItemUseStyleID.EatFood;
            item.useTime = item.useAnimation = 30;
            item.UseSound = SoundID.Item4;
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(gold: 2);
            item.useTurn = true;
        }

        public override bool CanUseItem(Player player) => player.statManaMax >= 200 && player.GetModPlayer<TerrePlayer>().starFruit < TerrePlayer.MAX_STAR_FRUIT;

        public override bool UseItem(Player player)
        {
            player.statManaMax2 += 10;
            player.statMana += 10;
            player.GetModPlayer<TerrePlayer>().starFruit += 1;

            if (Main.myPlayer == player.whoAmI)
            {
                player.ManaEffect(10);
            }

            AchievementsHelper.HandleSpecialEvent(player, 1); // Better to cover all ground, even if it doesn't matter.

            return true;
        }
    }
}