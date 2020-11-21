using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Content.Items.Materials;

namespace Terrexpansion
{
    public partial class Terrexpansion
    {
        // Compile-time constant strings so we can make sure we actually type the correct string.
        public const string IRON_LEAD_BAR = "Terrexpansion:IronLeadBar";

        public const string SILVER_TUNGSTEN_BAR = "Terrexpansion:SilverTungstenBar";
        public const string GOLD_PLATINUM_BAR = "Terrexpansion:GoldPlatinumBar";
        public const string MYTHRIL_ORICHALCUM_BAR = "Terrexpansion:MythrilOrichalcumBar";
        public const string ADAMANTITE_TITANIUM_BAR = "Terrexpansion:AdamantiteTitaniumBar";
        public const string WOOD = "Wood";

        public override void AddRecipes()
        {
            // Split recipes up into smaller, collapsible methods.
            EditRecipes();
            HandRecipes();
            WorkbenchRecipes();
            LoomRecipes();
            AnvilRecipes();
            MythrilAnvilRecipes();
            LivingLoomRecipes();
        }

        public void HandRecipes()
        {
            CreateRecipe(ItemID.BladedGlove)
                .AddIngredient(ItemID.ThrowingKnife, 5)
                .AddIngredient(ItemID.Leather, 3)
                .Register();

            CreateRecipe(ItemID.FallenStar)
                .AddIngredient(ModContent.ItemType<StarFragment>(), 5)
                .Register();
        }

        public void WorkbenchRecipes()
        {
            CreateRecipe(ItemID.Ruler)
                .AddRecipeGroup(WOOD, 35)
                .AddTile(TileID.WorkBenches)
                .Register();

            CreateRecipe(ItemID.WoodenBoomerang)
                .AddRecipeGroup(WOOD, 20)
                .AddIngredient(TileID.WorkBenches)
                .Register();
        }

        public void LoomRecipes()
        {
            CreateRecipe(ItemID.Umbrella)
                .AddRecipeGroup(WOOD, 10)
                .AddIngredient(ItemID.Silk, 5)
                .AddTile(TileID.Loom)
                .Register();
        }

        public void AnvilRecipes()
        {
            CreateRecipe(ItemID.Gladius)
                .AddRecipeGroup(SILVER_TUNGSTEN_BAR, 9)
                .AddRecipeGroup(GOLD_PLATINUM_BAR, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.AntlionClaw)
                .AddIngredient(ItemID.AntlionMandible, 10)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.CandyCaneSword)
                .AddIngredient(ItemID.CandyCaneBlock, 150)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.BoneSword)
                .AddIngredient(ItemID.Bone, 75)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Katana)
                .AddRecipeGroup(IRON_LEAD_BAR, 9)
                .AddRecipeGroup(GOLD_PLATINUM_BAR, 2)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Starfury)
                .AddIngredient(ItemID.PlatinumBroadsword)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddIngredient(ItemID.SunplateBlock, 20)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Starfury)
                .AddIngredient(ItemID.GoldBroadsword)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddIngredient(ItemID.SunplateBlock, 20)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Spear)
                .AddRecipeGroup(WOOD, 10)
                .AddRecipeGroup(IRON_LEAD_BAR, 7)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Trident)
                .AddRecipeGroup(GOLD_PLATINUM_BAR, 10)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.ThunderSpear)
                .AddIngredient(ItemID.ThunderStaff)
                .AddIngredient(ItemID.Spear)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Shroomerang)
                .AddIngredient(ItemID.WoodenBoomerang)
                .AddIngredient(ItemID.GlowingMushroom, 20)
                .AddTile(TileID.Anvils).Register();

            CreateRecipe(ItemID.IceBoomerang)
                .AddIngredient(ItemID.Shroomerang)
                .AddIngredient(ItemID.IceBlock, 20)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Flamarang)
                .AddIngredient(ItemID.IceBoomerang)
                .AddIngredient(ItemID.HellstoneBar, 12)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.ChainKnife)
                .AddIngredient(ItemID.Hook)
                .AddIngredient(ItemID.Chain, 2)
                .AddRecipeGroup(IRON_LEAD_BAR, 7)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Mace)
                .AddRecipeGroup(IRON_LEAD_BAR, 10)
                .AddIngredient(ItemID.Chain, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.Rally)
                .AddIngredient(ItemID.WoodYoyo)
                .AddRecipeGroup(IRON_LEAD_BAR, 15)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.FlintlockPistol)
                .AddRecipeGroup(WOOD, 10)
                .AddRecipeGroup(IRON_LEAD_BAR, 7)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(ItemID.FlareGun)
                .AddIngredient(ItemID.FlintlockPistol)
                .AddIngredient(ItemID.Flare, 100)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public void MythrilAnvilRecipes()
        {
            CreateRecipe(ItemID.Frostbrand)
                .AddRecipeGroup(MYTHRIL_ORICHALCUM_BAR, 8)
                .AddIngredient(ItemID.IceBlade)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe(ItemID.ChainGuillotines)
                .AddIngredient(ItemID.ChainKnife, 2)
                .AddRecipeGroup(ADAMANTITE_TITANIUM_BAR, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public void LivingLoomRecipes()
        {
            CreateRecipe(ItemID.WandofSparking)
                .AddRecipeGroup(WOOD, 10)
                .AddIngredient(ItemID.Torch, 5)
                .AddTile(TileID.LivingLoom)
                .Register();

            CreateRecipe(ItemID.BabyBirdStaff)
                .AddIngredient(ItemID.Bird).AddRecipeGroup(WOOD, 15)
                .AddTile(TileID.LivingLoom)
                .Register();

            CreateRecipe(ItemID.SlimeStaff)
                .AddIngredient(ItemID.Gel, 35)
                .AddRecipeGroup(WOOD, 15)
                .AddTile(TileID.LivingLoom)
                .Register();
        }
    }
}