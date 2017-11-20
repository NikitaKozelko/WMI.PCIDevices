using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMI.PCIDevices
{
    class DeviceInfo
    {
        private string DeviceID;
        private string VendorID;

        public DeviceInfo(string deviceID, string vendorID)
        {
            DeviceID = deviceID;
            VendorID = vendorID;
        }

        public string getDeviceID()
        {
            return DeviceID;
        }

        public string getVendorID()
        {
            return VendorID;
        }
    }
}
