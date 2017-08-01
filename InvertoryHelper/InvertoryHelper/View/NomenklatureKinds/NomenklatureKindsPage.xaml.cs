using InvertoryHelper.ViewModel.NomenclatureKinds;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.NomenclatureKinds
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NomenclatureKindsPage : ContentPage
    {
        public NomenclatureKindsPage()
        {
            InitializeComponent();

            var vm = new NomenclatureKindsViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;

        }
    }
}