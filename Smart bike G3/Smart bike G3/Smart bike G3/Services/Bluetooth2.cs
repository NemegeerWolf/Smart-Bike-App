using Android.Bluetooth;
using Android.Content;
using Android.Runtime;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_bike_G3.Services
{
    class Bluetooth2
    {
        static BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
        private static BluetoothAdapter.ILeScanCallback DeviceDiscovered;
        private static BluetoothSocket _socket;
        private static List<BluetoothDevice> listDevices;
        private static BluetoothGattCallback callback;


        

    public static void Scan()
        {

            //BluetoothDevice device = (from bd in adapter.BondedDevices
            //                          where bd.Name == "NameOfTheDevice"
            //                          select bd).FirstOrDefault();

            //if (device == null)
            //    throw new Exception("Named device not found.");




        }
        


        public static async Task Connect(int index)
        {

            //BluetoothDevice device = (from bd in adapter.BondedDevices
            //                          where bd.Name == "NameOfTheDevice"
            //                          select bd).FirstOrDefault();
            try
            {
                var device = listDevices[index];
                var u = device.GetUuids()[0];

                if (device == null)
                    throw new Exception("Named device not found.");

                _socket = device.CreateInsecureRfcommSocketToServiceRecord(u.Uuid);
                var p = _socket.RemoteDevice.BondState;
                if (!_socket.IsConnected)
                    _socket.Connect();
                var _stream = _socket.InputStream;
                var _reader = new InputStreamReader(_stream);
            }
            catch (IOException e)
            {
                Debug.WriteLine(e);
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

        public static List<string> GetDevices()
        {
            
            listDevices = adapter.BondedDevices.ToList();
            List<string> listDeviceNames = new List<string>();
            foreach(BluetoothDevice device in listDevices)
            {
                listDeviceNames.Add( device.Name);
            }
            return listDeviceNames;
        }

        public static async Task<string> GetData()
        {
            byte[] buffer = new byte[200];
            // Read data from the device
            int rawdata =  await _socket.InputStream.ReadAsync(buffer, 0, buffer.Length);

            //using (var rawdata3 = _socket.InputStream)
            //{
            //    var _ist = (rawdata3 as InputStreamInvoker).BaseInputStream;
            //    var aa = 0;
            //    if ((aa = _ist.Available()) > 0)
            //    {
            //        var nn = _ist.Read(buffer, 0, aa);
            //        System.Array.Resize(ref buffer, nn);
            //    }
            //}
            return "12";
            //int rawdata2 = await (Stream)_socket.InputStream;



            //using (StreamReader reader = new StreamReader(rawdata.))
            //string data = rawdata.
        }


        //private static void DeviceDiscovered(object sender, DeviceDiscoveredEventArgs e) // --> scan
        //{
        //    if (_deviceList.All(x => x.Id != e.Device.Id))
        //    {
        //        _deviceList.Add(e.Device);
        //    }
        //}
    }
}
