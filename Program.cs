using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMI.PCIDevices
{
    class Program
    {
        private static void Main(string[] args)
        {
            PciDevices devices = new PciDevices();
            foreach (var device in devices.FindDevices())
            {
                Console.WriteLine("VendorID: {0} , DeviceID {1}", device.GetVendorId(), device.GetDeviceId());
            }
            Console.ReadKey();

        }
    }
}
