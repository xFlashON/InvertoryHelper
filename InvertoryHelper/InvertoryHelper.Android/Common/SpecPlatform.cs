using System;
using Android.Content.PM;
using Android.Content.Res;
using Android.Util;
using Android.Views;
using InvertoryHelper.Common;
using InvertoryHelper.Droid.Common;
using Xamarin.Forms;
using Android.Content;
using Android.Runtime;

[assembly: Dependency(typeof(SpecPlatform))]

namespace InvertoryHelper.Droid.Common
{
    public class SpecPlatform : ISpecPlatform
    {
        public string GetDatabasePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public bool IsPortreitScreenOreientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            var rotation = windowManager.DefaultDisplay.Rotation;

            if (rotation == SurfaceOrientation.Rotation0 || rotation == SurfaceOrientation.Rotation180)
                return true;
            else
                return false;


        }
    }
}