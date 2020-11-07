using System.Collections.Generic;
using Terraria.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrexpansion.Common
{
    public abstract class Synergy : ModTexturedType
    {
        public AchievementCategory Category = AchievementCategory.Challenger;

        public int Type { get; internal set; }

        public new virtual string Name => "";

        public virtual string Description => "";

        public virtual int HeldItem => ItemID.None;

        public virtual List<int> EquippedAccessories => new List<int>() { ItemID.None };

        public virtual List<int> EquippedArmor => new List<int>() { ItemID.None };

        protected sealed override void Register() => Type = SynergyLoader.Add(this);
    }

    public class ExampleSynergy : Synergy
    {
        public override string Texture => "Terraria/Item_1";

        public override string Name => "Example Synergy";

        public override string Description => "Synergy made for testing purposes and is to be used as an example.\nHold a wood sword, wear wood armor, and have a step stool equipped.";

        public override int HeldItem => ItemID.WoodenSword;

        public override List<int> EquippedAccessories => new List<int>() { ItemID.PortableStool };

        public override List<int> EquippedArmor => new List<int>() { ItemID.WoodHelmet, ItemID.WoodBreastplate, ItemID.WoodGreaves };
    }
}
