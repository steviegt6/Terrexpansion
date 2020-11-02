using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Rarities;

namespace Terrexpansion.Content.Items.Accessories.Misc
{
    public class Peritum : BaseItem
    {
        public override string Texture => "Terrexpansion/Content/Items/MysteryItem";

        public override void SetStaticDefaults() => Tooltip.SetDefault("Press UP to change gravity" +
            "\nAllows the holder to reverse gravity" +
            "\nSlimes become friendly" +
            "\nAllows the player to dash into the enemy" +
            "\nDouble tap a direction" +
            "\nReduces damage taken by 20%" +
            "\nHas a chance to create illusions and dodge an attack" +
            "\nTemporarily increases critical chance after dodge" +
            "\nMay confuse nearby enemies after being struck" +
            "\nShoots crossbones at enemies while you are attacking" +
            "\nIncreases the strength of friendly bees" +
            "\nReleases volatile gelatin periodically that damages enemies" +
            "\nSummons spores over time that will damage enemies" +
            "\nGreatly increases life regen when not moving" +
            "\nGrants infinite wing and rocket boot flight" +
            "\nIncreases flight and jump mobility");

        public override void SafeSetDefaults()
        {
            item.damage = 60;
            item.defense = 4;
            item.accessory = true;
            item.rare = ModContent.RarityType<ZenithRarity>();
            item.expert = true;
            item.expertOnly = true;
            item.value = Item.sellPrice(gold: 76);
        }

        public override void UpdateEquip(Player player)
        {
            player.empressBrooch = true;
            player.moveSpeed += 0.1f;
            player.shinyStone = true;
            player.SporeSac();
            player.sporeSac = true;
            player.VolatileGelatin();
            player.volatileGelatin = true;
            player.strongBees = true;
            player.boneGlove = true;
            player.brainOfConfusion = true;
            player.endurance += 20f;
            player.dashType = 2;
            player.gravControl2 = true;
            player.npcTypeNoAggro[1] = true;
            player.npcTypeNoAggro[16] = true;
            player.npcTypeNoAggro[59] = true;
            player.npcTypeNoAggro[71] = true;
            player.npcTypeNoAggro[81] = true;
            player.npcTypeNoAggro[138] = true;
            player.npcTypeNoAggro[121] = true;
            player.npcTypeNoAggro[122] = true;
            player.npcTypeNoAggro[141] = true;
            player.npcTypeNoAggro[147] = true;
            player.npcTypeNoAggro[183] = true;
            player.npcTypeNoAggro[184] = true;
            player.npcTypeNoAggro[204] = true;
            player.npcTypeNoAggro[225] = true;
            player.npcTypeNoAggro[244] = true;
            player.npcTypeNoAggro[302] = true;
            player.npcTypeNoAggro[333] = true;
            player.npcTypeNoAggro[335] = true;
            player.npcTypeNoAggro[334] = true;
            player.npcTypeNoAggro[336] = true;
            player.npcTypeNoAggro[537] = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RoyalGel)
                .AddIngredient(ItemID.EoCShield)
                .AddIngredient(ItemID.WormScarf)
                .AddIngredient(ItemID.BrainOfConfusion)
                .AddIngredient(ItemID.BoneGlove)
                .AddIngredient(ItemID.HiveBackpack)
                .AddIngredient(ItemID.VolatileGelatin)
                .AddIngredient(ItemID.SporeSac)
                .AddIngredient(ItemID.ShinyStone)
                .AddIngredient(ItemID.EmpressFlightBooster)
                .AddIngredient(ItemID.GravityGlobe)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}