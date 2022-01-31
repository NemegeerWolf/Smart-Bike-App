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
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
       
        public Search()
        {
            InitializeComponent();

            
            Bluetooth.StopNotify();
            listView.ItemsSource = Bluetooth.Scan();


          
        }
    
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var n = (BleList)e.Item;
            
                    var check = await DisplayAlert("", "Connecting to  [" + n.Name+ "]", "ok", "cancel");

                    if (check)
                    {
                        Sensor.SensorName = n.Name;
                        Bluetooth.Connect(n);
                        Navigation.PopToRootAsync();
                    }
           
        }
       
        
        
    }
}
