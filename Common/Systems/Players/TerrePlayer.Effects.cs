using Terraria;
using Terraria.ModLoader;

namespace Terrexpansion.Common.Systems.Players
{
    public partial class TerrePlayer
    {
        public override void ResetEffects()
        {
            _origBreath = player.breathMax - (extendedLungs ? 100 : 0);

            cactusSetBonus = false;
            vileVial = false;

            player.statManaMax2 += starFruit * 10;
            player.breathMax = _origBreath + (extendedLungs ? 100 : 0);
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (cactusSetBonus)
            {
                player.ApplyDamageToNPC(npc, damage / 5, 5f, npc.position.X + npc.width / 2 < player.position.X + (player.width / 2) ? -1 : 1, crit: false);
            }
        }

        public override void ModifyWeaponDamage(Item item, ref Modifier damage, ref float flat)
        {
            if (item.DamageType == DamageClass.Summon)
            {
                if (vileVial)
                {
                    flat += 3f;
                }
            }
        }
    }
}