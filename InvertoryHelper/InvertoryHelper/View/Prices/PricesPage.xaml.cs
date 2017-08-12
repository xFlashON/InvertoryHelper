using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.Model;
using InvertoryHelper.ViewModel.Prices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Prices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PricesPage : ContentPage
    {
        public PricesPage()
        {
            InitializeComponent();

            var vm = new PricesViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;

        }

    }
}