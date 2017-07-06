﻿using Prism.Commands;
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
        public ICommand StartCounterCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ReportListCommand { get; set; }
        public ICommand DatePickerCommand { get; set; }
        
        public MainPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            HandleCounterIcon();
            StartCounterCommand = new Command(StartCounterCommandExecute);
            AddCommand = new Command(AddCommandExecute);
            ReportListCommand = new Command(ReportListCommandExecute);
            DatePickerCommand = new Command(DatePickerCommandExecute);
        }

        private void DatePickerCommandExecute(object obj)
        {
            DatePicker datePicker = new DatePicker
            {
                Format = "D",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = false,
                IsEnabled = true,
            };
            datePicker.IsEnabled = true;
            datePicker.Focus();
        }

        public void ReportListCommandExecute()
        {
            NavigationService.NavigateAsync("ReportListPage");
        }

        private void AddCommandExecute(object obj)
        {
            NavigationService.NavigateAsync("ReportEditionPage");
        }

        private void StartCounterCommandExecute(object obj)
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }
    }
}
