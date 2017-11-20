using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace WMI.PCIDevices
{
    class PCIDevices 
    {

        Regex deviceID;
        Regex vendorID;
        ManagementObjectSearcher PCIDeviceList;

        public PCIDevices()
        {
            deviceID = new Regex("DEV_.{4}");
            vendorID = new Regex("VEN_.{4}");
            PCIDeviceList = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE 'PCI%'");
        }

        public List<DeviceInfo> findDevices()
        {
            List<DeviceInfo> listDeviceInfo = new List<DeviceInfo>();

            foreach (var item in PCIDeviceList.Get())
            {
                var info = item["DeviceID"].ToString();
                var deviceIDTemp = deviceID.Match(info).Value.Substring(4).ToLower();
                var vendorIDTemp = vendorID.Match(info).Value.Substring(4).ToLower();
                DeviceInfo deviceInfo = DataFinder(deviceIDTemp, vendorIDTemp);
                if (deviceInfo != null)
                {
                    listDeviceInfo.Add(deviceInfo);
                }
            }

            return listDeviceInfo;
        }

        private DeviceInfo DataFinder(string dev, string ven)
        {
            var filePCI = new StreamReader("pci_ids.txt");
            var vendor = new Regex("^" + ven + "  ");
            var device = new Regex("^\\t" + dev + "  ");

            while (!filePCI.EndOfStream)
            {
                var vendorText = filePCI.ReadLine();
                if (vendorText != null && vendor.Match(vendorText).Success)
                {
                    while (!filePCI.EndOfStream)
                    {
                        var deviceText = filePCI.ReadLine();
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
