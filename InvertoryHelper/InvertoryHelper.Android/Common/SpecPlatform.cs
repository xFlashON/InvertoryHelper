using System;
using InvertoryHelper.Common;
using InvertoryHelper.Droid.Common;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpecPlatform))]

namespace InvertoryHelper.Droid.Common
{
    public class SpecPlatform : ISpecPlatform
    {
        public string GetDatabasePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}