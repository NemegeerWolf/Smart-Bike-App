using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.Runtime;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Smart_bike_G3.Services
{
    public class Bluetooth
    {
        static BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
        private static BluetoothAdapter.ILeScanCallback DeviceDiscovered;
        private static BluetoothSocket _socket;
        private static List<BluetoothDevice> listDevices;
        private static BluetoothGattCallback callback;
        private static BluetoothLeScanner scanner;
        private static ScanCallback scanCallback;

        public  void Scan()
        {
           
        }



        public async Task Connect(int index)
        {

            try
            {
                if (adapter == null)
                    throw new Exception("No Bluetooth adapter found.");

                if (!adapter.IsEnabled)
                    throw new Exception("Bluetooth adapter is not enabled.");
                var device = listDevices[index];
                device.FetchUuidsWithSdp();
                device.CreateBond();

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



        

        public List<string> GetDevices()
        {
            
            listDevices = adapter.BondedDevices.ToList();
            List<string> listDeviceNames = new List<string>();
            foreach(BluetoothDevice device in listDevices)
            {
                listDeviceNames.Add( device.Name);
            }
            return listDeviceNames;
        }

        public async Task<string> GetData(int index)
        {
            byte[] buffer = new byte[200];
            // Read data from the device
            int rawdata =  await _socket.InputStream.ReadAsync(buffer, 0, buffer.Length);

            string message = ASCIIEncoding.UTF8.GetString(buffer);
            

            return message;
         

           
        }


       
    }
}
