using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace FindPermission
{
    [ApiVersion(2, 1)]
    public class FindPermission : TerrariaPlugin
    {
        public override Version Version 
            => new Version(1, 0);

        public override string Author 
            => "Rozen4334";

        public override string Name 
            => "FindPermission";

        public override string Description 
            => "A plugin to find permissions by their command name.";

        public FindPermission(Main game) : base(game) 
            => Order = 1;

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("findperm.use", FindPerm, "findperm", "fp"));
        }

        public static void FindPerm(CommandArgs args)
        {
            if (args.Parameters.Count != 1)
            {
                args.Player.SendErrorMessage("Invalid syntax! Usage: /findpermission <commandname>");
                return;
            }
            string text = args.Parameters[0].ToLowerInvariant();
            if (text.StartsWith(TShock.Config.Settings.CommandSpecifier))
            {
                text = text.Substring(1);
            }
            int i = 0;
            while (i < Commands.ChatCommands.Count)
            {
                if (Commands.ChatCommands[i].Names.Contains(text))
                {
                    if (Commands.ChatCommands[i].Permissions.Count == 1)
                    {
                        args.Player.SendInfoMessage("The permission needed for {2}{1} is {0}", new object[]
                        {
                            Commands.ChatCommands[i].Permissions[0],
                            Commands.ChatCommands[i].Name,
                            TShock.Config.Settings.CommandSpecifier
                        });
                        return;
                    }
                    args.Player.SendInfoMessage("The permissions needed for {0}{1} are:", new object[]
                    {
                        TShock.Config.Settings.CommandSpecifier,
                        Commands.ChatCommands[i].Name
                    });
                    for (int j = 0; j < Commands.ChatCommands[i].Permissions.Count; j++)
                    {
                        args.Player.SendInfoMessage(Commands.ChatCommands[i].Permissions[j]);
                    }
                    return;
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
