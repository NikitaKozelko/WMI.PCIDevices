using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;


namespace WMI.PCIDevices
{
    class PciDevices
    {

        private readonly Regex _deviceId;
        private readonly Regex _vendorId;
        private readonly ManagementObjectSearcher _pciDeviceList;

        public PciDevices()
        {
            _deviceId = new Regex("DEV_.{4}");
            _vendorId = new Regex("VEN_.{4}");
            _pciDeviceList = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE 'PCI%'");
        }

        public List<DeviceInfo> FindDevices()
        {
            List<DeviceInfo> listDeviceInfo = new List<DeviceInfo>();

            foreach (var item in _pciDeviceList.Get())
            {
                var info = item["DeviceID"].ToString();
                var deviceIdTemp = _deviceId.Match(info).Value.Substring(4).ToLower();
                var vendorIdTemp = _vendorId.Match(info).Value.Substring(4).ToLower();
                DeviceInfo deviceInfo = DataFinder(deviceIdTemp, vendorIdTemp);
                if (deviceInfo != null)
                {
                    listDeviceInfo.Add(deviceInfo);
                }
            }

            return listDeviceInfo;
        }

        private DeviceInfo DataFinder(string dev, string ven)
        {
            var filePci = new StreamReader("pci_ids.txt");
            var vendor = new Regex("^" + ven + "  ");
            var device = new Regex("^\\t" + dev + "  ");

            while (!filePci.EndOfStream)
            {
                var vendorText = filePci.ReadLine();
                if (vendorText != null && vendor.Match(vendorText).Success)
                {
                    while (!filePci.EndOfStream)
                    {
                        var deviceText = filePci.ReadLine();
                        if (deviceText == null || !device.Match(deviceText).Success)
                            continue;
                        return new DeviceInfo(deviceText.Substring(7), vendorText.Substring(6));
                    }
                }
            }

            return null;
        }


    }
}
