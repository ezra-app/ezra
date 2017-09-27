using Ezra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Data
{
    public class ReportItemDatabase : BaseDatabase
    {

        public ReportItemDatabase()
        {

        }

        public List<ReportItem> ListReportsOrdered(int month, int year)
        {
            return (from r in GetDatabase().Table<ReportItem>()
                 where
                     r.Month == month && r.Year == year
                 orderby r.Day descending
                 select r).ToList();
        }

        public ReportItem GetReport(int month, int year)
        {
            List<ReportItem> itens =
                (from r in GetDatabase().Table<ReportItem>()
                 where
                     r.Month == month && r.Year == year
                 select r).ToList();


            ReportItem reportSummary = new ReportItem();
            foreach (var item in itens)
            {
                reportSummary.Hours += item.Hours;
                reportSummary.Minutes += item.Minutes;
                reportSummary.Placements += item.Placements;
                reportSummary.Videos += item.Videos;
                reportSummary.Studies += item.Studies;
                reportSummary.ReturnVisits += item.ReturnVisits;
            }

            return reportSummary;
        }

        public void Delete(int id)
        {
            GetDatabase().Delete<ReportItem>(id);
        }

    }
}
