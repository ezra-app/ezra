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
                Title = "Master Page"
            };
            Detail = new EzraNavigationPage(new MainPage());
        }
    }
}