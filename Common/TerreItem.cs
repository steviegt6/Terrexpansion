using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Common.Configs.ClientSide;
using Terrexpansion.Common.Utilities;

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

        public override bool CanPickup(Item item, Player player)
        {
            foreach (int type in ModContent.GetInstance<TerreConfigClientSide>().blacklistItemsList)
            {
                if (item.type == type)
                {
                    return false;
                }
            }

            return base.CanPickup(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine damage = tooltips.GetVanillaTooltip("Damage");
            TooltipLine critChance = tooltips.GetVanillaTooltip("CritChance");
            TooltipLine favorite = tooltips.GetVanillaTooltip("Favorite");
            TooltipLine favoriteDesc = tooltips.GetVanillaTooltip("FavoriteDesc");
            TooltipLine speed = tooltips.GetVanillaTooltip("Speed");
            TooltipLine knockback = tooltips.GetVanillaTooltip("Knockback");

            if (ModContent.GetInstance<TerreConfigTooltips>().showTooltipDamageMods && damage != null)
            {
                Item dummyItem = new Item(item.type);

                int damageDifference = Main.LocalPlayer.GetWeaponDamage(item) - dummyItem.damage;

                if (damageDifference != 0)
                {
                    string damageModificationText = $" ({dummyItem.damage} {(damageDifference > 0 ? "+" : "-")} {(damageDifference < 0 ? -damageDifference : damageDifference)})";

                    if (ModContent.GetInstance<TerreConfigTooltips>().colorTooltipDamageMods)
                    {
                        damageModificationText = $"[c/{(damageDifference > 0 ? Colors.RarityGreen.Hex3() : Colors.RarityRed.Hex3())}:{damageModificationText}]";
                    }

                    damage.text += damageModificationText;
                }
            }

            if (ModContent.GetInstance<TerreConfigTooltips>().showTooltipCritMods && critChance != null)
            {
                Item dummyItem = new Item(item.type);

                int critDifference = Main.LocalPlayer.GetWeaponCrit(item) - (dummyItem.crit + 4);

                if (critDifference != 0)
                {
                    string critModificationText = $" ({dummyItem.crit + 4}% {(critDifference > 0 ? "+" : "-")} {(critDifference < 0 ? -critDifference : critDifference)}%)";

                    if (ModContent.GetInstance<TerreConfigTooltips>().colorTooltipCritMods)
                    {
                        critModificationText = $"[c/{(critDifference > 0 ? Colors.RarityGreen.Hex3() : Colors.RarityRed.Hex3())}:{critModificationText}]";
                    }

                    critChance.text += critModificationText;
                }
            }

            if (ModContent.GetInstance<TerreConfigTooltips>().colorTooltipFavorites)
            {
                if (favorite != null)
                {
                    favorite.overrideColor = Main.OurFavoriteColor;
                }

                if (favoriteDesc != null)
                {
                    favoriteDesc.overrideColor = Main.OurFavoriteColor;
                }
            }

            if (ModContent.GetInstance<TerreConfigTooltips>().showTooltipUseTime && speed != null)
            {
                speed.text += $" ({item.useTime})";
            }


            if (ModContent.GetInstance<TerreConfigTooltips>().showTooltipKnockback && knockback != null)
            {
                knockback.text += $" ({Main.LocalPlayer.GetWeaponKnockback(item, item.knockBack)})";
            }
        }
    }
}