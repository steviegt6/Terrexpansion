using System.IO;
using Terraria;
using Terrexpansion.Common.Systems.Players;

namespace Terrexpansion
{
    public partial class Terrexpansion
    {
        public enum MessageType : byte
        {
            SyncModPlayer
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.SyncModPlayer:
                    byte player = reader.ReadByte();
                    TerrePlayer terrePlayer = Main.player[player].GetModPlayer<TerrePlayer>();
                    terrePlayer.starFruit = reader.ReadInt32();
                    terrePlayer.extendedLungs = reader.ReadBoolean();
                    break;
            }
        }
    }
}
