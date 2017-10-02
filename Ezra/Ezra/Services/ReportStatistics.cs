using Ezra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Services
{
    public class ReportStatistics
    {
        public ReportItem ReportReference { get; set; }
        public TimeSpan TimeTarget { get; set; }
        public TimeSpan TimeLeftToEnd { get; set; }
        public TimeSpan TimePerDay { get; set; }
    }
}
