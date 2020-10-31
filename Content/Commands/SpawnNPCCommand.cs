using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Common.Configs.ServerSide;

namespace Terrexpansion.Content.Commands
{
    public class SpawnNPCCommand : ModCommand
    {
        public override string Command => "spawnnpc";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            string realInput = args[0];
            if (ModContent.GetInstance<ServerSideConfig>().testingMode)
            {
                if (realInput.StartsWith("NPCID."))
                {
                    string[] output = realInput.Split('.');
                    if (NPCID.Search.TryGetId(output[1], out int id))
                    {
                        NPC.NewNPC((int)caller.Player.position.X, (int)caller.Player.position.Y + 100, id);
                        return;
                    }
                }
                else if (char.IsLetter(realInput[0]))
                {
                    if (NPCID.Search.TryGetId(realInput, out int id))
                    {
                        NPC.NewNPC((int)caller.Player.position.X, (int)caller.Player.position.Y + 100, id);
                        return;
                    }
                }
                else
                {
                    NPC.NewNPC((int)caller.Player.position.X, (int)caller.Player.position.Y + 100, int.Parse(realInput));
                }
            }
        }
    }
}
