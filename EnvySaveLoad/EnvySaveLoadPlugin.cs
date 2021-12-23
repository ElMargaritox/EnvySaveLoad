using EnvySaveLoad.Configuration;
using EnvySaveLoad.Database;
using EnvySaveLoad.Models;
using EnvySaveLoad.Services;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace EnvySaveLoad
{
    public class EnvySaveLoadPlugin : RocketPlugin<EnvySaveLoadConfiguration>
    {
        public static EnvySaveLoadPlugin Instance { get; set; }
        public SaveLoadProvider saveLoadProvider { get; set; }
        protected override void Load()
        {
            Instance = this;
            if (Configuration.Instance.AutoLoadOnDeath)
            {
                UnturnedPlayerEvents.OnPlayerRevive += OnRevive;
            }

            Logger.Log("EnvySaveLoad - By Margarita#8172 (Database Json)");
            Logger.Log("El Plugin original es de ImperialsPlugins solo que este tiene una database con Json");
            

            saveLoadProvider = new SaveLoadProvider();
            saveLoadProvider.Reload();
        }

        private void OnRevive(UnturnedPlayer player, Vector3 position, byte angle)
        {
            try
            {
                Loadout loadout = saveLoadProvider.GetLoadout(player.Id);
                if (loadout == null) throw new Exception();
                SaveLoading.DropLoadout(loadout, player);
                player.MaxSkills();
            }
            catch 
            {

            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                TranslationList translationLists = new TranslationList();

                translationLists.Add("noload", "No tienes nada en el inventario virtual usa /save");
                translationLists.Add("saved", "Has guardado tu inventario correctamente");
                return translationLists;
            }
        }

        protected override void Unload()
        {
            try
            {
                UnturnedPlayerEvents.OnPlayerRevive -= OnRevive;
            }
            catch { }
         

        }
    }
}
