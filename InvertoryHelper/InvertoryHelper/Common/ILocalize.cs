using System.Globalization;

namespace InvertoryHelper.Common
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}