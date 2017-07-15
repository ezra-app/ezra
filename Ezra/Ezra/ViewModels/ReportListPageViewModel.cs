using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ezra.ViewModels
{
    public class ReportListPageViewModel : BindableBase
    {
        public ObservableCollection<SourceMock> SourceMocks { get; set; }

        public ReportListPageViewModel()
        {
            SourceMocks = new ObservableCollection<SourceMock>();
        }

        public void Load()
        {
                SourceMocks.Add(new SourceMock("01:00"));
                SourceMocks.Add(new SourceMock("02:00"));
                SourceMocks.Add(new SourceMock("03:00"));
                SourceMocks.Add(new SourceMock("04:00"));
                SourceMocks.Add(new SourceMock("05:00"));
                SourceMocks.Add(new SourceMock("06:00"));
                SourceMocks.Add(new SourceMock("07:00"));
                SourceMocks.Add(new SourceMock("08:00"));
                SourceMocks.Add(new SourceMock("09:00"));
                SourceMocks.Add(new SourceMock("10:00"));
                SourceMocks.Add(new SourceMock("11:00"));
                SourceMocks.Add(new SourceMock("12:00"));
                SourceMocks.Add(new SourceMock("13:00"));
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
