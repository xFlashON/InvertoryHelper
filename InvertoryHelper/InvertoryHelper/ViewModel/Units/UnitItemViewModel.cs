using InvertoryHelper.Common;
using InvertoryHelper.Model;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Units
{
    public class UnitItemViewModel : BaseViewModel
    {
        public INavigation Navigation;
        private readonly Unit unit;

        public UnitItemViewModel(UnitModel Unit = null)
        {
            unit = new Unit();

            if (Unit == null)
            {
                Title = "Add unit";
            }
            else
            {
                Title = "Edit unit";
                unit.Uid = Unit.Uid;
                Name = Unit.Name;
            }
        }

        public string Name
        {
            get => unit.Name;
            set
            {
                unit.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public Command SaveButton => new Command(async () =>
        {
            MessagingCenter.Send(unit, "SaveUnit");
            await Navigation?.PopAsync();
        });

        public Command CancelButton => new Command(async () => { await Navigation?.PopAsync(); });
    }
}