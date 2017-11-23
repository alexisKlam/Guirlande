using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Guirlande.Lumineuse.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformService))]
namespace Guirlande.Lumineuse.Droid
{
   public class PlatformService :IPlatformService
   {

       public int HideTabBar()
       {
           var activity = (Activity)Forms.Context;
           activity.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
           return 0;
       }
   }
}