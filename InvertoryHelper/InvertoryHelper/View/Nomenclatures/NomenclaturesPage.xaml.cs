using System;
using InvertoryHelper.Common;
using InvertoryHelper.ViewModel.Nomenclatures;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Nomenclatures
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NomenclaturesPage : ContentPage
    {
        public NomenclaturesPage(bool selectionMode = false)
        {
            InitializeComponent();

            var vm = new NomenclaturesViewModel();

            vm.Navigation = Navigation;

            vm.SelectionMode = selectionMode;

            BindingContext = vm;

            SizeChanged += NomenclaturesPage_SizeChanged;
        }

        private void NomenclaturesPage_SizeChanged(object sender, EventArgs e)
        {
            if (DependencyService.Get<IOnPlatform>().IsPortreitScreenOreientation())
                FilterLayout.Orientation = StackOrientation.Vertical;
            else
                FilterLayout.Orientation = StackOrientation.Horizontal;
        }
    }
}