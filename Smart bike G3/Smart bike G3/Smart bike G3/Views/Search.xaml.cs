using Quick.Xamarin.BLE;
using Quick.Xamarin.BLE.Abstractions;
using Smart_bike_G3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBluethoot.Models;
using TestBluethoot.Services;
using Xamarin.Forms;

namespace Smart_bike_G3
{
    public partial class Search : ContentPage
    {
        //public static AdapterConnectStatus BleStatus;
        //public static IBle ble;  
        //public static IDev ConnectDevice = null;
        //ObservableCollection<BleList> blelist = new ObservableCollection<BleList>();
        //public static List<IDev> ScanDevices = new List<IDev>();
        public Search()
        {
            InitializeComponent();

            //ble = CrossBle.Createble();           
            ////when search devices
            //ble.OnScanDevicesIn += Ble_OnScanDevicesIn;

            //BleStatus = ble.AdapterConnectStatus;
            //listView.ItemsSource = blelist;
            Bluetooth.StopNotify();
            listView.ItemsSource = Bluetooth.Scan();


          
        }
    
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var n = (BleList)e.Item;
            //foreach(var dev in Bluetooth.GetDevices())
            //{
            //    if (n.Uuid == dev.Uuid)
            //    {
                    var check = await DisplayAlert("", "Connecting to  [" + n.Name+ "]", "ok", "cancel");

                    if (check)
                    {

                        Bluetooth.Connect(n);
                        Navigation.PopToRootAsync();
                    }
            //    }
            //}
        }
       
        
        
    }
}
