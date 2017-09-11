using Ezra.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ezra.Views
{
    public partial class ReportListPage : ContentPage
    {
        public ReportListPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            await Task.Delay(200);

            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            reportListView.IsVisible = true;
        }

        private void OnDatePickerClicked(object sender, System.EventArgs e)
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                myDatePicker.IsVisible = !myDatePicker.IsVisible;
            }

            myDatePicker.IsEnabled = true;
            myDatePicker.Focus();

        }

        private void OnDatePickerSelected(object sender, System.EventArgs e)
        {
            if ((Device.RuntimePlatform == Device.UWP)
                && myDatePicker.IsVisible)
            {
                myDatePicker.IsVisible = false;
            }
        }
    }
}
