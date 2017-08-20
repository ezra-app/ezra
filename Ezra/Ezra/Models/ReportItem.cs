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

    }
}
