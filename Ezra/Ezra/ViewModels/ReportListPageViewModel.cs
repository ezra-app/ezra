using Ezra.Data;
using Ezra.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ezra.ViewModels
{
    public class ReportListPageViewModel : BindableBase
    {
        public ObservableCollection<ReportItem> ReportItems { get; set; }
        public ReportItemDatabase ReportItemDatabase { get; set; }


        public ReportListPageViewModel()
        {
            ReportItems = new ObservableCollection<ReportItem>();
            ReportItemDatabase = new ReportItemDatabase();
        }

        public void Load()
        {
            List<ReportItem> items = ReportItemDatabase.ListAll();
            foreach(var item in items)
            {
                ReportItems.Add(item);
            }
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
