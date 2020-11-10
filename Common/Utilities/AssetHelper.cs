using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrexpansion.Common.Utilities
{
    public static class AssetHelper
    {
        public static Asset<Texture2D> VanillaClassicHeartTexture, VanillaClassicHeart2Texture, VanillaClassicManaTexture, ClassicMana2Texture, BarManaTexture, EndlessQuiver, LungExtendedBubble, LifeFruit, SynergyButton;

        public static void LoadAssets()
        {
            VanillaClassicHeartTexture = TextureAssets.Heart;
            VanillaClassicHeart2Texture = TextureAssets.Heart2;
            VanillaClassicManaTexture = TextureAssets.Mana;
            EndlessQuiver = TextureAssets.Item[ItemID.EndlessQuiver];
            LifeFruit = TextureAssets.Item[ItemID.LifeFruit];

            ClassicMana2Texture = ModContent.GetTexture("Terrexpansion/Assets/Star_StarFruit_Fancy");
            BarManaTexture = ModContent.GetTexture("Terrexpansion/Assets/Star_StarFruit_Bar");
            LungExtendedBubble = ModContent.GetTexture("Terrexpansion/Assets/Bubble_LungExtensionCard");
            SynergyButton = TextureAssets.EmoteMenuButton;
        }

        public static void UnloadAssets()
        {
            foreach (FieldInfo fieldInfo in typeof(AssetHelper).GetFields())
            {
                if (fieldInfo.IsStatic)
                {
                    fieldInfo.SetValue(null, null);
                }
            }
        }

        public static void SwapAssets()
        {
            TextureAssets.Heart = Main.Assets.Request<Texture2D>("Images\\UI\\PlayerResourceSets\\FancyClassic\\Heart_Fill");
            TextureAssets.Heart2 = Main.Assets.Request<Texture2D>("Images\\UI\\PlayerResourceSets\\FancyClassic\\Heart_Fill_B");
            TextureAssets.Mana = Main.Assets.Request<Texture2D>("Images\\UI\\PlayerResourceSets\\FancyClassic\\Star_Fill");
            TextureAssets.Item[ItemID.EndlessQuiver] = ModContent.GetTexture("Terrexpansion/Assets/Items/EndlessQuiver");
            TextureAssets.Item[ItemID.LifeFruit] = ModContent.GetTexture("Terrexpansion/Assets/Items/LifeFruit");
        }

        public static void LoadVanillaAssets()
        {
            TextureAssets.Heart = VanillaClassicHeartTexture;
            TextureAssets.Heart2 = VanillaClassicHeart2Texture;
            TextureAssets.Mana = VanillaClassicManaTexture;
            TextureAssets.Item[ItemID.EndlessQuiver] = EndlessQuiver;
            TextureAssets.Item[ItemID.LifeFruit] = LifeFruit;
        }
    }
}