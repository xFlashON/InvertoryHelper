using System;
using InvertoryHelper.ViewModel.Barcodes;
using InvertoryHelper.ViewModel.Nomenclatures;
using InvertoryHelper.ViewModel.Prices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Nomenclatures
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NomenclatureItemPage : TabbedPage
    {
        public NomenclatureItemPage(INavigation Navigation = null, NomenclatureModel nomenclature = null)
        {
            InitializeComponent();

            var vm = new NomenclatureItemViewModel(nomenclature);

            vm.Navigation = Navigation;

            BindingContext = vm;

            CurrentPageChanged += CurrentPageChangedFoo;
        }

        private void CurrentPageChangedFoo(object sender, EventArgs e)
        {
            var currentPage = ((TabbedPage) sender).CurrentPage;

            if (currentPage == PricesPage)
            {
                var vm = (PricesViewModel) currentPage.BindingContext;

                vm.SearchText = ((NomenclatureItemViewModel) BindingContext).Name;

                vm.SearchCommand.Execute(null);
            }

            if (currentPage == BarcodesPage)
            {
                var vm = (BarcodesViewModel)currentPage.BindingContext;

                vm.SearchText = ((NomenclatureItemViewModel)BindingContext).Name;

                vm.SearchCommand.Execute(null);
            }
        }
    }
}