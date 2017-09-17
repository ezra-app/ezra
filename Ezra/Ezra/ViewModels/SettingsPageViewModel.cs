using Ezra.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ezra.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        private Settings settings = new Settings();
        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public INavigationService NavigationService { get; }

        public SettingsPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}
