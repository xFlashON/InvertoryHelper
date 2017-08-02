using InvertoryHelper.Resourses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.Common
{
    [ContentProperty("Text")]
    class TranslateExtension : IMarkupExtension
    {

        public string Text { get; set; }

        public TranslateExtension()
        {
            Resource.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }


        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null) return "";

            string translation = Resource.ResourceManager.GetString(Text, Resource.Culture);

            if (translation == null) translation = Text;

            return translation;
        }
    }
}
