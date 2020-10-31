using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terrexpansion.Content.Items.Weapons.Swords;

namespace Terrexpansion
{
    partial class Terrexpansion
    {
        public override void AddRecipeGroups() => AddModdedRecipeGroups();

        public override void AddRecipes()
        {
            ModifyRecipes();
            AddModdedRecipes();
        }

        public void AddModdedRecipeGroups()
        {
            CreateRecipeGroup("CopperTinBar", "Copper/Tin Bar", new int[2] { ItemID.CopperBar, ItemID.TinBar });
            CreateRecipeGroup("IronLeadBar", "Iron/Lead Bar", new int[2] { ItemID.IronBar, ItemID.LeadBar });
            CreateRecipeGroup("SilverTungstenBar", "Silver/Tungsten Bar", new int[2] { ItemID.SilverBar, ItemID.TungstenBar });
            CreateRecipeGroup("GoldPlatinumBar", "Gold/Platinum Bar", new int[2] { ItemID.GoldBar, ItemID.PlatinumBar });
            CreateRecipeGroup("CobaltPalladiumBar", "Cobalt/Palladium Bar", new int[2] { ItemID.CobaltBar, ItemID.PalladiumBar });
            CreateRecipeGroup("MythrilOrichalcumBar", "Mythril/Arichalcum Bar", new int[2] { ItemID.MythrilBar, ItemID.OrichalcumBar });
            CreateRecipeGroup("AdamantiteTitaniumBar", "Adamantite/Titanium Bar", new int[2] { ItemID.AdamantiteBar, ItemID.TitaniumBar });
            CreateRecipeGroup("RottenChunkVertebrae", "Rotten Chunk/Vertebre", new int[2] { ItemID.RottenChunk, ItemID.Vertebrae });
        }

        public void ModifyRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ItemID.Zenith))
                {
                    if (recipe.TryGetIngredient(ItemID.Meowmere, out Item meowmere) && recipe.TryGetIngredient(ItemID.StarWrath, out Item starwrath) && recipe.TryGetIngredient(ItemID.TerraBlade, out Item terrablade))
                    {
                        recipe.RemoveIngredient(meowmere);
                        recipe.RemoveIngredient(starwrath);
                        recipe.RemoveIngredient(terrablade);
                        recipe.AddIngredient<FirstFractal>();
                    }
                }
            }
        }

        public void AddModdedRecipes()
        {
            CreateRecipe(ItemID.Gladius).AddRecipeGroup("Terrexpansion:SilverTungstenBar", 9).AddRecipeGroup("Terrexpansion:GoldPlatinumBar", 5).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.Ruler).AddRecipeGroup("Wood", 35).AddTile(TileID.WorkBenches).Register();
            CreateRecipe(ItemID.Umbrella).AddRecipeGroup("Wood", 10).AddIngredient(ItemID.Silk, 5).AddTile(TileID.Loom).Register();
            CreateRecipe(ItemID.AntlionClaw).AddIngredient(ItemID.AntlionMandible, 10).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.BladedGlove).AddIngredient(ItemID.ThrowingKnife, 5).AddIngredient(ItemID.Leather, 3).Register();
            CreateRecipe(ItemID.CandyCaneSword).AddIngredient(ItemID.CandyCaneBlock, 150).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.BoneSword).AddIngredient(ItemID.Bone, 75).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.Katana).AddRecipeGroup("Terrexpansion:IronLeadBar", 9).AddRecipeGroup("Terrexpansion:GoldPlatinumBar", 2).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.Starfury).AddIngredient(ItemID.PlatinumBroadsword).AddIngredient(ItemID.FallenStar, 5).AddIngredient(ItemID.SunplateBlock, 20).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.Starfury).AddIngredient(ItemID.GoldBroadsword).AddIngredient(ItemID.FallenStar, 5).AddIngredient(ItemID.SunplateBlock, 20).AddTile(TileID.Anvils);
            CreateRecipe(ItemID.Frostbrand).AddRecipeGroup("Terrexpansion:MythrilOrichalcumBar", 8).AddIngredient(ItemID.IceBlade).AddTile(TileID.MythrilAnvil).Register();
            CreateRecipe(ItemID.Spear).AddRecipeGroup("Wood", 10).AddRecipeGroup("Terrexpansion:IronLeadBar", 7).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.Trident).AddRecipeGroup("Terrexpansion:GoldPlatinumBar", 10).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.ThunderSpear).AddIngredient(ItemID.ThunderStaff).AddIngredient(ItemID.Spear).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.WoodenBoomerang).AddRecipeGroup("Wood", 20).AddIngredient(TileID.WorkBenches).Register();
            CreateRecipe(ItemID.Shroomerang).AddIngredient(ItemID.WoodenBoomerang).AddIngredient(ItemID.GlowingMushroom, 20).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.IceBoomerang).AddIngredient(ItemID.Shroomerang).AddIngredient(ItemID.IceBlock, 20).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.Flamarang).AddIngredient(ItemID.IceBoomerang).AddIngredient(ItemID.HellstoneBar, 12).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.ChainKnife).AddIngredient(ItemID.Hook).AddIngredient(ItemID.Chain, 2).AddRecipeGroup("Terrexpansion:IronLeadBar", 7).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.Mace).AddRecipeGroup("Terrexpansion:IronLeadBar", 10).AddIngredient(ItemID.Chain, 5).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.ChainGuillotines).AddIngredient(ItemID.ChainKnife, 2).AddRecipeGroup("Terrexpansion:AdamantiteTitaniumBar", 8).AddTile(TileID.MythrilAnvil).Register();
            CreateRecipe(ItemID.Rally).AddIngredient(ItemID.WoodYoyo).AddRecipeGroup("Terrexpansion:IronLeadBar", 15).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.FlintlockPistol).AddRecipeGroup("Wood", 10).AddRecipeGroup("Terrexpansion:IronLeadBar", 7).AddTile(TileID.Anvils).Register();
            CreateRecipe(ItemID.FlareGun).AddIngredient(ItemID.FlintlockPistol).AddIngredient(ItemID.Flare, 100).Register();
            CreateRecipe(ItemID.WandofSparking).AddRecipeGroup("Wood", 10).AddIngredient(ItemID.Torch, 5).AddTile(TileID.LivingLoom).Register();
            CreateRecipe(ItemID.BabyBirdStaff).AddIngredient(ItemID.Bird).AddRecipeGroup("Wood", 15).AddTile(TileID.LivingLoom).Register();
            CreateRecipe(ItemID.SlimeStaff).AddIngredient(ItemID.Gel, 35).AddRecipeGroup("Wood", 15).AddTile(TileID.LivingLoom).Register();
        }

        public RecipeGroup CreateRecipeGroup(string internalName, string displayName, params int[] items)
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + $" {displayName}", items);
            RecipeGroup.RegisterGroup($"Terrexpansion:{internalName}", group);

            return group;
        }
    }
}