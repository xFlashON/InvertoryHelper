using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.Nomenclatures;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclaturesViewModel : BaseViewModel
    {
        private bool _selectionMode;
        public INavigation Navigation;

        private NomenclaturesKind selectedNomenclstureKind;

        public NomenclaturesViewModel()
        {
            Title = Resource.Loading;

            NomenclaturesList = new ObservableCollection<NomenclatureModel>();

            LoadNomenclaturesList();

            SelectedNomenclature = null;
            OnPropertyChanged("SelectedNomenclature");

            MessagingCenter.Subscribe<Nomenclature>(this, "SaveNomenclature", SaveNomenclature);

            LoadNomenclatureKindsList();
        }

        public bool SelectionMode
        {
            get => _selectionMode;
            set
            {
                _selectionMode = value;
                OnPropertyChanged("SelectionMode");
            }
        }

        public NomenclatureModel SelectedNomenclature { get; set; }

        public ObservableCollection<NomenclatureModel> NomenclaturesList { get; set; }

        public List<NomenclaturesKind> NomenclatureKindsList { get; set; }

        public NomenclaturesKind SelectedNomenclstureKind
        {
            get => selectedNomenclstureKind;
            set
            {
                selectedNomenclstureKind = value;
                OnPropertyChanged("SelectedNomenclstureKind");
                SearchNomenclatures();
            }
        }

        public string SearchText { get; set; }

        public Command SearchCommand
        {
            get { return new Command(() => SearchNomenclatures()); }
        }

        public Command AddCommand
        {
            get { return new Command(() => AddNomenclature()); }
        }

        public Command EditCommand
        {
            get { return new Command(() => EditNomenclature()); }
        }

        public Command SelectCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (SelectedNomenclature != null)
                    {
                        MessagingCenter.Send(new Nomenclature(SelectedNomenclature),
                            "SelectedNomenclature");
                        Navigation?.PopAsync();
                    }
                });
            }
        }

        public Command ClearNomenclatureKindCommand => new Command(() => SelectedNomenclstureKind = null);

        private async void LoadNomenclaturesList()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync();

                if (SelectedNomenclstureKind != null)
                    nomenclaturesList = nomenclaturesList
                        .Where(N => SelectedNomenclstureKind.Equals(N.NomenclaturesKind)).ToList();

                NomenclaturesList =
                    new ObservableCollection<NomenclatureModel>(nomenclaturesList.Select(N => new NomenclatureModel(N))
                        .ToList());

                OnPropertyChanged("NomenclaturesList");

                Title = Resource.Nomenclatures;

                IsBusy = false;
            }
        }

        private async void SearchNomenclatures()
        {
            if (!IsBusy)
                if (string.IsNullOrEmpty(SearchText))
                {
                    LoadNomenclaturesList();
                }
                else
                {
                    IsBusy = true;

                    Title = Resource.Searching;

                    var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync(N =>
                    {
                        if (N.Name.ToUpper().Contains(SearchText.ToUpper()) || N.Artikul != null &&
                            N.Artikul.ToUpper().Contains(SearchText.ToUpper()))
                            return true;
                        return false;
                    });

                    if (SelectedNomenclstureKind != null)
                        nomenclaturesList = nomenclaturesList
                            .Where(N => SelectedNomenclstureKind.Equals(N.NomenclaturesKind)).ToList();

                    NomenclaturesList =
                        new ObservableCollection<NomenclatureModel>(nomenclaturesList
                            .Select(N => new NomenclatureModel(N)).ToList());

                    OnPropertyChanged("NomenclaturesList");

                    Title = Resource.Nomenclatures;

                    IsBusy = false;
                }
        }

        private async void AddNomenclature()
        {
            await Navigation?.PushAsync(new NomenclatureItemPage(Navigation));
        }

        private async void EditNomenclature()
        {
            if (SelectedNomenclature != null)
                await Navigation?.PushAsync(new NomenclatureItemPage(Navigation, SelectedNomenclature));
        }

        private async void SaveNomenclature(Nomenclature nomenclature)
        {
            if (nomenclature != null)
            {
                var uid = await DataRepository.Instance.SaveNomenclatureAsync(nomenclature);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Nomenclature is not saved!", "DisplayAlert");
                    return;
                }

                SearchNomenclatures();
            }
        }

        private async void LoadNomenclatureKindsList()
        {
            var nomenclatureKinds = await DataRepository.Instance.GetNomenclatureKindsAsync();
            NomenclatureKindsList = new List<NomenclaturesKind>(nomenclatureKinds);

            OnPropertyChanged("NomenclatureKindsLis");
        }
    }
}