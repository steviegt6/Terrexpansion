using Microsoft.Xna.Framework;
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

        public override string Usage => "/spawnnpc <ID> (<ID> can be NPCID.___, ___ without the NPCID., or an integer.";

        public override string Description => "Allows you to forcefully spawn an NPC. Only usable with testing mode enabled.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<ServerSideConfig>().testingMode)
            {
                if (args[0].StartsWith("NPCID."))
                {
                    string[] output = args[0].Split('.');
                    if (NPCID.Search.TryGetId(output[1], out int id))
                    {
                        NPC.NewNPC((int)caller.Player.position.X, (int)caller.Player.position.Y + 100, id);
                        return;
                    }
                    else
                    {
                        Main.NewText($"No NPCID by the name of {output[1]} found!", Color.Red);
                    }
                }
                else if (char.IsLetter(args[0][0]))
                {
                    if (NPCID.Search.TryGetId(args[0], out int id))
                    {
                        NPC.NewNPC((int)caller.Player.position.X, (int)caller.Player.position.Y + 100, id);
                        return;
                    }
                    else
                    {
                        Main.NewText($"No NPC with an NPCID name of {args[0]} found!", Color.Red);
                    }
                }
                else
                {
                    int id = int.Parse(args[0]);

                    if (id >= NPCID.NegativeIDCount && id <= NPCLoader.NPCCount)
                    {
                        NPC.NewNPC((int)caller.Player.position.X, (int)caller.Player.position.Y + 100, id);
                    }
                    else
                    {
                        Main.NewText($"{id} was either smaller than or equal to {NPCID.NegativeIDCount} or larger than or equal to {NPCLoader.NPCCount}", Color.Red);
                    }
                }
            }
        }
    }
}
