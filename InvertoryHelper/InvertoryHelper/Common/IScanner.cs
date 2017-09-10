using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvertoryHelper.Common
{
    public interface IScanner
    {
        List<string> GetDeviceList();

        Task<Exception> ConnectDevice(string deviceName);

        Exception DisconnectDevice();
        Exception GetBarcode();
    }
}