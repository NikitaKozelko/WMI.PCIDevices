using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMI.PCIDevices
{
    class Program
    {
        static void Main(string[] args)
        {
            PCIDevices devices = new PCIDevices();
            foreach (var device in devices.findDevices())
            {
                Console.WriteLine("VendorID: {0} , DeviceID {1}", device.getVendorID(), device.getDeviceID());
            }
            Console.ReadKey();
        }
    }
}
