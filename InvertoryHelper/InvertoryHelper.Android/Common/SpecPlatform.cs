using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using InvertoryHelper.Common;
using InvertoryHelper.Droid.Common;
using Xamarin.Forms;
using Application = Android.App.Application;
using Point = Android.Graphics.Point;

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
            var windowManager = Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var sizePoint = new Point();

            windowManager.DefaultDisplay.GetSize(sizePoint);

            if (sizePoint.Y > sizePoint.X)
                return true;
            return false;
        }
    }
}