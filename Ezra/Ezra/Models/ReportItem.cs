using SQLite;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
