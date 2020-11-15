using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace Terrexpansion
{
    partial class Terrexpansion
    {
        public static RecipeGroup CreateRecipeGroup(string internalName, string displayName, params int[] items)
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + $" {displayName}", items);
            RecipeGroup.RegisterGroup($"Terrexpansion:{internalName}", group);

            return group;
        }

        public override void AddRecipeGroups()
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
    }
}
