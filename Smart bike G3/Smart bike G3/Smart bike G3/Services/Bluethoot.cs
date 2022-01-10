
using BluetoothLE.Core;
using BluetoothLE.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smart_bike_G3.Services
{
    // Made by Wolfobus (aka Wolf Nemegeer)
    public static class Bluethoot
    {
        static private IAdapter BluetoothAdapter;
        static private List<IDevice> _deviceList;
        static private List<ICharacteristic> _characteristics;

        public static void Scan()
        {

            BluetoothAdapter = DependencyService.Get<IAdapter>();
            
            BluetoothAdapter.ScanTimeout = TimeSpan.FromSeconds(30);
            BluetoothAdapter.ConnectionTimeout = TimeSpan.FromSeconds(10);
            BluetoothAdapter.DeviceDiscovered += DeviceDiscovered;
            
            BluetoothAdapter.StartScanningForDevices();
        }



        public static void Connect(IDevice device)
        {
            
            BluetoothAdapter.ConnectToDevice(device);
        }

        //public async Task<int> getdate(IDevice device = null)
        //{
        //    if(device == null)
        //    {
        //        device = BluetoothAdapter.ConnectedDevices.First();
        //        Characteristics[0].StartUpdates();
        //        return Characteristics[0].Value();
        //    }

        //}

        public static List<IDevice> GetDevices()
        {
            
            Thread.Sleep(10000);
            
           // return _deviceList;
            return (List<IDevice>)BluetoothAdapter.DiscoveredDevices;
        }

        public static List<ICharacteristic> GetCharacteristics()
        {
            return _characteristics;
        }



        private static void DeviceDiscovered(object sender, DeviceDiscoveredEventArgs e) // --> scan
        {
            if (_deviceList.All(x => x.Id != e.Device.Id))
            {
                _deviceList.Add(e.Device);
            }
        }

        private static void BluetoothAdapterOnDeviceConnected(object sender, DeviceConnectionEventArgs deviceConnectionEventArgs) // --> connect
        {
            deviceConnectionEventArgs.Device.ServiceDiscovered += DeviceOnServiceDiscovered;
            deviceConnectionEventArgs.Device.DiscoverServices();
            
        }

        private static void DeviceOnServiceDiscovered(object sender, ServiceDiscoveredEventArgs serviceDiscoveredEventArgs) // -->
        {
           serviceDiscoveredEventArgs.Service.DiscoverCharacteristics();
            serviceDiscoveredEventArgs.Service.CharacteristicDiscovered += DeviceOnCharacteristicDiscovered;
        }

        private static void DeviceOnCharacteristicDiscovered(object sender, CharacteristicDiscoveredEventArgs e) // -->
        {
            _characteristics.Add(e.Characteristic);
            
        }
    }
}
