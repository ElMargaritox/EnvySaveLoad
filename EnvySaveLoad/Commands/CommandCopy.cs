using EnvySaveLoad.Models;
using EnvySaveLoad.Services;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EnvySaveLoad.Commands
{
    public class CommandCopy : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "copy";

        public string Help => "copy items";

        public string Syntax => "/copy <player>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            var instance = EnvySaveLoadPlugin.Instance;
            if(command.Length < 1)
            {
                ChatManager.serverSendMessage(Syntax, Color.red, null, player.SteamPlayer(), EChatMode.GLOBAL, instance.Configuration.Instance.Icon, true);
                return;
            }

            UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
            if(target == null)
            {
                ChatManager.serverSendMessage("No se ha encontrado el jugador", Color.red, null, player.SteamPlayer(), EChatMode.GLOBAL, instance.Configuration.Instance.Icon, true);
                return;
            }
            else
            {
                Loadout loadout = instance.saveLoadProvider.GetLoadout(target.Id);

                if(loadout == null)
                {
                    ChatManager.serverSendMessage("Este jugador no tiene nada para copiar :(", Color.red, null, player.SteamPlayer(), EChatMode.GLOBAL, instance.Configuration.Instance.Icon, true);
                    return;
                }
                else
                {
                    SaveLoading.DropLoadout(loadout, player);
                }
            }

        }
    }
}
