using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.ViewModels
{
    public abstract class BaseViewModel : BindableBase, INavigationAware
    {

        protected INavigationService NavigationService { get; set; }
        protected IPageDialogService DialogService { get; set; }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
        }

        public BaseViewModel(IPageDialogService dialogService)
        {
            DialogService = dialogService;
        }

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public BaseViewModel()
        {
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
