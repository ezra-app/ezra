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

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var originalColor = reportStackLayout.BackgroundColor;
            await reportStackLayout.ScaleTo(0.9, 50, Easing.Linear);
            await Task.Delay(100);
            await reportStackLayout.ScaleTo(1, 50, Easing.Linear);
            ViewModel.ReportListCommandExecute();
        }
    }
}
