using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Terrexpansion.Common.Configs.ClientSide
{
    [Label("Tooltip Modifications")]
    public class TerreConfigTooltips : ModConfig
    {
        public static TerreConfigTooltips Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Tooltip Modifications")]
        [Label("Show Damage Modifications")]
        [Tooltip("Show \"(+/- added/removed damage)\" next to an item's damage.")]
        [DefaultValue(true)]
        public bool showTooltipDamageMods;

        [Label("Color Damage Modifications")]
        [Tooltip("Color the \"(+/- added/removed damage)\" text depending on the value.")]
        [DefaultValue(true)]
        public bool colorTooltipDamageMods;

        [Label("Show Crit Chance Modifications")]
        [Tooltip("Show \"(+/- added/removed crit%)\" next to an item's crit.")]
        [DefaultValue(true)]
        public bool showTooltipCritMods;

        [Label("Color Crit Chance Modifications")]
        [Tooltip("Color the \"(+/- added/removed crit%)\" text depending on the value.")]
        [DefaultValue(true)]
        public bool colorTooltipCritMods;

        [Label("Color Favorited Tooltips")]
        [Tooltip("Colors the tooltips that indicate an item is favorited.")]
        [DefaultValue(true)]
        public bool colorTooltipFavorites;

        [Label("Show Use Animation")]
        [Tooltip("Show the use animation time of an item.")]
        [DefaultValue(true)]
        public bool showTooltipUseAnimation;

        [Label("Show Knockback")]
        [Tooltip("Show the knockback of an item.")]
        [DefaultValue(true)]
        public bool showTooltipKnockback;

        [Header("Tooltip Lines")]
        [Label("Show Item Names")]
        [DefaultValue(true)]
        public bool showItemName;

        [Label("Show Favorited Text")]
        [DefaultValue(true)]
        public bool showFavoriteText;

        [Label("Show Favorited Description Text")]
        [DefaultValue(true)]
        public bool showFavoriteDesc;

        [Label("Show Placing Item Into Itself Text")]
        [DefaultValue(true)]
        public bool showPlacementBlock;

        [Label("Show Social Text")]
        [DefaultValue(true)]
        public bool showSocialText;

        [Label("Show Social Description Text")]
        [DefaultValue(true)]
        public bool showSocialDesc;

        [Label("Show Item Damage")]
        [DefaultValue(true)]
        public bool showDamage;

        [Label("Show Crit Chance")]
        [DefaultValue(true)]
        public bool showItemCritChance;

        [Label("Show Use Animation")]
        [DefaultValue(true)]
        public bool showUseAnimation;

        [Label("Show Item Knockback")]
        [DefaultValue(true)]
        public bool showKnockback;

        [Label("Show Fishing Power")]
        [DefaultValue(true)]
        public bool showFishingPower;

        [Label("Show Bait Requirement Text")]
        [DefaultValue(true)]
        public bool showNeedsBait;

        [Label("Show Bait Power")]
        [DefaultValue(true)]
        public bool showBaitPower;

        [Label("Show Equip Text")]
        [DefaultValue(true)]
        public bool showEquip;

        [Label("Show Wand Consumption Text")]
        [DefaultValue(true)]
        public bool showWandConsumes;

        [Label("Show Quest Text")]
        [DefaultValue(true)]
        public bool showQuest;

        [Label("Show Vanity Text")]
        [DefaultValue(true)]
        public bool showVanity;

        // VanityLegal always removed

        [Label("Show Defense")]
        [DefaultValue(true)]
        public bool showDefense;

        [Label("Show Pickaxe Power")]
        [DefaultValue(true)]
        public bool showPickPower;

        [Label("Show Axe Power")]
        [DefaultValue(true)]
        public bool showAxePower;

        [Label("Show Hammer Power")]
        [DefaultValue(true)]
        public bool showHammerPower;

        [Label("Show Tile Boost")]
        [DefaultValue(true)]
        public bool showTileBoost;

        [Label("Show Life Healing Amount")]
        [DefaultValue(true)]
        public bool showHealLife;

        [Label("Show Mana Restoration Amount")]
        [DefaultValue(true)]
        public bool showHealMana;

        [Label("Show Mana Cost")]
        [DefaultValue(true)]
        public bool showUseMana;

        [Label("Show Placeable Text")]
        [DefaultValue(true)]
        public bool showPlaceable;

        [Label("Show Ammo Text")]
        [DefaultValue(true)]
        public bool showAmmo;

        [Label("Show Consumable Text")]
        [DefaultValue(true)]
        public bool showConsumable;

        [Label("Show Material Text")]
        [DefaultValue(true)]
        public bool showMaterial;

        [Label("Show Etherian Mana Info")]
        [DefaultValue(true)]
        public bool showEtherianManaWarning;

        [Label("Show Well Fed Text")]
        [DefaultValue(true)]
        public bool showWellFedExpert;

        [Label("Show Buff Time")]
        [DefaultValue(true)]
        public bool showBuffTime;

        [Label("Show One Drop Logo")]
        [DefaultValue(true)]
        public bool showOneDropLogo;

        [Label("Show Set Bonus Text")]
        [DefaultValue(true)]
        public bool showSetBonus;

        [Label("Show Expert Text")]
        [DefaultValue(true)]
        public bool showExpert;

        [Label("Show Master Text")]
        [DefaultValue(true)]
        public bool showMaster;

        [Label("Show Research Text")]
        [DefaultValue(true)]
        public bool showJourneyResearch;
    }
}