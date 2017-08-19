using Ezra.Data;
using Ezra.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ezra.ViewModels
{
    public class ReportEditionPageViewModel : BindableBase
    {
        private INavigationService NavigationService { get; set; }
        public ReportItemDatabase ReportItemDatabase { get; set; }

        private ReportItem reportItem;
        public ReportItem ReportItem
        {
            get { return reportItem; }
            set { SetProperty(ref reportItem, value); }
        }

        public ICommand SaveCommand { get; set; }

        public ReportEditionPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            ReportItemDatabase = new ReportItemDatabase();
            ReportItem = new ReportItem();
            ReportItem.Hours = 4;

            SaveCommand = new Command(SaveCommandExecute);
        }

        private void SaveCommandExecute(object obj)
        {
            ReportItemDatabase.Save(ReportItem);
            NavigationService.GoBackAsync();
        }
    }
}
