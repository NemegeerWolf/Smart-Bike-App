
using Quick.Xamarin.BLE;
using Quick.Xamarin.BLE.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestBluethoot.Models;
using TestBluethoot.Views;
using Xamarin.Forms;

namespace TestBluethoot.Services
{
    public class BluethootBLE
    {
        public static AdapterConnectStatus BleStatus;
        List<IGattCharacteristic> AllCharacteristics = new List<IGattCharacteristic>();
        IGattCharacteristic SelectCharacteristic = null;
        List<CharacteristicsList> CharacteristicsList = new List<CharacteristicsList>();
        string UuidCharacteristic;
        bool isnotify = false;

        public string RawData;
        public double Speed;

        public BluethootBLE(string uuidCharacteristic)
        {
            UuidCharacteristic = uuidCharacteristic;
            Search.ble.AdapterStatusChange += Ble_AdapterStatusChange;
            Search.ble.ServerCallBackEvent += Ble_ServerCallBackEvent;
            
            
        }

        
        //public Service()
        //{
        //    InitializeComponent();
            
        //    listView.ItemsSource = CharacteristicsList;

        //}

        private void Ble_ServerCallBackEvent(string uuid, byte[] value)
        {
            Device.BeginInvokeOnMainThread(() => {
                if (SelectCharacteristic != null)
                {
                    if (SelectCharacteristic.Uuid == uuid)
                    {
                        string str = BitConverter.ToString(value);
                        RawData = str;
                        Speed = Convert.ToInt32(str.Substring(-5,2 ),16)+ Convert.ToInt32(str.Substring(-2, 2), 16);
                        //info_read.Text = "CallBack UUID:" + str;
                    }
                }
            });

        }

        private void Ble_AdapterStatusChange(object sender, AdapterConnectStatus e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                Search.BleStatus = e;
                if (Search.BleStatus == AdapterConnectStatus.Connected)
                {
                
                    ReadCharacteristics();
                
                }

            });
        }


        public void Notify()
        {
            ReadCharacteristics();
            SelectCharacteristic = AllCharacteristics.Find(IGattCharacteristic => IGattCharacteristic.Uuid == UuidCharacteristic);
              ToggleNotify();
        }

        void ReadCharacteristics()
        {
            Search.ConnectDevice.CharacteristicsDiscovered(cha =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    AllCharacteristics.Add(cha);
                    CharacteristicsList.Add(new CharacteristicsList(cha.Uuid, cha.CanRead(), cha.CanWrite(), cha.CanNotify(), cha.ToString()));
                });
            });
        }
        
       

        

        //private void read_Clicked(object sender, EventArgs e)
        //{
        //    if (SelectCharacteristic != null)
        //    {
        //        SelectCharacteristic.ReadCallBack();
        //    }
        //}
        //private void write_Clicked(object sender, EventArgs e)
        //{
        //    var bytearray = StringToByteArray(info_write.Text);
        //    if (bytearray == null)
        //    {
        //        DisplayAlert("", "Input format error", "ok");
        //        return;
        //    }
        //    SelectCharacteristic.Write(bytearray);

        //}
        private void ToggleNotify()
        {
            if (SelectCharacteristic != null)
            {
                if (isnotify == false)
                {
                    isnotify = true;
                 
                    SelectCharacteristic.NotifyEvent += SelectCharacteristic_NotifyEvent;
                    SelectCharacteristic.Notify();
                }
                //else
                //{
                //    isnotify = false;
                    
                //    SelectCharacteristic.StopNotify();
                //    SelectCharacteristic.NotifyEvent -= SelectCharacteristic_NotifyEvent;
                //}

            }
        }

        private void SelectCharacteristic_NotifyEvent(object sender, byte[] value)
        {
            Device.BeginInvokeOnMainThread(() => {
                string str = BitConverter.ToString(value);
                RawData = str;
                Speed = Convert.ToInt32(str.Substring(-5, 2), 16) + Convert.ToInt32(str.Substring(-2, 2), 16);
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
    }
}

