using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ZXing.Mobile;

namespace InvertoryHelper.Droid
{
    [Activity(Label = "Invertory Helper", Icon = "@drawable/Icon", Theme = "@style/MainTheme", MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            LoadApplication(new App());

            MobileBarcodeScanner.Initialize(Application);
        }
    }
}