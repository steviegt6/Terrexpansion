using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terrexpansion.Common.Configs.ServerSide;

namespace Terrexpansion.Content.Commands
{
    public class SpawnItemCommand : ModCommand
    {
        public override string Command => "item";

        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/item <ID> <amount> (<ID> can be ItemID.___, ___ without the ItemID., or an integer.";

        public override string Description => "Allows you to forcefully spawn an item. Only usable with testing mode enabled.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<TerreConfigServerSide>().testingMode)
            {
                int amount = int.Parse(args[1]) >= 1 ? int.Parse(args[1]) : 1;

                if (amount <= 0)
                {
                    amount = 1;
                }

                if (args[0].StartsWith("ItemID."))
                {
                    string[] output = args[0].Split('.');
                    if (ItemID.Search.TryGetId(output[1], out int id))
                    {
                        caller.Player.QuickSpawnItem(id, amount);
                        return;
                    }
                    else
                    {
                        Main.NewText($"No ItemID by the name of {output[1]} found!", Color.Red);
                    }
                }
                else if (char.IsLetter(args[0][0]))
                {
                    if (ItemID.Search.TryGetId(args[0], out int id))
                    {
                        caller.Player.QuickSpawnItem(id, amount);
                        return;
                    }
                    else
                    {
                        Main.NewText($"No item with an ItemID name of {args[0]} found!", Color.Red);
                    }
                }
                else
                {
                    int id = int.Parse(args[0]);

                    if (id >= ItemID.IronPickaxe && id <= ItemLoader.ItemCount)
                    {
                        caller.Player.QuickSpawnItem(id, amount);
                    }
                    else
                    {
                        Main.NewText($"{id} was either smaller than or equal to 0 or larger than or equal to {ItemLoader.ItemCount}", Color.Red);
                    }
                }
            }
        }
    }
}