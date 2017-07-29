using System.ComponentModel;

namespace InvertoryHelper.Common
{
    public class BaseViewModel : INotifyPropertyChanged
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

        /// <summary>
        ///     Gets or sets if VM is busy working
        /// </summary>
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        //INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}