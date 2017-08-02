using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.ViewModel.Characteristics;
using InvertoryHelper.ViewModel.Units;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Characteristics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacteristicItemPage : ContentPage
	{
		public CharacteristicItemPage (INavigation Navigation = null, CharacteristicModel characteristic = null)
		{
		    InitializeComponent();

		    var vm = new CharacteristicItemViewModel(characteristic);

		    vm.Navigation = Navigation;

		    BindingContext = vm;
        }
	}
}