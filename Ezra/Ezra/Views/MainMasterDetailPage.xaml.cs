using Xamarin.Forms;

namespace Ezra.Views
{
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        public MainMasterDetailPage()
        {
            InitializeComponent();
            Master = new ContentPage()
            {
                Title = "Retório"
            };
            Detail = new EzraNavigationPage(new MainPage());
            MasterBehavior = MasterBehavior.Popover;
        }
    }
}