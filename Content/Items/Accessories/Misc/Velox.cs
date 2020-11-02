using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Common.Utilities;
using Terrexpansion.Content.Rarities;

namespace Terrexpansion.Content.Items.Accessories.Misc
{
    public class Velox : BaseItem
    {
        public override string Texture => "Terrexpansion/Content/Items/MysteryItem";

        public override void SetStaticDefaults() => Tooltip.SetDefault("Hold UP to reach higher" +
                "\nAllows the ability to climb walls" +
                "\nAllows flight, super fast running, and extra mobility on ice" +
                "\nGrants the ability to swim and greatly extends underwater breathing" +
                "\nGrants immunity to fire blocks and 7 seconds of immunity to lava" +
                "\nGrants the ability to float in water" +
                "\nGenerates a very suble glow which becomes more vibrant underwater" +
                "\nIncreases jump speed and allows auto-jump" +
                "\nIncreases jump height" +
                "\n25% increased movement speed" +
                "\nProvides the ability to walk on water, honey, & lava" +
                "\nReduces damage from touching lava" +
                "\nNegates fall damage" +
                "\nLeaves a trail of flames in your wake");

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.value = Item.buyPrice(platinum: 1, gold: 52);
            item.rare = ModContent.RarityType<ZenithRarity>();
        }

        public override void UpdateEquip(Player player)
        {
            player.autoJump = true;
            player.jumpSpeedBoost += 3.5f;
            player.sailDash = true;
            player.frogLegJumpBoost = true;
            player.accFlipper = true;
            player.spikedBoots += 2;
            player.hasJumpOption_Cloud = true;
            player.hasJumpOption_Blizzard = true;
            player.hasJumpOption_Fart = true;
            player.hasJumpOption_Sail = true;
            player.hasJumpOption_Sandstorm = true;
            player.releaseBeesWhenHurt = true;
            player.jumpBoost = true;
            player.noFallDmg = true;
            player.carpet = true;
            player.hasFloatingTube = true;
            player.canFloatInWater = true;
            player.portableStoolInfo.SetStats(26, 26, 26);
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 420;
            player.lavaRose = true;
            player.accRunSpeed = 8.5f;
            player.rocketBoots = 4;
            player.moveSpeed += 0.25f;
            player.iceSkate = true;
            player.fairyBoots = true;
            player.accDivingHelm = true;
            player.arcticDivingGear = true;

            if (!player.wet)
            {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.05f, 0.15f, 0.225f);
            }
            else
            {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.4f, 1.2f, 1.8f);
            }

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
                .AddIngredient(ModContent.ItemType<AmphibianGear>())
                .AddIngredient(ModContent.ItemType<BundleofBundleofBalloons>())
                .AddIngredient(ModContent.ItemType<FlyingStepTube>())
                .AddIngredient(ModContent.ItemType<TerraflareBoots>())
                .AddIngredient(ItemID.ArcticDivingGear)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}