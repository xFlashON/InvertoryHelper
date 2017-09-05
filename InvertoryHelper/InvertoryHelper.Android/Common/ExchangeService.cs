using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using InvertoryHelper.Common;
using InvertoryHelper.Droid.Common;
using Java.Security;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExchangeService))]
namespace InvertoryHelper.Droid.Common
{
    public class ExchangeService:IWebExchange
    {

        private static Boolean isBusy;

        public ExchangeResult GetData()
        {
            if (isBusy)
                return  new ExchangeResult(){Sucsess = false, Message = "Exchange in process"};

            try
            {
                isBusy = true;

                string url = string.Empty;
                string login = string.Empty;
                string pwd = string.Empty;
                string node = string.Empty;

                if (App.Current.Properties.ContainsKey("ExchangeUrl"))
                    url = (string) App.Current.Properties["ExchangeUrl"];

                if (App.Current.Properties.ContainsKey("Login"))
                    login = (string)App.Current.Properties["Login"];

                if (App.Current.Properties.ContainsKey("Password"))
                    pwd = (string)App.Current.Properties["Password"];

                if (App.Current.Properties.ContainsKey("NodeId"))
                    node = (string)App.Current.Properties["NodeId"];

                var srv = new ExchangeReference.ExchangeERP_MobApp();

                srv.Url = url;

                srv.PreAuthenticate = true;
                srv.Credentials = new NetworkCredential(login, pwd);

                var res = srv.GetNomenklatures(node, false);
                
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex.Message);
                return new ExchangeResult() {Sucsess = false, Message = ex.Message};
            }
            finally
            {
                isBusy = false;
            }

            return new ExchangeResult(){Sucsess = true};

        }

        public ExchangeResult SendData()
        {
            throw new NotImplementedException();
        }
    }
}