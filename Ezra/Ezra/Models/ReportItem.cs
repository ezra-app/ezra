﻿using Ezra.Utils;
using SQLite.Net.Attributes;
using System;

namespace Ezra.Models
{
    public class ReportItem : BaseModel
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Placements { get; set; }
        public int Videos { get; set; }
        public int Studies { get; set; }
        public int ReturnVisits { get; set; }

        public ReportItem(int month, int day, int year, int minutes)
        {
            Month = month;
            Day = day;
            Year = year;
            Minutes = minutes;
        }

        public ReportItem(int month, int day, int year, int hours, int minutes) : 
            this(month, day, year, minutes)
        {
            Hours = hours;
        }

        public ReportItem()
        {
        }

        [Ignore]
        public string FormatedHour
        {
            get
            {
                return ReportUtils.FormatHour(this);
            }
        }

        [Ignore]
        public string FormatedDateWeek
        {
            get
            {
                return ReportUtils.FormateDateWeek(this);
            }
        }

        [Ignore]
        public string FormatedDateMonth
        {
            get
            {
                return ReportUtils.FormatedDateMonth(this);
            }
        }

        [Ignore]
        public DateTime Date
        {
            get
            {
                return ReportUtils.ToDateTime(this);
            }
        }

        public bool IsEmpty()
        {
            return Hours == 0 && Minutes == 0 && Placements == 0 && Videos == 0 && ReturnVisits == 0 && Studies == 0;
        }

    }
}
