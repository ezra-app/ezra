using System;
using System.ComponentModel;
using Plugin.Iconize;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(IconButton), typeof(EzraIconButtonRenderer))]
namespace Plugin.Iconize
{

    public class EzraIconButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null || Element == null)
                return;

            UpdateText();
        }

        protected override void OnElementPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || Element == null)
                return;

            switch (e.PropertyName)
            {
                case nameof(IconButton.FontSize):
                case nameof(IconButton.Text):
                case nameof(IconButton.TextColor):
                    UpdateText();
                    break;
            }
        }

        private void UpdateText()
        {
            var icon = Iconize.FindIconForKey(Element.Text);
            if (icon != null)
            {
                Control.Content = $"{icon.Character}";
                Control.FontFamily = Iconize.FindModuleOf(icon).ToFontFamily();
            }
        }
    }
}