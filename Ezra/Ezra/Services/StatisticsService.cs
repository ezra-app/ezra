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
        }

        public ReportStatistics Calculate(ReportItem report)
        {
            SettingsDatabase = new SettingsDatabase();
            Settings = SettingsDatabase.GetSettings();
            if (Settings == null)
            {
                Settings = new Settings();
            }
            ReportStatistics statistics = new ReportStatistics();
            statistics.ReportReference = report;
            statistics.TimeLeftToEnd = GetTimeLeftToEnd(report);
            statistics.TimePerDay = GetTimePerDay(report);
            statistics.TimeTarget = GetTarget();
            return statistics;
        }

        private TimeSpan GetTarget()
        {
            if (Settings != null)
            {
                return new TimeSpan(Settings.HoursTarget, 0, 0);
            }
            else
            {
                return new TimeSpan(0, 0, 0);
            }

        }

        private TimeSpan GetTimePerDay(ReportItem report)
        {
            int daysLeftInMonth = DateTime.DaysInMonth(report.Year, report.Month) - DateTime.Now.Day + 1;
            if (Settings.GetDaysOfWeek().Count() > 0)
            {
                daysLeftInMonth = ReportUtils.GetEffectiveDaysInMonth(Settings.GetDaysOfWeek());
            }

            if (daysLeftInMonth <= 0)
            {
                daysLeftInMonth = 1;
            }

            TimeSpan timeLeftToEnd = GetTimeLeftToEnd(report);
            if (timeLeftToEnd.TotalMinutes > 0)
            {
                double minutesPerDay = timeLeftToEnd.TotalMinutes / daysLeftInMonth;
                return new TimeSpan(0, Convert.ToInt16(minutesPerDay), 0);
            }
            else
            {
                return new TimeSpan(0, 0, 0);
            }
        }

        public TimeSpan GetTimeLeftToEnd(ReportItem report)
        {
            TimeSpan hoursTarget = new TimeSpan(Settings.HoursTarget, 0, 0);
            TimeSpan totalTime = ReportUtils.ToTimeSpan(report);
            return hoursTarget.Subtract(totalTime);
        }
    }
}
