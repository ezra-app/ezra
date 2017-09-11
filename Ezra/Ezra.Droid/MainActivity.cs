using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;

namespace Ezra.Droid
{
    [Activity(Label = "Ezra", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
            /*FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar);
            Plugin.Iconize.Iconize
                      .With(new Plugin.Iconize.Fonts.IoniconsModule());*/

 /*           if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.LOLLIPOP)
            {
                // Do something for lollipop and above versions
            }
            else
            {
                // do something for phones running an SDK before lollipop
            }*/
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(200, 19, 91, 108));
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
            
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

