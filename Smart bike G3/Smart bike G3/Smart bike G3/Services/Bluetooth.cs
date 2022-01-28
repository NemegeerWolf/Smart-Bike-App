using Quick.Xamarin.BLE;
using Quick.Xamarin.BLE.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBluethoot.Models;
using Xamarin.Forms;

namespace Smart_bike_G3.Services
{
    public class Bluetooth
    {
        public static AdapterConnectStatus BleStatus;
        public static IBle ble;
        public static IDev ConnectDevice = null;
        public static ObservableCollection<BleList> blelist = new ObservableCollection<BleList>();
        public static List<IDev> ScanDevices = new List<IDev>();
        static List<IGattCharacteristic> AllCharacteristics = new List<IGattCharacteristic>();
        static IGattCharacteristic SelectCharacteristic = null;
        static ObservableCollection<CharacteristicsList> CharacteristicsList = new ObservableCollection<CharacteristicsList>();
        public static bool isnotify = false;
        public static event EventHandler<byte[]> NewData;
        public static event EventHandler<bool> LostConnection;
        public static event EventHandler<bool> MadeConnection;

        public static ObservableCollection<BleList> Scan()
        {
            //Device.BeginInvokeOnMainThread(() => { 
            //ble = CrossBle.Createble();
            ////when search devices
            //ble.OnScanDevicesIn += Ble_OnScanDevicesIn;
            //ble.StartScanningForDevices();
            //BleStatus = ble.AdapterConnectStatus;

            //});


            LostConnection?.Invoke("SelectCharacteristic", true);
            ScanDevices.Clear();
            blelist.Clear();
            ble = CrossBle.Createble();
            //when search devices
            ble.OnScanDevicesIn += Ble_OnScanDevicesIn;
            ble.StartScanningForDevices();
            BleStatus = ble.AdapterConnectStatus;
            return blelist;
        }



        public static void Connect(BleList device)
        {

            foreach (var dev in ScanDevices)
            {
                if (device.Uuid == dev.Uuid)
                {
                    ConnectDevice = dev;
                    ConnectDevice.ConnectToDevice();
                    ble.AdapterStatusChange += Ble_AdapterStatusChange;
                    ble.ServerCallBackEvent += Ble_ServerCallBackEvent;
                    ble.StopScanningForDevices();
                    break;
                    // listView.ItemsSource = CharacteristicsList;
                }
            }

        }

        public static ObservableCollection<BleList> GetDevices()
        {
            return blelist;
        }

        public static ObservableCollection<CharacteristicsList> GetCharacteristics()
        {
            ReadCharacteristics();
            return CharacteristicsList;
        }



        private static void Ble_OnScanDevicesIn(object sender, IDev e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                try
                {

                    if (e.Name != null)
                    {
                        var n = ScanDevices.Find(x => x.Uuid == e.Uuid);
                        if (n == null)
                        {
                            ScanDevices.Add(e);
                            blelist.Add(new BleList(e.Name, e.Uuid));
                        }

                    }
                }
                catch { }

            });
        }

        private static void Ble_ServerCallBackEvent(string uuid, byte[] value)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (SelectCharacteristic != null)
                {
                    if (SelectCharacteristic.Uuid == uuid)
                    {
                        //string str = BitConverter.ToString(value);
                        NewData?.Invoke("SelectCharacteristic", value);
                    }
                }
            });

        }

        private static void Ble_AdapterStatusChange(object sender, AdapterConnectStatus e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                BleStatus = e;
                if (BleStatus == AdapterConnectStatus.Connected)
                {

                    await Task.Delay(3000);


                    ReadCharacteristics();
                    MadeConnection?.Invoke("SelectCharacteristic", true);
                }
                if (BleStatus == AdapterConnectStatus.None)
                {
                    //await Navigation.PopToRootAsync(true);
                    LostConnection?.Invoke("SelectCharacteristic", true);
                }
            });
        }

        private static void ReadCharacteristics()
        {
            ConnectDevice.CharacteristicsDiscovered(cha =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    AllCharacteristics.Add(cha);
                    CharacteristicsList.Add(new CharacteristicsList(cha.Uuid, cha.CanRead(), cha.CanWrite(), cha.CanNotify(), cha.ToString()));
                });
            });
        }


        private static void SelectCharacteristic_NotifyEvent(object sender, byte[] value)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                string str = BitConverter.ToString(value);
                NewData?.Invoke("SelectCharacteristic", value);
            });
        }

        public static byte[] StringToByteArray(string hex)
        {
            try
            {
                return Enumerable.Range(0, hex.Length)
                                 .Where(x => x % 2 == 0)
                                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                 .ToArray();
            }
            catch { return null; }
        }


        public static byte Select_Characteristic(CharacteristicsList Characteristic)
        {
            foreach (var c in AllCharacteristics)
            {
                if (c.Uuid == Characteristic.Uuid)
                {
                    SelectCharacteristic = c;


                    byte response = 0;

                    if (SelectCharacteristic.CanRead()) response += 1;

                    if (SelectCharacteristic.CanWrite()) response += 2;

                    if (SelectCharacteristic.CanNotify()) response += 4;

                    return response;
                }
            }
            return 0;
        }

        public static void Read()
        {
            if (SelectCharacteristic != null)
            {
                SelectCharacteristic.ReadCallBack();
            }
        }
        public static void Write(string data)
        {
            var bytearray = StringToByteArray(data);
            if (bytearray == null)
            {
                throw new ArgumentNullException();

            }
            SelectCharacteristic.Write(bytearray);

        }
        public static void Notify()
        {
            if (SelectCharacteristic != null)
            {
                if (!isnotify)
                {
                    isnotify = true;
                    SelectCharacteristic.NotifyEvent += SelectCharacteristic_NotifyEvent;
                    SelectCharacteristic.Notify();
                }
            }
        }

        public static void StopNotify()
        {
            if (isnotify)
            {
                isnotify = false;
                SelectCharacteristic.StopNotify();
                SelectCharacteristic.NotifyEvent -= SelectCharacteristic_NotifyEvent;
            }
        }
    }
}
