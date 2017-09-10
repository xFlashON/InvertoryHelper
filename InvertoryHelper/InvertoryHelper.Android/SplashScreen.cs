using Android.App;
using Android.OS;

namespace InvertoryHelper.Droid
{
    [Activity(Label = "Invertory Helper", Icon = "@drawable/Icon", Theme = "@style/SplashScreenTheme",
        MainLauncher = true, NoHistory = true)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }
    }
}