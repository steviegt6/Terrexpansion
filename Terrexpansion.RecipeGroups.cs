using Terraria.ID;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion
{
    public partial class Terrexpansion
    {
        public override void AddRecipeGroups()
        {
            // Create recipe groups by calling the CreateRecipeGroup method found in RecipeUtils.cs.
            RecipeUtils.CreateRecipeGroup("CopperTinBar", "Copper/Tin Bar", new int[2] { ItemID.CopperBar, ItemID.TinBar });
            RecipeUtils.CreateRecipeGroup("IronLeadBar", "Iron/Lead Bar", new int[2] { ItemID.IronBar, ItemID.LeadBar });
            RecipeUtils.CreateRecipeGroup("SilverTungstenBar", "Silver/Tungsten Bar", new int[2] { ItemID.SilverBar, ItemID.TungstenBar });
            RecipeUtils.CreateRecipeGroup("GoldPlatinumBar", "Gold/Platinum Bar", new int[2] { ItemID.GoldBar, ItemID.PlatinumBar });
            RecipeUtils.CreateRecipeGroup("CobaltPalladiumBar", "Cobalt/Palladium Bar", new int[2] { ItemID.CobaltBar, ItemID.PalladiumBar });
            RecipeUtils.CreateRecipeGroup("MythrilOrichalcumBar", "Mythril/Arichalcum Bar", new int[2] { ItemID.MythrilBar, ItemID.OrichalcumBar });
            RecipeUtils.CreateRecipeGroup("AdamantiteTitaniumBar", "Adamantite/Titanium Bar", new int[2] { ItemID.AdamantiteBar, ItemID.TitaniumBar });
            RecipeUtils.CreateRecipeGroup("RottenChunkVertebrae", "Rotten Chunk/Vertebre", new int[2] { ItemID.RottenChunk, ItemID.Vertebrae });
        }
    }
}