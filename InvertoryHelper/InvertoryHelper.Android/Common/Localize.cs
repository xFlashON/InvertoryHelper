using System;
using System.Globalization;
using InvertoryHelper.Common;
using Xamarin.Forms;

[assembly:Dependency(typeof(InvertoryHelper.Droid.Common.Localize))]
namespace InvertoryHelper.Droid.Common
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-");
            return new System.Globalization.CultureInfo(netLanguage);
        }
    }
}