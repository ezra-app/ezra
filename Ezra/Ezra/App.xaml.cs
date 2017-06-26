using Prism.Unity;
using Ezra.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace Ezra
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            
            //NavigationService.NavigateAsync("EzraNavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
            //NavigationService.NavigateAsync("MainMasterDetailPage");
            MainPage = new MainMasterDetailPage();
            MainPage.On<Windows>().SetToolbarPlacement(ToolbarPlacement.Top);

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<EzraNavigationPage>();
            Container.RegisterTypeForNavigation<MainMasterDetailPage>();
            Container.RegisterTypeForNavigation<ReportEditionPage>();
        }
    }
}
