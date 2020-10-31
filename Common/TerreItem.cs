using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrexpansion.Common
{
    public class TerreItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            switch (item.type)
            {
                case ItemID.EndlessQuiver:
                    item.SetNameOverride("Endless Wooden Quiver");
                    break;

                case ItemID.CactusPickaxe:
                    item.pick = 30;
                    break;

                case ItemID.ReaverShark:
                    item.pick = 80;
                    break;

                case ItemID.LifeFruit:
                    item.useStyle = ItemUseStyleID.EatFood;
                    item.useTurn = true;
                    item.useTime = item.useAnimation = 30;

                    ItemID.Sets.FoodParticleColors[(short)item.type] = new Color[3] { Color.LightGreen, Color.Gold, Color.Yellow };
                    ItemID.Sets.IsFood[item.type] = true;

                    Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(int.MaxValue, 3));
                    break;
            }
        }

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ItemID.CactusHelmet && body.type == ItemID.CactusBreastplate && legs.type == ItemID.CactusLeggings)
            {
                return "Cactus";
            }

            return base.IsArmorSet(head, body, legs);
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            switch (set)
            {
                case "Cactus":
                    //player.setBonus += "\nAttackers will be pricked";
                    //player.GetModPlayer<TerrePlayer>().cactusSetBonus = true;
                    break;
            }
        }
    }
}