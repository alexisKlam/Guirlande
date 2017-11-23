using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Guirlande.Lumineuse.Droid
{
    [Activity(Label = "Guirlande.Lumineuse", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Forms.DependencyService.Register<PlatformService>();

            LoadApplication(new App());
        }
    }
}

