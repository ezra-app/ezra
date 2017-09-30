using Ezra.Data;
using Ezra.Models;
using Ezra.Services;
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
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService NavigationService { get; set; }
        private ReportItemDatabase ReportItemDatabase { get; set; }

        private ReportItem reportSummary;
        public ReportItem ReportSummary
        {
            get { return reportSummary; }
            set { SetProperty(ref reportSummary, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string counterIconName;
        public string CounterIconName
        {
            get { return counterIconName; }
            set { SetProperty(ref counterIconName, value); }
        }

        public bool counterStarted = false;
        public bool CounterStarted
        {
            get { return counterStarted; }
            set { SetProperty(ref counterStarted, value); }
        }

        private DateTime dateControl;
        public DateTime DateControl
        {
            get { return dateControl; }
            set
            {
                SetProperty(ref dateControl, value);
                FormatTitle();
                LoadReportSummary();
            }
        }

        private string hoursTargetMessage;
        public string HoursTargetMessage
        {
            get { return hoursTargetMessage; }
            set { SetProperty(ref hoursTargetMessage, value); }
        }

        private string hoursLeftMessage;
        public string HoursLeftMessage
        {
            get { return hoursLeftMessage; }
            set { SetProperty(ref hoursLeftMessage, value); }
        }

        private string hoursPerDayMessage;
        public string HoursPerDayMessage
        {
            get { return hoursPerDayMessage; }
            set { SetProperty(ref hoursPerDayMessage, value); }
        }

        public ICommand StartCounterCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ReportListCommand { get; set; }
        public ICommand BackDateCommand { get; set; }
        public ICommand ForwardDateCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService)
        {
            DateControl = DateTime.Now;
            FormatTitle();
            ReportSummary = new ReportItem();
            ReportItemDatabase = new ReportItemDatabase();
            LoadReportSummary();
            CreateTargetMessages();
            NavigationService = navigationService;
            HandleCounterIcon();

            StartCounterCommand = new DelegateCommand(StartCounterCommandExecute);
            AddCommand = new DelegateCommand(AddCommandExecute);
            ReportListCommand = new DelegateCommand(ReportListCommandExecute);
            BackDateCommand = new DelegateCommand(BackDateCommandExecute);
            ForwardDateCommand = new DelegateCommand(ForwardCommandExecute);
            SettingsCommand = new DelegateCommand(SettingsCommandExecute);
        }

        private void SettingsCommandExecute()
        {
            NavigationService.NavigateAsync("SettingsPage");
        }

        public void LoadReportSummary()
        {
            if (ReportItemDatabase != null)
            {
                ReportSummary = ReportItemDatabase.GetReport(DateControl.Month, DateControl.Year);
            }
        }

        private void ForwardCommandExecute()
        {
            DateControl = DateControl.AddMonths(1);
        }

        private void BackDateCommandExecute()
        {
            DateControl = DateControl.AddMonths(-1);
        }

        public void ReportListCommandExecute()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("dateControl", DateControl);
            NavigationService.NavigateAsync("ReportListPage", navigationParams);
        }

        private void AddCommandExecute()
        {
            NavigationService.NavigateAsync("ReportEditionPage");
        }

        private void StartCounterCommandExecute()
        {
            CounterStarted = !CounterStarted;
            HandleCounterIcon();
        }

        private void HandleCounterIcon()
        {
            if (CounterStarted)
            {
                CounterIconName = "pause.png";
            }
            else
            {
                CounterIconName = "play.png";
            }
        }

        private void FormatTitle()
        {
            var formatedMonthTitle = String.Format("{0:MMM yyyy}", DateControl);
            Title = formatedMonthTitle.Substring(0, 1).ToUpper() + formatedMonthTitle.Substring(1);
        }

        public void CreateTargetMessages()
        {
            var statisticsService = new StatisticsService();
            HoursLeftMessage = statisticsService.GetLeftToEndMessage(ReportSummary);
            HoursTargetMessage = statisticsService.GetTargetMessage();
            HoursPerDayMessage = statisticsService.GetPerDayMessage(ReportSummary);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            LoadReportSummary();
            CreateTargetMessages();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
