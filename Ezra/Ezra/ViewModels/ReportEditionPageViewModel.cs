﻿using Ezra.Data;
using Ezra.Models;
using Ezra.Utils;
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
    public class ReportEditionPageViewModel : BaseViewModel
    {
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

        public ICommand SaveCommand { get; set; }

        public ReportEditionPageViewModel(INavigationService navigationService) : base(navigationService)
        {
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

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(EzraConstants.REPORT_ITEM_NAV_PARAM))
            {
                ReportItem = (ReportItem)parameters[EzraConstants.REPORT_ITEM_NAV_PARAM];
                DateControl = ReportItem.Date;
            }
            if (parameters.ContainsKey(EzraConstants.DATE_CONTROL_NAV_PARAM))
            {
                DateControl = (DateTime)parameters[EzraConstants.DATE_CONTROL_NAV_PARAM];
            }

            if (parameters.ContainsKey(EzraConstants.IS_EDITING_NAV_PARAM))
            {
                Editing = (bool)parameters[EzraConstants.IS_EDITING_NAV_PARAM];
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
