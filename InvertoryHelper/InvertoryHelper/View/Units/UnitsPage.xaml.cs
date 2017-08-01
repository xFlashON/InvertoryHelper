using InvertoryHelper.ViewModel.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Units
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnitsPage : ContentPage
    {
        public UnitsPage()
        {
            InitializeComponent();

            var vm = new UnitsViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;

        }
    }
}