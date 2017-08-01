using InvertoryHelper.Common;
using InvertoryHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Units
{
    public class UnitItemViewModel : BaseViewModel
    {
        private Unit unit;

        public INavigation Navigation;

        public string Name
        {
            get => unit.Name;
            set
            {
                unit.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public Command SaveButton
        {
            get => new Command(async () =>
            {
                MessagingCenter.Send<Unit>(unit, "SaveUnit");
                await Navigation?.PopAsync();
            });
        }
        public Command CancelButton { get => new Command(async () => { await Navigation?.PopAsync(); }); }

        public UnitItemViewModel(UnitModel Unit = null)
        {
            unit = new Unit();

            if (Unit == null)
                Title = "Add unit";
            else
            {
                Title = "Edit unit";
                unit.Uid = Unit.Uid;
                Name = Unit.Name;
            }
        }
    }
}

