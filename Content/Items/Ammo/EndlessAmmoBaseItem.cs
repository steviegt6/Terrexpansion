using Terraria.ID;

namespace Terrexpansion.Content.Items.Ammo
{
    /// <summary>
    /// This class exists purely to serve as a base for infinite-ammo items (e.g. endless quivers) in order to reduce boilerplate.
    /// </summary>
    public abstract class EndlessAmmoBaseItem : BaseItem
    {
        /// <summary>
        /// The type of the non-endless version of our item.
        /// <para>Used for recipes and <c>item.CloneDefaults()</c>.</para>
        /// </summary>
        public abstract int ItemType { get; }

        /// <summary>
        /// The amount of <c>ItemType</c> required to make our endless variant.
        /// <para>Defaults to 3996.</para>
        /// </summary>
        public virtual int ItemCount => 3996;

        /// <summary>
        /// The crafting station the item will be crafting at.
        /// <para>Defaults to <c>TileID.CrystalBall</c>.</para>
        /// </summary>
        public virtual int CraftingStation => TileID.CrystalBall;

        public override void SafeSetDefaults()
        {
            item.CloneDefaults(ItemType); // First, we want to clone the stats of the normal item.
            item.consumable = false; // Then, we want to set item.consumable to false so the item isn't consumed upon use.
            item.value *= ItemCount; // Lastly, we want to multiply our item's value by the amount of the normal item it would use so the value makes at least some sense.
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemType, ItemCount) // Here we add ItemType as an ingredient with ItemCount as the amount required for out recipe automatically.
                .AddTile(CraftingStation) // Ditto.
                .Register();
        }
    }
}