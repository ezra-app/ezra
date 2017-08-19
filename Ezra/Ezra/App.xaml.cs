using Prism.Unity;
using Ezra.Views;
using Xamarin.Forms;
using Ezra.Data;

namespace Ezra
{
    public partial class App : PrismApplication
    {
        static EzraDatabaseManager databaseManager;
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            //NavigationService.NavigateAsync("EzraNavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
            NavigationService.NavigateAsync("MainMasterDetailPage");
            //MainPage = new MainMasterDetailPage();

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<EzraNavigationPage>();
            Container.RegisterTypeForNavigation<MainMasterDetailPage>();
            Container.RegisterTypeForNavigation<ReportEditionPage>();
            Container.RegisterTypeForNavigation<ReportListPage>();
        }

        public static EzraDatabaseManager DatabaseManager
        {
            get
            {
                if (databaseManager == null)
                {
                    databaseManager = new EzraDatabaseManager(DependencyService.Get<IDatabaseConnection>());
                }
                return databaseManager;
            }
        }
    }
}
