using Prism.Unity;
using Ezra.Views;
using Xamarin.Forms;
using Ezra.Data;
using Ezra.ViewModels;
using Acr.UserDialogs;
using Prism;
using Prism.Ioc;

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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<EzraNavigationPage>();
            containerRegistry.RegisterForNavigation<MainMasterDetailPage>();
            containerRegistry.RegisterForNavigation<ReportEditionPage, ReportEditionPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportListPage, ReportListPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
            containerRegistry.RegisterForNavigation<BackupPage, BackupPageViewModel>();
            containerRegistry.RegisterForNavigation<StatisticsPage>();
            containerRegistry.RegisterForNavigation<SettingsGeneralPage>();
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
