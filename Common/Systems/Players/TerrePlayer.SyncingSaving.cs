using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terrexpansion.Common.Systems.Players
{
    public partial class TerrePlayer
    {
        public override void clientClone(ModPlayer clientClone)
        {
            TerrePlayer clone = (TerrePlayer)clientClone;

            clone.starFruit = starFruit;
            clone.extendedLungs = extendedLungs;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            TerrePlayer clone = (TerrePlayer)clientPlayer;
            ModPacket packet = Mod.GetPacket();

            packet.Write(clone.starFruit);
            packet.Write(clone.extendedLungs);
            packet.Send();
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();

            packet.Write((byte)Terrexpansion.MessageType.SyncModPlayer);
            packet.Write((byte)player.whoAmI);
            packet.Send();
        }

        public override TagCompound Save() => new TagCompound
        {
            { "starFruit", starFruit },
            { "extendedLungs", extendedLungs }
        };

        public override void Load(TagCompound tag)
        {
            starFruit = tag.GetInt("starFruit");
            extendedLungs = tag.GetBool("extendedLungs");
        }
    }
}