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
        [OneToOne]
        public WeekSettings WeekSettings { get; set; }
    }

}
