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
using Xamarin.Forms;

[assembly: Dependency(typeof(ExchangeService))]
namespace InvertoryHelper.Droid.Common
{
    public class ExchangeService : IWebExchange
    {

        public Boolean GetData()
        {

            try
            {
                var srv = new ExchangeReference.ExchangeERP_MobApp();

                srv.Url = "http://193.93.76.208:8080/erp_dev1/ws/ExchangeERP_MobApp.1cws";

                srv.PreAuthenticate = true;
                srv.Credentials = new NetworkCredential("test", "test");

                var res = srv.GetNomenklatures("002", false);

            }
            catch (Exception ex)
            {
                Log.Error("Error", ex.Message);
            }

            return true;

        }

        public bool SendData()
        {
            throw new NotImplementedException();
        }
    }
}