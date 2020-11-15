using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Rarities;

namespace Terrexpansion.Content.Items.Accessories.Misc
{
    public class MagiaSalutem : BaseItem
    {
        public override string Texture => "Terrexpansion/Content/Items/MysteryItem";

        public override void SetStaticDefaults() => Tooltip.SetDefault("Increases pickup range for mana stars" +
            "\nRestores mana when damaged" +
            "\nIncreases maximum mana by 20" +
            "\n24% reduced mana usage" +
            "\nAutomatically uses mana potions when needed" +
            "\nCauses stars to fall after taking damage" +
            "\nStars restore mana when collected" +
            "\nProvides life regeneration and reduces the cooldown of healing potions by 25%" +
            "\n25% increases magic damage" +
            "\nEnemies are less likely to target you");

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.rare = ModContent.RarityType<ZenithRarity>();
            item.value = Item.sellPrice(gold: 71);
            item.lifeRegen = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.manaMagnet = true;
            player.statManaMax2 += 20;
            player.magicCuffs = true;
            player.manaCost -= 0.24f;
            player.manaFlower = true;
            player.starCloak = true;
            player.starCloakIsManaCloak = true;
            player.aggro -= 400;
            player.pStone = true;
            player.GetDamage(DamageClass.Magic) += 0.25f;
        }

        public override void AddRecipes() => CreateRecipe()
                .AddIngredient(ItemID.CelestialCuffs)
                .AddIngredient(ItemID.ArcaneFlower)
                .AddIngredient(ItemID.ManaCloak)
                .AddIngredient(ItemID.MagnetFlower)
                .AddIngredient(ItemID.CharmofMyths)
                .AddIngredient(ItemID.CelestialEmblem)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
    }
}