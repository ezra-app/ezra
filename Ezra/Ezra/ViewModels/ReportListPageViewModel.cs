using Ezra.Data;
using Ezra.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ezra.ViewModels
{
    public class ReportListPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService NavigationService { get; set; }
        public ObservableCollection<ReportItem> ReportItems { get; set; }
        public ReportItemDatabase ReportItemDatabase { get; set; }

        private DateTime dateControl = DateTime.Now;
        public DateTime DateControl
        {
            get { return dateControl; }
            set
            {
                SetProperty(ref dateControl, value);
                Load();
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }


        public ReportListPageViewModel(INavigationService navigationService)
        {
            FormatTitle();
            ReportItems = new ObservableCollection<ReportItem>();
            ReportItemDatabase = new ReportItemDatabase();
            EditCommand = new DelegateCommand<object>(EditCommandExecute);
            DeleteCommand = new DelegateCommand<object>(DeleteCommandExecute);
            AddCommand = new DelegateCommand(AddCommandExecute);
            NavigationService = navigationService;
        }

        private void DeleteCommandExecute(object id)
        {
            ReportItemDatabase.Delete((int) id);
            Load();
        }

        private void EditCommandExecute(object reportItem)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("reportItem", (ReportItem) reportItem);
            NavigationService.NavigateAsync("ReportEditionPage", navigationParams);
        }

        private void AddCommandExecute()
        {
            NavigationService.NavigateAsync("ReportEditionPage");
        }

        public void Load()
        {
            ReportItems.Clear();
            List<ReportItem> items = ReportItemDatabase.ListReportsOrdered(DateControl.Month, DateControl.Year);
            foreach (var item in items)
            {
                ReportItems.Add(item);
            }
        }

        private void FormatTitle()
        {
            var formatedMonthTitle = String.Format("{0:MMMM}", DateControl) + " " + DateControl.Year.ToString();
            Title = formatedMonthTitle.Substring(0, 1).ToUpper() + formatedMonthTitle.Substring(1);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("dateControl"))
            {
                DateControl = (DateTime)parameters["dateControl"];
            }
            FormatTitle();
            Load();
        }
    }


    public class SourceMock
    {
        public SourceMock(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
    }
}
