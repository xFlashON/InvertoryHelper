using System.Windows.Input;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.View;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public INavigation navigation;


        public MainViewModel()
        {
            var repo = DataRepository.Instance;
        }

        public ICommand OpenRefereccesPage => new Command(() =>
        {
            if (navigation != null)
                navigation.PushAsync(new SettingsPage());
        });
    }
}