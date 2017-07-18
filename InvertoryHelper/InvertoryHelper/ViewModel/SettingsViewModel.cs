using System.Windows.Input;
using InvertoryHelper.Common;
using InvertoryHelper.View.Nomenclatures;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public INavigation navigation;

        public ICommand OpenReference => new Command(OpenReferenceCmd);

        private async void OpenReferenceCmd(object param)
        {
            switch (param)
            {
                case "Nomenclatures":
                {
                    if (navigation != null)
                        await navigation.PushAsync(new NomenclaturesPage());
                }
                    break;
                case "Units":

                    break;
            }
        }
    }
}