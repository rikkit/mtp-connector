using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PortableDeviceApiLib;

namespace WPScrobbler
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new PortableDeviceCollection();

            collection.Refresh();

            foreach (var device in collection)
            {
                Console.WriteLine(device.DeviceId);
            }

            Console.ReadLine();
        }
    }
}


public class PortableDeviceCollection : Collection<PortableDevice>
{
    private readonly PortableDeviceManager _deviceManager;

    public PortableDeviceCollection()
    {
        this._deviceManager = new PortableDeviceManager();
    }

    public void Refresh()
    {
        this._deviceManager.RefreshDeviceList();

        // Determine how many WPD devices are connected
        uint count = 0;
        this._deviceManager.GetDevices(null, ref count);

        // Retrieve the device id for each connected device
        var deviceIds = new string[count];
        this._deviceManager.GetDevices( deviceIds, ref count);
        foreach (var deviceId in deviceIds)
        {
            Add(new PortableDevice(deviceId));
        }
    }
}

public class PortableDevice
{
    public PortableDevice(string deviceId)
    {
        this.DeviceId = deviceId;
    }

    public string DeviceId { get; set; }
}