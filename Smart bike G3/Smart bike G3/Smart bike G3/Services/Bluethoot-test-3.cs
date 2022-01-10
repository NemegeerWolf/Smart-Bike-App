using Plugin.BLE;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smart_bike_G3.Services
{
    class Bluethoot3
    {
        static private Plugin.BLE.Abstractions.Contracts.IBluetoothLE ble = CrossBluetoothLE.Current;
        static private Plugin.BLE.Abstractions.Contracts.IAdapter adapter = CrossBluetoothLE.Current.Adapter;
        static private List<Plugin.BLE.Abstractions.Contracts.IDevice> _deviceList;
        static private List<Plugin.BLE.Abstractions.Contracts.ICharacteristic> _characteristics;
        private static IReadOnlyList<Plugin.BLE.Abstractions.Contracts.IDevice> listDevices;

        public static async Task Scan()
        {
            adapter.ScanMode = Plugin.BLE.Abstractions.Contracts.ScanMode.Balanced;
            adapter.ScanTimeout = 5000;
            adapter.DeviceDiscovered += DeviceDiscovered;
            //    (sender, e) =>
            //{
            //    if (a.Device != null)
            //    {
            //        Debug.WriteLine("Device list: " + a.Device);
            //        _deviceList.Add((Plugin.BLE.Abstractions.Contracts.IDevice)a.Device.NativeDevice);
            //    }
            //};
            adapter.DeviceAdvertised += (s, a) =>
            {
                Debug.WriteLine("Device advertised: " + a.Device);
            };

            await adapter.StartScanningForDevicesAsync();

            
        }

       

        public static async Task Connect(int index)
        {

            try
            {
                var device = _deviceList[index];
               
                await adapter.ConnectToDeviceAsync(device);

                
                adapter.DeviceConnected += BluetoothAdapterOnDeviceConnected;
            }
            catch (DeviceConnectionException e)
            {
                // ... could not connect to device
            }
        }

        private static async void BluetoothAdapterOnDeviceConnected(object sender, DeviceEventArgs e)
        {
            
            //
            IReadOnlyList<Plugin.BLE.Abstractions.Contracts.IService> k = await e.Device.GetServicesAsync();
            Debug.WriteLine(k);
            foreach (Plugin.BLE.Abstractions.Contracts.IService m in k)
            {
                Debug.WriteLine(m.Name);
                var p = await m.GetCharacteristicsAsync();
                foreach (Plugin.BLE.Abstractions.Contracts.ICharacteristic c in p)
                {
                    Debug.WriteLine(c.Name);

                }
            }
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



        public static async Task<List<string>> GetDevices()
        {

            Thread.Sleep(2000);
            var i = (List < Plugin.BLE.Abstractions.Contracts.IDevice >) adapter.DiscoveredDevices;
            // return _deviceList;
            if (_deviceList != null)
            {


                List<string> listDeviceNames = new List<string>();
                foreach (Plugin.BLE.Abstractions.Contracts.IDevice device in _deviceList)
                {
                    listDeviceNames.Add(device.Name);
                }
                return listDeviceNames;
            }
            return new List<string>() { "Noting found"};
            
        }

        private static void DeviceDiscovered(object sender, DeviceEventArgs e)
        {
            //if (_deviceList.All(x => x.Id != e.Device.Id))
            //{
            Debug.WriteLine(e.Device.Name);
                //_deviceList.Add(e.Device.);
            //}
        }
        //private static void DeviceDiscovered(object sender, DeviceDiscoveredEventArgs e) // --> scan
        //{
        //    
        //}

        //public static List<ICharacteristic> GetCharacteristics()
        //{
        //    return _characteristics;
        //}





        //private static void BluetoothAdapterOnDeviceConnected(object sender, DeviceConnectionEventArgs deviceConnectionEventArgs) // --> connect
        //{
        //    deviceConnectionEventArgs.Device.ServiceDiscovered += DeviceOnServiceDiscovered;
        //    deviceConnectionEventArgs.Device.DiscoverServices();

        //}

        //private static void DeviceOnServiceDiscovered(object sender, ServiceDiscoveredEventArgs serviceDiscoveredEventArgs) // -->
        //{
        //    serviceDiscoveredEventArgs.Service.DiscoverCharacteristics();
        //    serviceDiscoveredEventArgs.Service.CharacteristicDiscovered += DeviceOnCharacteristicDiscovered;
        //}

        //private static void DeviceOnCharacteristicDiscovered(object sender, CharacteristicDiscoveredEventArgs e) // -->
        //{
        //    _characteristics.Add(e.Characteristic);

        //}
    }
}
