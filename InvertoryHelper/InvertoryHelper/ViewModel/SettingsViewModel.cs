using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.Common;
using Xamarin.Forms;


namespace InvertoryHelper.ViewModel
{
    public class SettingsViewModel:BaseViewModel
    {
        public INavigation Navigation;

        public string ExchangeUrl
        {
            get
            {
                string key = "ExchangeUrl";
                string defaultValue = @"http://{ip}/erp/ws/ExchangeERP_MobApp.1cws";

                if (!App.Current.Properties.ContainsKey(key))
                {
                    App.Current.Properties.Add(key, defaultValue);
                }

                return (string)App.Current.Properties[key];
            }

            set
            {
                string key = "ExchangeUrl";

                if (!App.Current.Properties.ContainsKey(key))
                    App.Current.Properties.Add(key, value);
                else
                    App.Current.Properties[key] = value;

                OnPropertyChanged("ExchangeUrl");

            }

        }

        public string Login
        {
            get
            {
                string key = "Login";
                string defaultValue = "";

                if (!App.Current.Properties.ContainsKey(key))
                {
                    App.Current.Properties.Add(key, defaultValue);
                }

                return (string)App.Current.Properties[key];
            }

            set
            {
                string key = "Login";

                if (!App.Current.Properties.ContainsKey(key))
                    App.Current.Properties.Add(key, value);
                else
                    App.Current.Properties[key] = value;

                OnPropertyChanged("Login");

            }

        }

        public string Password
        {
            get
            {
                string key = "Password";
                string defaultValue = "";

                if (!App.Current.Properties.ContainsKey(key))
                {
                    App.Current.Properties.Add(key, defaultValue);
                }

                return (string)App.Current.Properties[key];
            }

            set
            {
                string key = "Password";

                if (!App.Current.Properties.ContainsKey(key))
                    App.Current.Properties.Add(key, value);
                else
                    App.Current.Properties[key] = value;

                OnPropertyChanged("Password");

            }

        }

        public string NodeId
        {
            get
            {
                string key = "NodeId";
                string defaultValue = "";

                if (!App.Current.Properties.ContainsKey(key))
                {
                    App.Current.Properties.Add(key, defaultValue);
                }

                return (string)App.Current.Properties[key];
            }

            set
            {
                string key = "NodeId";

                if (!App.Current.Properties.ContainsKey(key))
                    App.Current.Properties.Add(key, value);
                else
                    App.Current.Properties[key] = value;

                OnPropertyChanged("NodeId");

            }

        }

    }
}
