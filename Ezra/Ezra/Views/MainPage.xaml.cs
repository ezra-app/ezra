using Ezra.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Ezra.Views
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel ViewModel => BindingContext as MainPageViewModel;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnDatePickerClicked(object sender, System.EventArgs e)
        {
            if (Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.Windows)
            {
                myDatePicker.IsVisible = !myDatePicker.IsVisible;
            }

            myDatePicker.IsEnabled = true;
            myDatePicker.Focus();
            
        }

        private void OnDatePickerSelected(object sender, System.EventArgs e)
        {
            if (Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.Windows)
            {
                myDatePicker.IsVisible = !myDatePicker.IsVisible;
                Debug.WriteLine("Selectionei");
            }
            Debug.WriteLine("Selectionei");
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            reportStackLayout.ScaleTo(0.9, 20, Easing.Linear);
            //await Task.Delay(50);
            reportStackLayout.ScaleTo(1, 20, Easing.Linear);
            ViewModel.ReportListCommandExecute();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadReportSummary();
        }

    }
}
