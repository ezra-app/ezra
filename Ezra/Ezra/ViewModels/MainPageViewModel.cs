using Ezra.Data;
using Ezra.Models;
using Ezra.Services;
using Ezra.Utils;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
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
        private IPageDialogService DialogService { get; set; }
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
                HandleDateControlChange();
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

        private bool isClosed;

        public bool IsClosed
        {
            get { return isClosed; }
            set { SetProperty(ref isClosed, value); }
        }


        public ICommand StartCounterCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ReportListCommand { get; set; }
        public ICommand BackDateCommand { get; set; }
        public ICommand ForwardDateCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand TransferCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            DateControl = DateTime.Now;
            VerifyIfIsClosed();
            FormatTitle();
            ReportSummary = new ReportItem();
            ReportItemDatabase = new ReportItemDatabase();
            LoadReportSummary();
            CreateTargetMessages();
            NavigationService = navigationService;
            DialogService = dialogService;
            HandleCounterIcon();

            StartCounterCommand = new DelegateCommand(StartCounterCommandExecute);
            AddCommand = new DelegateCommand(AddCommandExecute);
            ReportListCommand = new DelegateCommand(ReportListCommandExecute);
            BackDateCommand = new DelegateCommand(BackDateCommandExecute);
            ForwardDateCommand = new DelegateCommand(ForwardCommandExecute);
            SettingsCommand = new DelegateCommand(SettingsCommandExecute);
            ShareCommand = new DelegateCommand(ShareCommandExecute);
            TransferCommand = new DelegateCommand(TransferCommandExecute);
        }

        private async void TransferCommandExecute()
        {
            TimeSpan totalReport = ReportUtils.ToTimeSpan(ReportSummary).Duration();
            var a = DateTime.Now;

            var answer = await DialogService.DisplayAlertAsync("Transferir Minutos",
                $"Deseja mesmo transferir {totalReport.Minutes} minutos para o mês atual?",
                "Sim", "Não");

            if (answer)
            {
                var now = DateTime.Now;
                ReportItemDatabase.Save(new ReportItem(DateControl.Month, ReportUtils.DaysInMonth(DateControl), DateControl.Year, totalReport.Minutes * -1));
                ReportItemDatabase.Save(new ReportItem(now.Month, now.Day, now.Year, totalReport.Minutes));
                DateControl = now;
                HandleDateControlChange();
            }
        }

        private void ShareCommandExecute()
        {
            if (!CrossShare.IsSupported)
                return;

            String title = $"Meu Relatório de {ReportSummary.FormatedDateMonth}";
            String body = title +
                "\nHoras: " + ReportSummary.FormatedHour +
                "\nPublicações: " + ReportSummary.Placements +
                "\nVídeos: " + ReportSummary.Videos +
                "\nRevisistas: " + ReportSummary.ReturnVisits +
                "\nEstudos: " + ReportSummary.Studies;

            CrossShare.Current.Share(new ShareMessage
            {
                Title = title,
                Text = body,
                Url = ""
            });
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
            var now = DateTime.Now;
            if (DateControl.Month != now.Month || DateControl.Year != now.Year)
            {
                DateControl = DateControl.AddMonths(1);
                if (DateControl.Month == now.Month && DateControl.Year == now.Year)
                {
                    DateControl = now;
                }
                HandleDateControlChange();
            }
        }

        private void BackDateCommandExecute()
        {
            DateControl = DateControl.AddMonths(-1);
            HandleDateControlChange();
        }

        private void HandleDateControlChange()
        {
            if (ReportItemDatabase != null)
            {
                VerifyIfIsClosed();
                FormatTitle();
                LoadReportSummary();
                CreateTargetMessages();
            }
        }

        public void ReportListCommandExecute()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("dateControl", DateControl);
            NavigationService.NavigateAsync("ReportListPage", navigationParams);
        }

        private void AddCommandExecute()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("dateControl", (DateTime)DateControl);
            NavigationService.NavigateAsync("ReportEditionPage", navigationParams);
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
            var formatedMonthTitle = String.Format("{0:MMMM yyyy}", DateControl);
            Title = formatedMonthTitle.Substring(0, 1).ToUpper() + formatedMonthTitle.Substring(1);
        }

        public void CreateTargetMessages()
        {
            var statisticsTarget = new StatisticsService().Calculate(ReportSummary);
            if (!IsClosed)
            {
                HoursTargetMessage = $"Minha Meta é {ReportUtils.FormatHour(statisticsTarget.TimeTarget)} hrs";
                if (statisticsTarget.TimeLeftToEnd.TotalMinutes > 0)
                {
                    HoursPerDayMessage = $"Preciso de {ReportUtils.FormatHour(statisticsTarget.TimePerDay)} hrs por dia";
                    HoursLeftMessage = $"Faltam {ReportUtils.FormatHour(statisticsTarget.TimeLeftToEnd)} para acabar";
                }
                else
                {
                    HoursPerDayMessage = "Você fechou suas horas!";
                    HoursLeftMessage = "";
                }
            }
            else
            {
                HoursLeftMessage = "";
                if (statisticsTarget.TimeLeftToEnd.TotalMinutes > 0)
                {
                    HoursPerDayMessage = $"Faltou {ReportUtils.FormatHour(statisticsTarget.TimeLeftToEnd)} para acabar";
                }
                else
                {
                    HoursPerDayMessage = "Você fechou suas horas!";
                }
            }
        }

        private void VerifyIfIsClosed()
        {
            var now = DateTime.Now;
            IsClosed = DateControl.Year != now.Year || DateControl.Month != now.Month;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
