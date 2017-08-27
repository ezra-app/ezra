using Ezra.Data;
using Ezra.Models;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ezra.ViewModels
{
    public class ReportListPageViewModel : BindableBase, INavigationAware
    {
        public ObservableCollection<ReportItem> ReportItems { get; set; }
        public ReportItemDatabase ReportItemDatabase { get; set; }

        private DateTime dateControl = DateTime.Now;
        public DateTime DateControl
        {
            get { return dateControl; }
            set
            {
                SetProperty(ref dateControl, value);
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public ReportListPageViewModel()
        {
            FormatTitle();
            ReportItems = new ObservableCollection<ReportItem>();
            ReportItemDatabase = new ReportItemDatabase();
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
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("dateControl"))
            {
                DateControl = (DateTime)parameters["dateControl"];
            }
            FormatTitle();
            Load();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
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
