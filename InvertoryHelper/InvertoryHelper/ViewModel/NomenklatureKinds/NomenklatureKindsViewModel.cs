using System;
using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.NomenclatureKinds;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.NomenclatureKinds
{
    public class NomenclatureKindsViewModel : BaseViewModel
    {
        public INavigation Navigation;

        public NomenclatureKindsViewModel()
        {
            Title = Resource.Loading;

            NomenclatureKindsList = new ObservableCollection<NomenclatureKindModel>();

            LoadNomenclatureKindsList();

            MessagingCenter.Subscribe<NomenclaturesKind>(this, "SaveNomenclaturesKind", SaveNomenclatureKind);
        }

        public NomenclatureKindModel SelectedNomenclatureKind { get; set; }

        public ObservableCollection<NomenclatureKindModel> NomenclatureKindsList { get; set; }

        public Command AddCommand
        {
            get { return new Command(() => AddNomenclatureKind()); }
        }

        public Command EditCommand
        {
            get { return new Command(() => EditNomenclatureKind()); }
        }

        private async void LoadNomenclatureKindsList()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                NomenclatureKindsList.Clear();

                var nomenclatureKindsList = await DataRepository.Instance.GetNomenclatureKindsAsync();

                foreach (var nomenclatureKind in nomenclatureKindsList)
                    NomenclatureKindsList.Add(new NomenclatureKindModel(nomenclatureKind));

                Title = Resource.NomenclatureKinds;

                IsBusy = false;
            }
        }

        private async void AddNomenclatureKind()
        {
            if (Navigation != null)
                await Navigation.PushAsync(new NomenclatureKindItemPage(Navigation));
        }

        private async void EditNomenclatureKind()
        {
            if (SelectedNomenclatureKind != null)
                if (Navigation != null)
                    await Navigation.PushAsync(new NomenclatureKindItemPage(Navigation, SelectedNomenclatureKind));
        }

        private async void SaveNomenclatureKind(NomenclaturesKind nomenclaturesKind)
        {
            if (nomenclaturesKind != null)
            {
                var uid = await DataRepository.Instance.SaveNomenclatureKindAsync(nomenclaturesKind);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Nomenclature kind is not saved!", "DisplayAlert");
                    return;
                }

                LoadNomenclatureKindsList();
            }
        }
    }
}