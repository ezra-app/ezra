using Ezra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Utils
{
    public class ReportUtils
    {
        public static string FormatHour(ReportItem report)
        {
            return FormatHour(ToTimeSpan(report));
        }


        public static string FormatHour(TimeSpan ts)
        {
            return string.Format("{0:00}:", (ts.Days * 24 + ts.Hours))
               + string.Format("{0:00}", (ts.Minutes));
        }

        public static string FormateDateWeek(ReportItem report)
        {
            DateTime date = ToDateTime(report);
            string formatedDateWeek = String.Format("{0:dddd, dd}", date);
            return formatedDateWeek.Substring(0, 1).ToUpper() + formatedDateWeek.Substring(1);
        }

        public static string FormatedDateMonth(ReportItem report)
        {
            DateTime date = ToDateTime(report);
            string formatedMonth = String.Format("{0:MMMM}", date) + " " + date.Year.ToString();
            return formatedMonth.Substring(0, 1).ToUpper() + formatedMonth.Substring(1);
        }

        public static DateTime ToDateTime(ReportItem report)
        {
            DateTime date = DateTime.Now;
            if (report.Year != 0)
            {
                date = new DateTime(report.Year, report.Month, report.Day);
            }
            return date;
        }

        public static TimeSpan ToTimeSpan(ReportItem report)
        {
            return new TimeSpan(report.Hours, report.Minutes, 0);
        }

        public static int DaysInMonth(ReportItem report)
        {
            return DateTime.DaysInMonth(report.Year, report.Month);
        }

        public static int DaysInMonth(DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static int GetEffectiveDaysInMonth(List<DayOfWeek> effectiveWeekDays)
        {
            DateTime date = DateTime.Now;
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            int effectiveDays = 0;
            for (int i = DateTime.Now.Day; i <= daysInMonth; i++)
            {
                if (effectiveWeekDays.Contains(date.DayOfWeek))
                {
                    effectiveDays++;
                }
                date = date.AddDays(1);
            }
            return effectiveDays;
        }
    }
}
