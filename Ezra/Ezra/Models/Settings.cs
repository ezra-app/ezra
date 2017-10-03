using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Models
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int HoursTarget { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }

        public List<DayOfWeek> GetDaysOfWeek()
        {
            var daysOfWeek = new List<DayOfWeek>();

            if (Sunday)
                daysOfWeek.Add(DayOfWeek.Sunday);
            if (Monday)
                daysOfWeek.Add(DayOfWeek.Monday);
            if (Tuesday)
                daysOfWeek.Add(DayOfWeek.Tuesday);
            if (Wednesday)
                daysOfWeek.Add(DayOfWeek.Wednesday);
            if (Thursday)
                daysOfWeek.Add(DayOfWeek.Thursday);
            if (Friday)
                daysOfWeek.Add(DayOfWeek.Friday);
            if (Saturday)
                daysOfWeek.Add(DayOfWeek.Saturday);

            return daysOfWeek;
        }
    }

}
