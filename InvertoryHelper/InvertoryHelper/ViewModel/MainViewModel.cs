using System.Windows.Input;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.View;
using InvertoryHelper.View.Documents.Orders;
using InvertoryHelper.View.Documents.Recounts;
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

        public ICommand PriceCheckingCmd => new Command(
            async () => await navigation.PushAsync(new PriceCheckingPage()));

        public ICommand OpenOrdersCmd => new Command(async () => { await navigation?.PushAsync(new OrdersPage()); });

        public ICommand OpenRecountsCmd => new Command(async () => { await navigation?.PushAsync(new RecountsPage()); });

    }
}