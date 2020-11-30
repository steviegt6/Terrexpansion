using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrexpansion.Common.Systems.Players
{
    public partial class TerrePlayer : ModPlayer
    {
        public const int MAX_STAR_FRUIT = 10;

        public bool cactusSetBonus, vileVial, extendedLungs, currentlyRotated, currentlyRotatedByToRotation, wasAirborn, lerpingToRotation = false;
        public float correctToRotation = 0f;
        public int starFruit, timeAirborne, timeNotAirborne = 0;
        public int remainingDeadeyeBullets = 6;

        private int _origBreath;

        public override void OnRespawn(Player player)
        {
            player.statLife = player.statLifeMax2;
            player.statMana = player.statManaMax2;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            player.fullRotation = 0;

            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            player.fullRotation = 0;
            Terrexpansion.DeathSplashText = Language.GetTextValue("Mods.Terrexpansion.DeathSplash." + Main.rand.Next(Language.FindAll(Lang.CreateDialogFilter("Mods.Terrexpansion.DeathSplash" + ".", null)).Length), player.name);
            Terrexpansion.CoinSplashText = "Mods.Terrexpansion.CoinSplash." + Main.rand.Next(Language.FindAll(Lang.CreateDialogFilter("Mods.Terrexpansion.CoinSplash" + ".", null)).Length);
        }
    }
}