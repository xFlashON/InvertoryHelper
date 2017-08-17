using System;
using System.Threading.Tasks;
using Android.Runtime;
using Android.Util;
using Android.Views;
using InvertoryHelper.Common;
using InvertoryHelper.Droid.Common;

using ZXing.Mobile;
using Point = Android.Graphics.Point;
using Android.App;
using Xamarin.Forms;
using Android.Content;

[assembly: Dependency(typeof(OnPlatform))]

namespace InvertoryHelper.Droid.Common
{
    public class OnPlatform : IOnPlatform
    {
        public string GetDatabasePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public bool IsPortreitScreenOreientation()
        {
            var windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var sizePoint = new Point();

            windowManager.DefaultDisplay.GetSize(sizePoint);

            if (sizePoint.Y > sizePoint.X)
                return true;
            return false;
        }

        public async Task<string> ScanBarcode(string title)
        {

            try
            {
                var scanner = new MobileBarcodeScanner();

                scanner.BottomText = title;

                var result = await scanner.Scan();

                if (result != null)
                    return result.Text;

            }
            catch (Exception ex)
            {
                Log.Error("BarcodeScanningError", ex.Message);
            }

            return string.Empty;
        }

        public void PlaySound(string fileName)
        {

            try
            {

                var player = new Android.Media.MediaPlayer();

                var fd = Android.App.Application.Context.Assets.OpenFd("sucsess.wav");

                player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);

                player.Prepare();

                player.Prepared += (s, e) =>
                {
                    player.Start();
                };

            }
            catch (Exception ex)
            {
                Log.Error("Audio", ex.Message);
            }

        }
    }
}