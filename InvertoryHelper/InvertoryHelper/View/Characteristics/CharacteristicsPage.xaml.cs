using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.ViewModel.Characteristics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Characteristics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacteristicsPage : ContentPage
	{
		public CharacteristicsPage ()
		{
			InitializeComponent ();

		    var vm = new CharacteristicsViewModel();

		    vm.Navigation = Navigation;

		    BindingContext = vm;

        }
	}
}