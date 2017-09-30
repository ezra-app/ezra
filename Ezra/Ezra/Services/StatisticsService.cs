using Ezra.Data;
using Ezra.Models;
using Ezra.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Services
{
    public class StatisticsService
    {
        private SettingsDatabase SettingsDatabase { get; set; }
        private Settings Settings { get; set; }

        public StatisticsService()
        {
            SettingsDatabase = new SettingsDatabase();
            Settings = SettingsDatabase.GetSettings();
        }

        public string GetTargetMessage()
        {
            var target = Settings.HoursTarget;
            return $"Minha Meta é {target} hrs";
        }

        public string GetPerDayMessage(ReportItem report)
        {
            var perDayTarget = 0;
            return $"Preciso de {perDayTarget} hrs por dia";
        }

        public string GetLeftToEndMessage(ReportItem report)
        {
            TimeSpan hoursTarget = new TimeSpan(Settings.HoursTarget, 0, 0);
            TimeSpan totalTime = ReportUtils.ToTimeSpan(report);
            TimeSpan leftTime = hoursTarget.Subtract(totalTime);
            if(leftTime.TotalMinutes > 0)
            {
                return $"Faltam {ReportUtils.FormatHour(leftTime)} para acabar";
            } else
            {
                return "Você fechou as horas do mês!";
            }
            
        }
    }
}
