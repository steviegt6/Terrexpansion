using Terraria.ID;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion
{
    public partial class Terrexpansion
    {
        public override void AddRecipeGroups()
        {
            // Create recipe groups by calling the CreateRecipeGroup method found in RecipeHelper.cs.
            RecipeHelper.CreateRecipeGroup("CopperTinBar", "Copper/Tin Bar", new int[2] { ItemID.CopperBar, ItemID.TinBar });
            RecipeHelper.CreateRecipeGroup("IronLeadBar", "Iron/Lead Bar", new int[2] { ItemID.IronBar, ItemID.LeadBar });
            RecipeHelper.CreateRecipeGroup("SilverTungstenBar", "Silver/Tungsten Bar", new int[2] { ItemID.SilverBar, ItemID.TungstenBar });
            RecipeHelper.CreateRecipeGroup("GoldPlatinumBar", "Gold/Platinum Bar", new int[2] { ItemID.GoldBar, ItemID.PlatinumBar });
            RecipeHelper.CreateRecipeGroup("CobaltPalladiumBar", "Cobalt/Palladium Bar", new int[2] { ItemID.CobaltBar, ItemID.PalladiumBar });
            RecipeHelper.CreateRecipeGroup("MythrilOrichalcumBar", "Mythril/Arichalcum Bar", new int[2] { ItemID.MythrilBar, ItemID.OrichalcumBar });
            RecipeHelper.CreateRecipeGroup("AdamantiteTitaniumBar", "Adamantite/Titanium Bar", new int[2] { ItemID.AdamantiteBar, ItemID.TitaniumBar });
            RecipeHelper.CreateRecipeGroup("RottenChunkVertebrae", "Rotten Chunk/Vertebre", new int[2] { ItemID.RottenChunk, ItemID.Vertebrae });
        }
    }
}