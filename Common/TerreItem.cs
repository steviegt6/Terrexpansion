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
            TerreConfigTooltips config = ModContent.GetInstance<TerreConfigTooltips>();

            if (!config.showItemName && tooltips.TryGetVanillaTooltip("ItemName", out TooltipLine itemName))
            {
                tooltips.Remove(itemName);
            }

            if (tooltips.TryGetVanillaTooltip("Favorite", out TooltipLine favorite))
            {
                if (config.colorTooltipFavorites)
                {
                    favorite.overrideColor = Main.OurFavoriteColor;
                }

                if (!config.showFavoriteText)
                {
                    tooltips.Remove(favorite);
                }
            }

            if (tooltips.TryGetVanillaTooltip("FavoriteDesc", out TooltipLine favoriteDesc))
            {
                if (config.colorTooltipFavorites)
                {
                    favoriteDesc.overrideColor = Main.OurFavoriteColor;
                }

                if (!config.showFavoriteDesc)
                {
                    tooltips.Remove(favoriteDesc);
                }
            }

            if (tooltips.TryGetVanillaTooltip("NoTransfer", out TooltipLine noTransfer))
            {
                if (!config.showPlacementBlock)
                {
                    tooltips.Remove(noTransfer);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Social", out TooltipLine social))
            {
                if (!config.showSocialText)
                {
                    tooltips.Remove(social);
                }
            }

            if (tooltips.TryGetVanillaTooltip("SocialDesc", out TooltipLine socialDesc))
            {
                if (!config.showSocialDesc)
                {
                    tooltips.Remove(socialDesc);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Damage", out TooltipLine damage))
            {
                if (config.showTooltipDamageMods)
                {
                    Item dummyItem = new Item(item.type);

                    int damageDifference = Main.LocalPlayer.GetWeaponDamage(item) - dummyItem.damage;

                    if (damageDifference != 0)
                    {
                        string damageModificationText = $" ({dummyItem.damage} {(damageDifference > 0 ? "+" : "-")} {(damageDifference < 0 ? -damageDifference : damageDifference)})";

                        if (config.colorTooltipDamageMods)
                        {
                            damageModificationText = $"[c/{(damageDifference > 0 ? Colors.RarityGreen.Hex3() : Colors.RarityRed.Hex3())}:{damageModificationText}]";
                        }

                        damage.text += damageModificationText;
                    }
                }

                if (!config.showDamage)
                {
                    tooltips.Remove(damage);
                }
            }

            if (tooltips.TryGetVanillaTooltip("CritChance", out TooltipLine critChance))
            {
                if (config.showTooltipCritMods)
                {
                    Item dummyItem = new Item(item.type);

                    int critDifference = Main.LocalPlayer.GetWeaponCrit(item) - (dummyItem.crit + 4);

                    if (critDifference != 0)
                    {
                        string critModificationText = $" ({dummyItem.crit + 4}% {(critDifference > 0 ? "+" : "-")} {(critDifference < 0 ? -critDifference : critDifference)}%)";

                        if (config.colorTooltipCritMods)
                        {
                            critModificationText = $"[c/{(critDifference > 0 ? Colors.RarityGreen.Hex3() : Colors.RarityRed.Hex3())}:{critModificationText}]";
                        }

                        critChance.text += critModificationText;
                    }
                }

                if (!config.showItemCritChance)
                {
                    tooltips.Remove(critChance);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Speed", out TooltipLine speed))
            {
                if (config.showTooltipUseAnimation)
                {
                    speed.text += $" ({item.useTime})";
                }

                if (!config.showUseAnimation)
                {
                    tooltips.Remove(speed);
                }
            }


            if (tooltips.TryGetVanillaTooltip("Knockback", out TooltipLine knockback))
            {
                if (config.showTooltipKnockback)
                {
                    knockback.text += $" ({Main.LocalPlayer.GetWeaponKnockback(item, item.knockBack)})";
                }

                if (!config.showKnockback)
                {
                    tooltips.Remove(knockback);
                }
            }

            if (tooltips.TryGetVanillaTooltip("FishingPower", out TooltipLine fishingPower))
            {
                if (!config.showFishingPower)
                {
                    tooltips.Remove(fishingPower);
                }
            }

            if (tooltips.TryGetVanillaTooltip("NeedsBait", out TooltipLine needsBait))
            {
                if (!config.showNeedsBait)
                {
                    tooltips.Remove(needsBait);
                }
            }

            if (tooltips.TryGetVanillaTooltip("BaitPower", out TooltipLine baitPower))
            {
                if (!config.showBaitPower)
                {
                    tooltips.Remove(baitPower);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Equipable", out TooltipLine equipable))
            {
                if (!config.showEquip)
                {
                    tooltips.Remove(equipable);
                }
            }

            if (tooltips.TryGetVanillaTooltip("WandConsumes", out TooltipLine wandConsumes))
            {
                if (!config.showWandConsumes)
                {
                    tooltips.Remove(wandConsumes);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Quest", out TooltipLine quest))
            {
                if (!config.showQuest)
                {
                    tooltips.Remove(quest);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Vanity", out TooltipLine vanity))
            {
                if (!config.showVanity)
                {
                    tooltips.Remove(vanity);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Defense", out TooltipLine defense))
            {
                if (!config.showDefense)
                {
                    tooltips.Remove(defense);
                }
            }

            if (tooltips.TryGetVanillaTooltip("PickPower", out TooltipLine pickPower))
            {
                if (!config.showPickPower)
                {
                    tooltips.Remove(pickPower);
                }
            }

            if (tooltips.TryGetVanillaTooltip("AxePower", out TooltipLine axePower))
            {
                if (!config.showAxePower)
                {
                    tooltips.Remove(axePower);
                }
            }

            if (tooltips.TryGetVanillaTooltip("HammerPower", out TooltipLine hammerPower))
            {
                if (!config.showHammerPower)
                {
                    tooltips.Remove(hammerPower);
                }
            }

            if (tooltips.TryGetVanillaTooltip("TileBoost", out TooltipLine tileBoost))
            {
                if (!config.showTileBoost)
                {
                    tooltips.Remove(tileBoost);
                }
            }

            if (tooltips.TryGetVanillaTooltip("HealLife", out TooltipLine healLife))
            {
                if (!config.showHealLife)
                {
                    tooltips.Remove(healLife);
                }
            }

            if (tooltips.TryGetVanillaTooltip("HealMana", out TooltipLine healMana))
            {
                if (!config.showHealMana)
                {
                    tooltips.Remove(healMana);
                }
            }

            if (tooltips.TryGetVanillaTooltip("UseMana", out TooltipLine useMana))
            {
                if (!config.showUseMana)
                {
                    tooltips.Remove(useMana);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Placeable", out TooltipLine placeable))
            {
                if (!config.showPlaceable)
                {
                    tooltips.Remove(placeable);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Ammo", out TooltipLine ammo))
            {
                if (!config.showAmmo)
                {
                    tooltips.Remove(ammo);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Consumable", out TooltipLine consumable))
            {
                if (!config.showConsumable)
                {
                    tooltips.Remove(consumable);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Material", out TooltipLine material))
            {
                if (!config.showMaterial)
                {
                    tooltips.Remove(material);
                }
            }

            if (tooltips.TryGetVanillaTooltip("EtherianManaWarning", out TooltipLine etherianManaWarning))
            {
                if (!config.showEtherianManaWarning)
                {
                    tooltips.Remove(etherianManaWarning);
                }
            }

            if (tooltips.TryGetVanillaTooltip("WellFedExpert", out TooltipLine wellFedExpert))
            {
                if (!config.showWellFedExpert)
                {
                    tooltips.Remove(wellFedExpert);
                }
            }

            if (tooltips.TryGetVanillaTooltip("BuffTime", out TooltipLine buffTime))
            {
                if (!config.showBuffTime)
                {
                    tooltips.Remove(buffTime);
                }
            }

            if (tooltips.TryGetVanillaTooltip("OneDropLogo", out TooltipLine oneDropLogo))
            {
                if (!config.showOneDropLogo)
                {
                    tooltips.Remove(oneDropLogo);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Expert", out TooltipLine expert))
            {
                if (!config.showExpert)
                {
                    tooltips.Remove(expert);
                }
            }

            if (tooltips.TryGetVanillaTooltip("Master", out TooltipLine master))
            {
                if (!config.showMaster)
                {
                    tooltips.Remove(master);
                }
            }

            if (tooltips.TryGetVanillaTooltip("JourneyResearch", out TooltipLine journeyResearch))
            {
                if (!config.showJourneyResearch)
                {
                    tooltips.Remove(journeyResearch);
                }
            }
        }
    }
}