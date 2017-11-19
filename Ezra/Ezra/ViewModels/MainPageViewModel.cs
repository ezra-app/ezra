using Ezra.Data;
using Ezra.Models;
using Ezra.Models;
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
    public class MainPageViewModel : BaseViewModel
    {
        private ReportItemDatabase ReportItemDatabase { get; set; }

        private ReportItem reportSummary;
        public ReportItem ReportSummary
        {
            get { return reportSummary; }
            set { SetProperty(ref reportSummary, value); }
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

        public CounterTimestamp CounterTimestamp { get; set; }

        private String counterText;
        public String CounterText
        {
            get { return counterText; }
            set { SetProperty(ref counterText, value); }
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

        public MainPageViewModel(INavigationService navigationService, 
            IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            DateControl = DateTime.Now;
            VerifyIfIsClosed();
            FormatTitle();
            ReportSummary = new ReportItem();
            ReportItemDatabase = new ReportItemDatabase();
            LoadReportSummary();
            CreateTargetMessages();
            LoadCounter();
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

        private void LoadCounter()
        {
            //ReportItemDatabase.GetDatabase().DeleteAll<CounterTimestamp>();
            CounterTimestamp = ReportItemDatabase.GetDatabase().Table<CounterTimestamp>().FirstOrDefault();
            var a = ReportItemDatabase.GetDatabase().Table<CounterTimestamp>().ToList();
            if (CounterTimestamp != null)
            {
                CounterStarted = CounterTimestamp.Started;
                CounterText = ReportUtils.FormatHourToCounter(new TimeSpan(CounterTimestamp.InitialTimestamp));
            }
            else
            {
                CounterStarted = false;
            }
            HandleCounterIcon();
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
                "\nRevisitas: " + ReportSummary.ReturnVisits +
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
            if (CounterStarted)
            {
                if (CounterTimestamp == null)
                {
                    CounterTimestamp = new CounterTimestamp { InitialTimestamp = DateTime.Now.Ticks, Started = true };
                    ReportItemDatabase.Save(CounterTimestamp);
                }
                else
                {
                    CounterTimestamp.InitialTimestamp = DateTime.Now.Ticks;
                    CounterTimestamp.Started = true;
                    ReportItemDatabase.GetDatabase().Update(CounterTimestamp);
                }

                CounterText = ReportUtils.FormatHourToCounter(new TimeSpan(CounterTimestamp.InitialTimestamp));
            }
            else
            {
                if (CounterTimestamp != null)
                {
                    var now = DateTime.Now;
                    long inicialDate = CounterTimestamp.InitialTimestamp;
                    long finalDate = now.Ticks;
                    TimeSpan totalTime = new TimeSpan(finalDate - inicialDate).Duration();

                    CounterTimestamp.Started = false;
                    ReportItemDatabase.Update(CounterTimestamp);

                    var reportItem = new ReportItem(now.Month, now.Day, now.Year, totalTime.Hours, totalTime.Minutes);
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("reportItem", (ReportItem)reportItem);
                    navigationParams.Add("isEditing", false);
                    NavigationService.NavigateAsync("ReportEditionPage", navigationParams);
                }
            }
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
            var statisticsTarget = new MonthlyStatisticsService().Calculate(ReportSummary);
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
    }
}
