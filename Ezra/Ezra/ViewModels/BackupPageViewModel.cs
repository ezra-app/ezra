using Ezra.Services;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Windows.Input;

namespace Ezra.ViewModels
{
    public class BackupPageViewModel : BindableBase
    {
        private IPageDialogService DialogService { get; set; }
        public ICommand SendBkpCommand { get; set; }
        public ICommand RestoreBkpCommand { get; set; }
        public BackupService BackupService { get; set; }
        private string restoreContent;
        public string RestoreContent
        {
            get { return restoreContent; }
            set { SetProperty(ref restoreContent, value); }
        }


        public BackupPageViewModel(IPageDialogService dialogService)
        {
            DialogService = dialogService;
            SendBkpCommand = new DelegateCommand(SendBkpCommandExecute);
            RestoreBkpCommand = new DelegateCommand(RestoreBkpCommandExecute);
            BackupService = new BackupService();
        }

        private void RestoreBkpCommandExecute()
        {
            try
            {
                BackupService.ImportDatabaseFromJson(RestoreContent);
                DialogService.DisplayAlertAsync("Backup", "Backup Restaurado", "OK");
            } catch
            {
                DialogService.DisplayAlertAsync("Erro", "Erro ao restaurar cópia de segurança", "Fechar");
            }
            
        }

        private void SendBkpCommandExecute()
        {
            if (!CrossShare.IsSupported)
                return;

            String title = "Meu Relatório: Backup " + DateTime.Now.ToString();
            String body = BackupService.ExportDatabaseToJson();

            CrossShare.Current.Share(new ShareMessage
            {
                Title = title,
                Text = body,
                Url = ""
            });
        }
    }
}
