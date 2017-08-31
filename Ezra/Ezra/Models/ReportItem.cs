using SQLite.Net.Attributes;
using System;

namespace Ezra.Models
{
    public class ReportItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Placements { get; set; }
        public int Videos { get; set; }
        public int Studies { get; set; }
        public int ReturnVisits { get; set; }

        [Ignore]
        public string FormatedHour
        {
            get
            {
                TimeSpan ts = new TimeSpan(this.Hours, this.Minutes, 0);
                return string.Format("{0:00}:", (ts.Days * 24 + ts.Hours))
                    + string.Format("{0:00}", (ts.Minutes));
            }
        }

        [Ignore]
        public string FormatedDateWeek
        {
            get
            {
                DateTime date = DateTime.Now;
                if (this.Year != 0)
                {
                    date = new DateTime(this.Year, this.Month, this.Day);
                }

                string formatedDateWeek = String.Format("{0:dddd}", date) + ", " + String.Format("{0:dd}", date);
                return formatedDateWeek.Substring(0, 1).ToUpper() + formatedDateWeek.Substring(1);
            }
        }

        [Ignore]
        public string FormatedDateMonth
        {
            get
            {
                DateTime date = DateTime.Now;
                if (this.Year != 0)
                {
                    date = new DateTime(this.Year, this.Month, this.Day);
                }

                string formatedMonth = String.Format("{0:MMMM}", date) + " " + date.Year.ToString();
                return formatedMonth.Substring(0, 1).ToUpper() + formatedMonth.Substring(1);
            }
        }

        [Ignore]
        public DateTime Date
        {
            get
            {
                DateTime date = DateTime.Now;
                if (this.Year != 0)
                {
                    date = new DateTime(this.Year, this.Month, this.Day);
                }
                return date;
            }
        }

    }
}
