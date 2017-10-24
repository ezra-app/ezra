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

        public SettingsDatabase SettingsDatabase { get; set; }

        public INavigationService NavigationService { get; }
        public ICommand SaveCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand BackupCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SaveCommand = new DelegateCommand(SaveCommandExecute);
            AboutCommand = new DelegateCommand(AboutCommandExecute);
            BackupCommand = new DelegateCommand(BackupCommandExecute);
            SettingsDatabase = new SettingsDatabase();
            LoadSettings();
        }

        private void BackupCommandExecute()
        {
            NavigationService.NavigateAsync("BackupPage");
        }

        private void AboutCommandExecute()
        {
            NavigationService.NavigateAsync("AboutPage");
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
