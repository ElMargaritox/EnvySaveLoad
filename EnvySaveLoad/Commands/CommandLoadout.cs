using EnvySaveLoad.Models;
using EnvySaveLoad.Services;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnvySaveLoad.Models.Loadout;

namespace EnvySaveLoad.Commands
{
    internal class CommandLoadout : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "load";

        public string Help => "load items";

        public string Syntax => String.Empty;

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            var instance = EnvySaveLoadPlugin.Instance;
            try
            {
                Loadout load = instance.saveLoadProvider.GetLoadout(player.Id);
                if (load == null) throw new Exception();

                SaveLoading.DropLoadout(load, player);

                
            }
            catch 
            {
                ChatManager.serverSendMessage(instance.Translate("noload"), UnityEngine.Color.white, null, player.SteamPlayer(), EChatMode.GLOBAL, instance.Configuration.Instance.Icon, true);
            }
        }
        
    }
}
