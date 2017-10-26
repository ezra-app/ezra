using Ezra.Data;
using Ezra.Models;
using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Services
{
    public class BackupService
    {
        public ReportItemDatabase ReportItemDatabase { get; set; }

        public BackupService()
        {
            ReportItemDatabase = new ReportItemDatabase();
        }

        public string ExportDatabaseToJson()
        {
            return JsonConvert.SerializeObject(ReportItemDatabase.ListAll<ReportItem>());
        }

        public void ImportDatabaseFromJson(string reportJson)
        {
            var currentReportItens = ReportItemDatabase.ListAll<ReportItem>();
            List<ReportItem> reportItensImported = null;
            try
            {
                reportItensImported = JsonConvert.DeserializeObject<List<ReportItem>>(reportJson);
                if (reportItensImported != null && reportItensImported.Count() > 0)
                {
                    ReportItemDatabase.SaveAll<ReportItem>(reportItensImported);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            if(reportItensImported != null && reportItensImported.Count() > 0)
            {
                foreach (var item in currentReportItens)
                {
                    ReportItemDatabase.Delete(item.Id);
                }
            }
           
        }

        public async void BackupInFile()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("RelatorioBKP",
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("rdb.bkp",
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(ExportDatabaseToJson());
        }

        public async void RestoreBackup()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("RelatorioBKP",
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("rdb.bkp",
                CreationCollisionOption.OpenIfExists);
            var content = await FileExtensions.ReadAllTextAsync(file);
            ImportDatabaseFromJson(content);
        }
    }
}
