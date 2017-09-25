using Ezra.Data;
using Ezra.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ezra.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        private Settings settings = new Settings();
        public Settings Settings
        {
            get { return settings; }
            set { SetProperty(ref settings, value); }
        }

        public BaseDatabase BaseDatabase { get; set; }

        public INavigationService NavigationService { get; }
        public ICommand SaveCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SaveCommand = new DelegateCommand(SaveCommandExecute);
            BaseDatabase = new BaseDatabase();
            LoadSettings();
        }

        private void SaveCommandExecute()
        {

            var db = BaseDatabase.GetDatabase();
            Settings currentSettings = db.Table<Settings>().FirstOrDefault();
            if (currentSettings != null)
            {
                Settings.Id = currentSettings.Id;
                db.Update(Settings);
            }
            else
            {
                db.Insert(Settings);
            }
            NavigationService.GoBackAsync();
        }

        private void LoadSettings()
        {
            Settings currentSettings = BaseDatabase.GetDatabase().Table<Settings>().FirstOrDefault();
            if(currentSettings != null)
            {
                Settings = currentSettings;
            }

        }
    }
}
