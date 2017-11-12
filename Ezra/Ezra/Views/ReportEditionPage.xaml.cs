using System;
using Xamarin.Forms;

namespace Ezra.Views
{
    public partial class ReportEditionPage : ContentPage
    {
        public ReportEditionPage()
        {
            InitializeComponent();
        }

        private void OnEntryFocused(Entry sender, FocusEventArgs e)
        {
            if (sender.Text == "0")
            {
                sender.Text = "";
            }
        }

        private void OnEntryUnfocused(Entry sender, FocusEventArgs e)
        {
            if (sender.Text == "")
            {
                sender.Text = "0";
            }
        }

        private void OnNumericEntryTextChanged(Entry sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(sender.Text) || int.TryParse(sender.Text, out var n))
            {
                sender.Text = e.NewTextValue;
            }
            else
            {
                sender.Text = e.OldTextValue;
            }
        }

        private void OnReturnVisitsPlusClicked(object sender, EventArgs e)
        {
            PlusButtonHandler(ReturnVisitsEntry);
        }

        private void OnReturnVisitsMinusClicked(object sender, EventArgs e)
        {
            MinusButtonHandler(ReturnVisitsEntry);
        }

        private void OnPlacementsPlusClicked(object sender, EventArgs e)
        {
            PlusButtonHandler(PlacementsEntry);
        }

        private void OnPlacementsMinusClicked(object sender, EventArgs e)
        {
            MinusButtonHandler(PlacementsEntry);
        }

        private void OnVideosPlusClicked(object sender, EventArgs e)
        {
            PlusButtonHandler(VideosEntry);
        }

        private void OnVideosMinusClicked(object sender, EventArgs e)
        {
            MinusButtonHandler(VideosEntry);
        }

        private void OnStudiesPlusClicked(object sender, EventArgs e)
        {
            PlusButtonHandler(StudiesEntry);
        }

        private void OnStudiesMinusClicked(object sender, EventArgs e)
        {
            MinusButtonHandler(StudiesEntry);
        }

        private void PlusButtonHandler(Entry entry)
        {
            var valueText = entry.Text;
            if (string.IsNullOrEmpty(valueText))
            {
                valueText = "0";
            }

            entry.Text = (Convert.ToInt16(valueText) + 1).ToString();
        }

        private void MinusButtonHandler(Entry entry)
        {
            var valueText = entry.Text;
            if (string.IsNullOrEmpty(valueText))
            {
                valueText = "0";
            }
            var valueInt = Convert.ToInt16(valueText);
            if (valueInt > 0)
            {
                entry.Text = (Convert.ToInt16(valueText) - 1).ToString();
            }
        }
    }
}
