using System.Globalization;
using InvertoryHelper.Common;
using InvertoryHelper.Droid.Common;
using Java.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]

namespace InvertoryHelper.Droid.Common
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-");
            return new CultureInfo(netLanguage);
        }
    }
}