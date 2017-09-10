using System.Collections.Generic;
using InvertoryHelper.Common;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public INavigation Navigation;

        public string ExchangeUrl
        {
            get
            {
                var key = "ExchangeUrl";
                var defaultValue = @"http://{ip}/erp/ws/ExchangeERP_MobApp.1cws";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, defaultValue);

                return (string) Application.Current.Properties[key];
            }

            set
            {
                var key = "ExchangeUrl";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, value);
                else
                    Application.Current.Properties[key] = value;

                OnPropertyChanged("ExchangeUrl");
            }
        }

        public string Login
        {
            get
            {
                var key = "Login";
                var defaultValue = "";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, defaultValue);

                return (string) Application.Current.Properties[key];
            }

            set
            {
                var key = "Login";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, value);
                else
                    Application.Current.Properties[key] = value;

                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get
            {
                var key = "Password";
                var defaultValue = "";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, defaultValue);

                return (string) Application.Current.Properties[key];
            }

            set
            {
                var key = "Password";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, value);
                else
                    Application.Current.Properties[key] = value;

                OnPropertyChanged("Password");
            }
        }

        public string NodeId
        {
            get
            {
                var key = "NodeId";
                var defaultValue = "";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, defaultValue);

                return (string) Application.Current.Properties[key];
            }

            set
            {
                var key = "NodeId";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, value);
                else
                    Application.Current.Properties[key] = value;

                OnPropertyChanged("NodeId");
            }
        }

        public string DeviceName
        {
            get
            {
                var key = "DeviceName";
                var defaultValue = "";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, defaultValue);

                return (string)Application.Current.Properties[key];
            }

            set
            {
                var key = "DeviceName";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, value);
                else
                    Application.Current.Properties[key] = value;

                OnPropertyChanged("DeviceName");
            }
        }

        public bool UseScanner
        {
            get
            {
                var key = "UseScanner";
                var defaultValue = false;

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, defaultValue);

                return (bool) Application.Current.Properties[key];
            }

            set
            {
                var key = "UseScanner";

                if (!Application.Current.Properties.ContainsKey(key))
                    Application.Current.Properties.Add(key, value);
                else
                    Application.Current.Properties[key] = value;

                OnPropertyChanged("UseScanner");
            }
        }

        public List<string> DevicesList
        {
            get
            {
                return DependencyService.Get<IScanner>()?.GetDeviceList() ?? new List<string>();
            }
        } 
    }
}