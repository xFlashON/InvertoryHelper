using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Model.Documents.Recount;
using InvertoryHelper.View.Documents.Recounts;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Documents.Recounts
{
    public class RecountsViewModel : BaseViewModel
    {
        public INavigation Navigation;

        public RecountsViewModel()
        {
            RecountsList = new ObservableCollection<RecountModel>();

            LoadRecounts();
        }

        public ObservableCollection<RecountModel> RecountsList { get; set; }

        public RecountModel CurrentRecount { get; set; }

        public Command EditCommand => new Command(async () =>
            {
                if (CurrentRecount != null)
                    Navigation?.PushAsync(new RecountPage(
                        new RecountModel(await DataRepository.Instance.GetRecountAsync(CurrentRecount.Uid))));
            }
        );

        public Command AddCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation?.PushAsync(new RecountPage(new RecountModel
                    {
                        Number = RecountsList.Any()?RecountsList.Max(o => o.Number) + 1 : 1
                    }));
                });
            }
        }

        public async void LoadRecounts()
        {
            var recountsList = await DataRepository.Instance.GetRecountsAsync();

            RecountsList =
                new ObservableCollection<RecountModel>(recountsList.Select(o => new RecountModel(o)).OrderBy(o => o.Number));

            OnPropertyChanged("RecountsList");
        }
    }
}