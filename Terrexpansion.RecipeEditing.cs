using Terraria;
using Terraria.ID;
using Terrexpansion.Common.Utilities;
using Terrexpansion.Content.Items.Weapons.Swords;

namespace Terrexpansion
{
    public partial class Terrexpansion
    {
        private void EditRecipes()
        {
            // Loop through all recipes so we can find what to edit.
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // Check if the recipe crafts a Zenith.
                // If so, attempt to remove the Moewmere, Star Wrath, and Terra Blade. If it succeeds, add the First Fractal.
                if (recipe.HasResult(ItemID.Zenith))
                    if (recipe.TryRemoveIngredient(ItemID.Meowmere) && recipe.TryRemoveIngredient(ItemID.StarWrath) && recipe.TryRemoveIngredient(ItemID.TerraBlade))
                        recipe.AddIngredient<FirstFractal>();
            }
        }
    }
}