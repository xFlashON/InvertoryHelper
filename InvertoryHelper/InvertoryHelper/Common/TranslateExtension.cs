using System;
using InvertoryHelper.Resourses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.Common
{
    [ContentProperty("Text")]
    internal class TranslateExtension : IMarkupExtension
    {
        public TranslateExtension()
        {
            Resource.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }


        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null) return "";

            var translation = Resource.ResourceManager.GetString(Text, Resource.Culture);

            if (translation == null) translation = Text;

            return translation;
        }
    }
}