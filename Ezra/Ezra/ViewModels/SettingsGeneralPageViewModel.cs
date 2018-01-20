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
    public class SettingsGeneralPageViewModel : BaseViewModel
    {
        private Settings settings = new Settings();
        public Settings Settings
        {
            get { return settings; }
            set { SetProperty(ref settings, value); }
        }
        public SettingsDatabase SettingsDatabase { get; set; }
        public ICommand SaveCommand { get; set; }

        public SettingsGeneralPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SaveCommand = new DelegateCommand(SaveCommandExecute);
            SettingsDatabase = new SettingsDatabase();
            LoadSettings();
        }

        private void SaveCommandExecute()
        {
            Settings currentSettings = SettingsDatabase.GetSettings();
            if (currentSettings != null)
            {
                Settings.Id = currentSettings.Id;
                SettingsDatabase.Update(Settings);
            }
            else
            {
                SettingsDatabase.Save(Settings);
            }
            NavigationService.GoBackAsync();
        }

        private void LoadSettings()
        {
            Settings currentSettings = SettingsDatabase.GetSettings();
            if (currentSettings != null)
            {
                Settings = currentSettings;
            }

        }
    }
}
