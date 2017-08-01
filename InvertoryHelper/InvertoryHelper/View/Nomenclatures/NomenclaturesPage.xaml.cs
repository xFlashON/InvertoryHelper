using Android.Content.PM;
using Android.Views;
using InvertoryHelper.Common;
using InvertoryHelper.ViewModel.Nomenclatures;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Nomenclatures
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NomenclaturesPage : ContentPage
    {
        public NomenclaturesPage()
        {
            InitializeComponent();

            var vm = new NomenclaturesViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;

            MessagingCenter.Subscribe<string>(this, "DisplayAlert", DisplayMessage);

            SizeChanged += NomenclaturesPage_SizeChanged;

        }

        private void NomenclaturesPage_SizeChanged(object sender, System.EventArgs e)
        {

            if (DependencyService.Get<ISpecPlatform>().IsPortreitScreenOreientation())
                FilterLayout.Orientation = StackOrientation.Vertical;
            else
                FilterLayout.Orientation = StackOrientation.Horizontal;

        }

        private async void DisplayMessage(string message)
        {
            await DisplayAlert("Message", message, "Close");
        }
    }
}