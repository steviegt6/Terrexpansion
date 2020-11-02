using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Common.Utilities;

namespace Terrexpansion.Content.Items.Accessories.Misc
{
    //[AutoloadEquip(EquipType.Shoes)]
    public class TerraflareBoots : BaseItem
    {
        public override string Texture => "Terrexpansion/Content/Items/MysteryItem";

        public override void SetStaticDefaults() => Tooltip.SetDefault("Allows flight, super fast running, and extra mobility on ice" +
            "\n10% increased movement speed" +
            "\nProvides the ability to walk on water, honey, & lava" +
            "\nGrants immunity to fire blocks and 7 seconds of immunity to lava" +
            "\nReduces damage from touching lava" +
            "\nLeaves a trail of flames in your wake");

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(gold: 37);
        }

        public override void UpdateEquip(Player player)
        {
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 420;
            player.lavaRose = true;
            player.accRunSpeed = 7f;
            player.rocketBoots = 4;
            player.moveSpeed += 0.1f;
            player.iceSkate = true;
            player.fairyBoots = true;

            Terrexpansion.TerrariaAssembly.GetCachedType("Terraria.Player").GetInstanceMethod("DoBootsEffect").Invoke(player, new object[1] { Delegate.CreateDelegate(typeof(Utils.TileActionAttempt), player, Terrexpansion.TerrariaAssembly.GetCachedType("Terraria.Player").GetInstanceMethod("DoBootsEffect_PlaceFlamesOnTile")) });
            Terrexpansion.TerrariaAssembly.GetCachedType("Terraria.Player").GetInstanceMethod("DoBootsEffect").Invoke(player, new object[1] { Delegate.CreateDelegate(typeof(Utils.TileActionAttempt), player, Terrexpansion.TerrariaAssembly.GetCachedType("Terraria.Player").GetInstanceMethod("DoBootsEffect_PlaceFlowersOnTile")) });
        }

        public override void UpdateVanity(Player player, EquipType type)
        {
            Terrexpansion.TerrariaAssembly.GetCachedType("Terraria.Player").GetInstanceMethod("DoBootsEffect").Invoke(player, new object[1] { Delegate.CreateDelegate(typeof(Utils.TileActionAttempt), player, Terrexpansion.TerrariaAssembly.GetCachedType("Terraria.Player").GetInstanceMethod("DoBootsEffect_PlaceFlamesOnTile")) });
            Terrexpansion.TerrariaAssembly.GetCachedType("Terraria.Player").GetInstanceMethod("DoBootsEffect").Invoke(player, new object[1] { Delegate.CreateDelegate(typeof(Utils.TileActionAttempt), player, Terrexpansion.TerrariaAssembly.GetCachedType("Terraria.Player").GetInstanceMethod("DoBootsEffect_PlaceFlowersOnTile")) });
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TerrasparkBoots)
                .AddIngredient(ItemID.FlameWakerBoots)
                .AddIngredient(ItemID.FairyBoots)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}