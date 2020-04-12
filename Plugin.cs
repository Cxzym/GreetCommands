using System;
using TShockAPI;
using TerrariaApi.Server;
using Terraria;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Collections.Generic;
using Terraria.DataStructures;
using TShockAPI.Hooks;

namespace GreetPlugin.cs
{
    [ApiVersion(2, 1)]

    public class GreetPlugin : TerrariaPlugin
    {

        public override string Name => "GreetPlugin";
        public override Version Version => new Version();
        public override string Author => "Wade";
        public override string Description => "This command will allow you to greet other users.";

        public GreetPlugin(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("greetcommand.shake", Greet, "greet")
            {
                HelpText = "Greets another user."
            });
        }

        private void Greet(CommandArgs args)
        {
            throw new NotImplementedException();
        }

        private void Shake(CommandArgs args)
        {
            string playerName = args.Parameters[0];

            var players = TShock.Utils.FindPlayer(playerName);

            if (players.Count == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }

            else if (players.Count > 1)
            {
                TShock.Utils.SendMultipleMatchError(args.Player, players.Select(p => p.Name));
            }

            else
            {

                TSPlayer.All.SendSuccessMessage($"{args.Player.Name} greeted {players[0].Name}!");
            }
        }

    }
}

