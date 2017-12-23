using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using InvertoryHelper.Common;
using InvertoryHelper.Droid.Common;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExternalScanner))]

namespace InvertoryHelper.Droid.Common
{
    public class ExternalScanner : IScanner
    {
        private static BluetoothSocket socket;
        private static bool DeviceConnected;

        public List<string> GetDeviceList()
        {
            var adapter = BluetoothAdapter.DefaultAdapter;

            if (adapter == null)
                return new List<string>();

            if (!adapter.IsEnabled)
            {
                adapter.Enable();
                while (!adapter.IsEnabled)
                {
                    System.Threading.Thread.Sleep(500);
                }
            }

            return adapter.BondedDevices.Select(d => d.Name).ToList();
        }

        public async Task<Exception> ConnectDevice(string deviceName)
        {
            try
            {
                var adapter = BluetoothAdapter.DefaultAdapter;

                if (adapter == null)
                    return new Exception("Bluetooth adapter not found");

                if (!adapter.IsEnabled)
                {
                    adapter.Enable();
                    while (!adapter.IsEnabled)
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                }

                var device = adapter.BondedDevices.FirstOrDefault(d => d.Name == deviceName);

                if (device == null)
                    return new Exception($"Device {deviceName} not found");

                socket = device.CreateInsecureRfcommSocketToServiceRecord(device.GetUuids().ElementAt(0).Uuid);

                await socket.ConnectAsync();

                DeviceConnected = true;

                return null;
            }
            catch (Exception e)
            {
                DeviceConnected = false;

                return e;
            }
        }

        public Exception DisconnectDevice()
        {
            try
            {
                if (DeviceConnected)
                {
                    socket.Close();
                    DeviceConnected = false;

                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public Exception GetBarcode()
        {
            try
            {
                if (DeviceConnected)
                {
                    var buffer = new byte[1024];

                    var encoder = new ASCIIEncoding();

                    while (true)
                    {
                        if (socket.InputStream.CanRead)
                        {
                            var bytesRead = socket.InputStream.Read(buffer, 0, buffer.Length);

                            var result = encoder.GetString(buffer, 0, buffer.Length);

                            result = result.Replace("\r", string.Empty);
                            result = result.Replace("\0", string.Empty);

                            if (result != string.Empty)
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    MessagingCenter.Send<string>(result, "ScannedCode");
                                });
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}