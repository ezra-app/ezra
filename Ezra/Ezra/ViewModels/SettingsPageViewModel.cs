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
    public class SettingsPageViewModel : BaseViewModel
    {
        private Settings settings = new Settings();
        public Settings Settings
        {
            get { return settings; }
            set { SetProperty(ref settings, value); }
        }

        public SettingsDatabase SettingsDatabase { get; set; }
        public ICommand GeneralSettingsCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand BackupCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GeneralSettingsCommand = new DelegateCommand(GeneralSettingsCommandExecute);
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

        private void GeneralSettingsCommandExecute()
        {
            NavigationService.NavigateAsync("SettingsGeneralPage");
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
