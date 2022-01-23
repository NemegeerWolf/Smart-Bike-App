using Quick.Xamarin.BLE;
using Quick.Xamarin.BLE.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TestBluethoot.Services;
using TestBluethoot.Models;

namespace TestBluethoot.Views
{
    public partial class Search : ContentPage
    {
        BluethootBLE bleBluethoot;
        public static AdapterConnectStatus BleStatus;
        public static IBle ble;  
        public static IDev ConnectDevice = null;
        ObservableCollection<BleList> blelist = new ObservableCollection<BleList>();
        public static List<IDev> ScanDevices = new List<IDev>();
        public Search()
        {
            InitializeComponent();
            
            ble = CrossBle.Createble();           
            //when search devices
            ble.OnScanDevicesIn += Ble_OnScanDevicesIn;
          
            BleStatus = ble.AdapterConnectStatus;
            listView.ItemsSource = blelist;

          
        }

        private void Ble_OnScanDevicesIn(object sender, IDev e)
        {
            Device.BeginInvokeOnMainThread(() => {
                
                try
                {
                    
                    if (e.Name != null)
                    {
                        var n = ScanDevices.Find(x => x.Uuid == e.Uuid);
                        if (n==null)
                        {
                            ScanDevices.Add(e);
                            blelist.Add(new BleList(e.Name,e.Uuid));
                        }
                       
                    }
                }
                catch {}
 
            });
        } 
    
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var n = (BleList)e.Item;
            foreach(var dev in ScanDevices)
            {
                if (n.Uuid == dev.Uuid)
                {
                    var check = await DisplayAlert("", "Connecting to  [" + dev.Name+ "]", "ok", "cancel");

                    if (check)
                    {
                        ConnectDevice = dev;
                        ConnectDevice.ConnectToDevice();
                        // Navigation.PushAsync(new Service(), false);
                        Search.ble.AdapterStatusChange += Ble_AdapterStatusChange;
                        bleBluethoot = new BluethootBLE("00002a05-0000-1000-8000-00805f9b34fb");
                        Device.StartTimer(TimeSpan.FromSeconds(1), changeLabel);
                        bleBluethoot.Notify();
                    }
                }
            }
        }

        private bool changeLabel()
        {
            lblinput.Text = bleBluethoot.RawData;
            bleBluethoot.Notify();
            return true;
        }

        private void Ble_AdapterStatusChange(object sender, AdapterConnectStatus e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                Search.BleStatus = e;
                if (Search.BleStatus == AdapterConnectStatus.Connected)
                {
                    msg_txt.Text = "Success";
                    await Task.Delay(3000);
                    msg_layout.IsVisible = false;
                    listView.IsVisible = true;
                    

                }
                if (Search.BleStatus == AdapterConnectStatus.None)
                {
                   // await Navigation.PopToRootAsync(true);
                }
            });
        }

        protected override async void OnAppearing()
        {
             
            ScanDevices.Clear();
            blelist.Clear();
            
            ble.StartScanningForDevices();
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            ble.StopScanningForDevices();
            base.OnDisappearing();
        }
        private void ListView_Refreshing(object sender, EventArgs e)
        {
            ScanDevices.Clear();
            blelist.Clear();
            ScanDevices = new List<IDev>();
            blelist = new ObservableCollection<BleList>();
            listView.EndRefresh();
        }
    }
}
