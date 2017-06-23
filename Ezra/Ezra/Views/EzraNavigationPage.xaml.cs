using Xamarin.Forms;

namespace Ezra.Views
{
    public partial class EzraNavigationPage : NavigationPage
    {
        public EzraNavigationPage()
        {
            InitializeComponent();
        }

        public EzraNavigationPage(Page page) : base (page)
        {
            InitializeComponent();
        }
    }
}
