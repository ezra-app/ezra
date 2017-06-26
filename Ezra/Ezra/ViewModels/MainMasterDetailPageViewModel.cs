using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ezra.ViewModels
{
    public class MainMasterDetailPageViewModel : BindableBase
    {
        public List<MasterPageItem> MasterDetailItens { get; set; }
        public MainMasterDetailPageViewModel()
        {
            MasterDetailItens = new List<MasterPageItem>();
            MasterDetailItens.Add(new MasterPageItem("Inicio", "play.png", "MainPage"));
        }
    }

    public class MasterPageItem
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public string Page { get; set; }

        public MasterPageItem(string label, string icon, string page)
        {
            Label = label;
            Icon = icon;
            Page = page;
        }
    }
}
