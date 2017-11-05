using Ezra.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Text;
using System.Windows.Input;

namespace Ezra.ViewModels
{
    public class BackupPageViewModel : BaseViewModel
    {
        public ICommand SaveBkpCommand { get; set; }
        public ICommand RestoreBkpCommand { get; set; }
        public BackupService BackupService { get; set; }
        private string restoreContent;
        public string RestoreContent
        {
            get { return restoreContent; }
            set { SetProperty(ref restoreContent, value); }
        }


        public BackupPageViewModel(IPageDialogService dialogService) : base(dialogService)
        {
            SaveBkpCommand = new DelegateCommand(SaveBkpCommandExecute);
            RestoreBkpCommand = new DelegateCommand(RestoreBkpCommandExecute);
            BackupService = new BackupService();
        }

        private async void RestoreBkpCommandExecute()
        {
            var answer = await DialogService.DisplayAlertAsync($"Gostaria de restaurar este backup?", "O processo apagará os dados atuais e restaurará o do backup", "Sim", "Não");
            if (answer)
            {
                FileData fileData = null;
                try
                {
                    fileData = await CrossFilePicker.Current.PickFile();
                    if (fileData != null)
                    {
                        string reportJson = Encoding.UTF8.GetString(fileData.DataArray, 0, fileData.DataArray.Length);
                        BackupService.ImportDatabaseFromJson(reportJson);
                        await DialogService.DisplayAlertAsync("Backup", $"Backup {fileData.FileName} Restaurado", "OK");
                    }
                }
                catch (Exception e)
                {
                    await DialogService.DisplayAlertAsync("Erro", $"Erro ao restaurar backup {fileData.FileName}", "Fechar");
                }
            }
            /*
            try
            {
                BackupService.ImportDatabaseFromJson(RestoreContent);
                DialogService.DisplayAlertAsync("Backup", "Backup Restaurado", "OK");
            } catch
            {
                DialogService.DisplayAlertAsync("Erro", "Erro ao restaurar cópia de segurança", "Fechar");
            }*/

        }

        private async void SaveBkpCommandExecute()
        {
            if (!CrossShare.IsSupported)
                return;

            String title = "Meu Relatório: Backup " + DateTime.Now.ToString();
            String body = BackupService.ExportDatabaseToJson();

            await CrossShare.Current.Share(new ShareMessage
            {
                Title = title,
                Text = body,
                Url = ""
            });
        }
    }
}
