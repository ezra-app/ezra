using Prism.Unity;
using Ezra.Views;
using Xamarin.Forms;
using Ezra.Data;
using Ezra.ViewModels;

namespace Ezra
{
    public partial class App : PrismApplication
    {
        static EzraDatabaseManager databaseManager;
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.IoniconsModule());
            NavigationService.NavigateAsync("EzraNavigationPage/MainPage");
            //NavigationService.NavigateAsync("MainMasterDetailPage");
            //MainPage = new MainMasterDetailPage();

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>();
            Container.RegisterTypeForNavigation<EzraNavigationPage>();
            Container.RegisterTypeForNavigation<MainMasterDetailPage>();
            Container.RegisterTypeForNavigation<ReportEditionPage, ReportEditionPageViewModel>();
            Container.RegisterTypeForNavigation<ReportListPage, ReportListPageViewModel>();
            Container.RegisterTypeForNavigation<SettingsPage, SettingsPageViewModel>();
            Container.RegisterTypeForNavigation<AboutPage, AboutPageViewModel>();
            Container.RegisterTypeForNavigation<BackupPage, BackupPageViewModel>();
            Container.RegisterTypeForNavigation<StatisticsPage>();
            Container.RegisterTypeForNavigation<SettingsGeneralPage>();
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
