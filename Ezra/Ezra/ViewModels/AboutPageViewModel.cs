using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ezra.ViewModels
{
	public class AboutPageViewModel : BindableBase
	{
        private IPageDialogService DialogService { get; set; }
        public ICommand ShowTeamCommand { get; set; }

        public AboutPageViewModel(IPageDialogService dialogService)
        {
            DialogService = dialogService;
            ShowTeamCommand = new DelegateCommand(ShowTeamCommandExecute);
        }

        private void ShowTeamCommandExecute()
        {
            string teamMessage = "Desenvolvedor: Humberto Machado\n\n" +
                "Testadores:\nRaquel Machado\nRafael Marques\nHugo Machado\n\n" +
                "Página do Projeto: https://github.com/linck/ezra-xamarin";
            DialogService.DisplayAlertAsync("Equipe", teamMessage, "Fechar");
        }
    }
}
