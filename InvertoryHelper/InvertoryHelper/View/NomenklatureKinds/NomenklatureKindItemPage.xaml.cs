using InvertoryHelper.ViewModel.NomenclatureKinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.NomenclatureKinds
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NomenclatureKindItemPage : ContentPage
    {
        public NomenclatureKindItemPage(INavigation Navigation = null, NomenclatureKindModel nomenclatureKind = null)
        {
            InitializeComponent();

            var vm = new NomenclatureKindItemViewModel(nomenclatureKind);

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}