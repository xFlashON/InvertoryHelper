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
            using (var db = DependencyService.Get<IDataRepository>())
            {
                db.CreateDb();
            }
        }

        public ICommand OpenRefereccesPage => new Command(() =>
        {
            if (navigation != null)
                navigation.PushAsync(new SettingsPage());
        });
    }
}