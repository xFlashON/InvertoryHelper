using System;
using InvertoryHelper.Model.Documents.Recount;
using InvertoryHelper.ViewModel.Documents.Recounts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Documents.Recounts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecountsPage : ContentPage
    {
        public RecountsPage()
        {
            InitializeComponent();

            var vm = new RecountsViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
        }

        private void RecountsPage_OnAppearing(object sender, EventArgs e)
        {
            var vm = BindingContext as RecountsViewModel;

            if (vm != null)
                MessagingCenter.Subscribe<Recount>(vm, "SaveRecount", o => { vm.LoadRecounts(); });
        }

        private void RecountsPage_OnDisappearing(object sender, EventArgs e)
        {
            var vm = BindingContext as RecountsViewModel;

            if (vm != null)
                MessagingCenter.Unsubscribe<Recount>(vm, "SaveRecount");
        }
    }
}