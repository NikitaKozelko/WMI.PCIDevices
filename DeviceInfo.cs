using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMI.PCIDevices
{
    class DeviceInfo
    {

        private readonly string _deviceId;
        private readonly string _vendorId;

        public DeviceInfo(string deviceId, string vendorId)
        {
            _deviceId = deviceId;
            _vendorId = vendorId;

        }

        public string GetDeviceId()
        {
            return _deviceId;
        }

        public string GetVendorId()
        {
            return _vendorId;
        }
    }
}
