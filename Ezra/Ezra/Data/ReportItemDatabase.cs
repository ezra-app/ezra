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

        public void Save(ReportItem reportItem)
        {
            GetDatabase().Insert(reportItem);
        }

        public List<ReportItem> ListAll()
        {
            return GetDatabase().Table<ReportItem>().ToList();
        }

        public ReportItem GetReport(int month, int year)
        {
            // List<ReportItem> itens = await GetDatabase().Table<ReportItem>().Where(i => i.Month == month && i.Year == year).ToListAsync();
            List<ReportItem> itens = ListAll();

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

    }
}
