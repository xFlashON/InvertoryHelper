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

namespace InvertoryHelper.Droid
{
    [Activity(Label = "Invertory Helper", Icon = "@drawable/Icon",  Theme = "@style/SplashScreenTheme", MainLauncher = true, NoHistory = true)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
            
        }
    }
}