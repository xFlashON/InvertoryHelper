using System;
using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.View.Units;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Units
{
    public class UnitsViewModel : BaseViewModel
    {
        public INavigation Navigation;

        public UnitsViewModel()
        {
            Title = "Loading";

            UnitsList = new ObservableCollection<UnitModel>();

            LoadUnitsList();

            MessagingCenter.Subscribe<Unit>(this, "SaveUnit", SaveUnit);
        }

        public UnitModel SelectedUnit { get; set; }

        public ObservableCollection<UnitModel> UnitsList { get; set; }

        public Command AddCommand
        {
            get { return new Command(() => AddUnit()); }
        }

        public Command EditCommand
        {
            get { return new Command(() => EditUnit()); }
        }

        private async void LoadUnitsList()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                UnitsList.Clear();

                var unitsList = await DataRepository.Instance.GetUnitsAsync();

                foreach (var unit in unitsList)
                    UnitsList.Add(new UnitModel(unit));

                Title = "Units";

                IsBusy = false;
            }
        }

        private async void AddUnit()
        {
            if (Navigation != null)
                await Navigation.PushAsync(new UnitItemPage(Navigation));
        }

        private async void EditUnit()
        {
            if (SelectedUnit != null)
                if (Navigation != null)
                    await Navigation.PushAsync(new UnitItemPage(Navigation, SelectedUnit));
        }

        private async void SaveUnit(Unit unit)
        {
            if (unit != null)
            {
                var uid = await DataRepository.Instance.SaveUnitAsync(unit);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Unit is not saved!", "DisplayAlert");
                    return;
                }

                LoadUnitsList();
            }
        }
    }
}