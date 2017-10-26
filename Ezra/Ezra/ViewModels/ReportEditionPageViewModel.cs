using Ezra.Data;
using Ezra.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ezra.ViewModels
{
    public class ReportEditionPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService NavigationService { get; set; }
        public ReportItemDatabase ReportItemDatabase { get; set; }
        public bool Editing { get; set; }
        public bool IsProcessing { get; set; }

        private ReportItem reportItem;
        public ReportItem ReportItem
        {
            get { return reportItem; }
            set { SetProperty(ref reportItem, value); }
        }

        private DateTime dateControl = DateTime.Now;
        public DateTime DateControl
        {
            get { return dateControl; }
            set { SetProperty(ref dateControl, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public ICommand SaveCommand { get; set; }

        public ReportEditionPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            ReportItemDatabase = new ReportItemDatabase();
            ReportItem = new ReportItem();
            DateControl = DateTime.Now;

            SaveCommand = new DelegateCommand(SaveCommandExecute);
        }

        private async void SaveCommandExecute()
        {
            if (!IsProcessing)
            {
                IsProcessing = true;
                ReportItem.Day = DateControl.Day;
                ReportItem.Month = DateControl.Month;
                ReportItem.Year = DateControl.Year;

                if (Editing)
                {
                    ReportItemDatabase.Update(ReportItem);
                }
                else
                {
                    if (!ReportItem.IsEmpty())
                        ReportItemDatabase.Save(ReportItem);
                }
            }
            await NavigationService.GoBackAsync();
            IsProcessing = false;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("reportItem"))
            {
                ReportItem = (ReportItem)parameters["reportItem"];
                DateControl = ReportItem.Date;
                Editing = true;
            }
            else if (parameters.ContainsKey("dateControl"))
            {
                DateControl = (DateTime)parameters["dateControl"];
                Editing = false;
            }
            else
            {
                Editing = false;
            }

            if (Editing)
            {
                Title = "Editar Relatório";
            }
            else
            {
                Title = "Adicionar Relatório";
            }
        }
    }
}
