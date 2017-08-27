namespace InvertoryHelper.Common
{
    public class BaseViewModel : ObservableObject
    {
        private bool isBusy;

        private string title;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public bool IsInitialized { get; set; }
    }
}