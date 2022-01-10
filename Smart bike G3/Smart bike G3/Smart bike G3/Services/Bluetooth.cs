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
    class Bluetooth
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
            //scanCallback.OnScanResult(ScanCallbackType.AllMatches, null);

            // scanner = adapter.BluetoothLeScanner;
            //scanner.StartScan(scanCallback);

            //adapter.StartLeScan(null);
            //adapter.StartDiscovery();
            

            //BluetoothDevice device = (from bd in adapter.BondedDevices
            //                          where bd.Name == "NameOfTheDevice"
            //                          select bd).FirstOrDefault();

            //if (device == null)
            //    throw new Exception("Named device not found.");




        }



        public async Task Connect(int index)
        {

  /**
                BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
                if (adapter == null) Debug.WriteLine("No Bluetooth adapter found.");
                else if (!adapter.IsEnabled) Debug.WriteLine("Bluetooth adapter is not enabled.");

                List<BluetoothDevice> L = new List<BluetoothDevice>();
                foreach (BluetoothDevice d in adapter.BondedDevices)
                {
                    Debug.WriteLine("D: " + d.Name + " " + d.Address + " " + d.BondState.ToString());
                    L.Add(d);
                }

                //BluetoothDevice device = null;
                //device = L.Find(j => j.Name == "PC-NAME");
                var device = listDevices[index];
                var u = device.GetUuids()[0];

                if (device == null) Debug.WriteLine("Named device not found.");
                else
                {
                    Debug.WriteLine("Device has been found: " + device.Name + " " + device.Address + " " + device.BondState.ToString());
                }

                _socket = device.CreateRfcommSocketToServiceRecord(u.Uuid);
                await _socket.ConnectAsync();

                if (_socket != null && _socket.IsConnected) Debug.WriteLine("Connection successful!");
                else Debug.WriteLine("Connection failed!");

                var inStream = (InputStreamInvoker)_socket.InputStream;
                var outStream = (OutputStreamInvoker)_socket.OutputStream;


                if (_socket != null && _socket.IsConnected)
                {
                    Task t = new Task(() => Listen(inStream));
                    t.Start();
                }
                else throw new Exception("Socket not existing or not connected.");
            }

            private static async void Listen(Stream inStream)
            {
                bool Listening = true;
                Debug.WriteLine("Listening has been started.");
                byte[] uintBuffer = new byte[sizeof(uint)]; // This reads the first 4 bytes which form an uint that indicates the length of the string message.
                byte[] textBuffer; // This will contain the string message.

                // Keep listening to the InputStream while connected.
                while (Listening)
                {
                    try
                    {
                        // This one blocks until it gets 4 bytes.
                        await inStream.ReadAsync(uintBuffer, 0, uintBuffer.Length);
                        uint readLength = BitConverter.ToUInt32(uintBuffer, 0);

                        textBuffer = new byte[readLength];
                        // Here we know for how many bytes we are looking for.
                        await inStream.ReadAsync(textBuffer, 0, (int)readLength);

                        string s = Encoding.UTF8.GetString(textBuffer);
                        Debug.WriteLine("Received: " + s);
                    }
                    catch (Java.IO.IOException e)
                    {
                        Debug.WriteLine("Error: " + e.Message);
                        Listening = false;
                        break;
                    }
                }
                Debug.WriteLine("Listening has ended.");
            }

            BluetoothDevice device = (from bd in adapter.BondedDevices
                                      where bd.Name == "NameOfTheDevice"
                                      select bd).FirstOrDefault();
            
            */
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



        //public async Task<int> getdate(IDevice device = null)
        //{
        //    if(device == null)
        //    {
        //        device = BluetoothAdapter.ConnectedDevices.First();
        //        Characteristics[0].StartUpdates();
        //        return Characteristics[0].Value();
        //    }

        //}

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

        //private static void DeviceDiscovered(object sender, DeviceDiscoveredEventArgs e) // --> scan
        //{
        //    if (_deviceList.All(x => x.Id != e.Device.Id))
        //    {
        //        _deviceList.Add(e.Device);
        //    }
        //}
    }
}
