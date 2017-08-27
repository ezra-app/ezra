using Ezra.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ezra.Views
{
    public partial class ReportListPage : ContentPage
    {
        private ReportListPageViewModel ViewModel => BindingContext as ReportListPageViewModel;
        public ReportListPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            if(ViewModel != null)
            {
                await Task.Delay(500);
            }

            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            reportListView.IsVisible = true;
        }
    }
}
