using System;
using System.Linq;
using Xamarin.Forms;
using TestBluethoot.Services;
using TestBluethoot.Models;
using System.Threading;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using Xamarin.Forms.Xaml;
using Smart_bike_G3.Services;
using System.Diagnostics;
using Smart_bike_G3.Views;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3
{
    public partial class NoSensorPage : ContentPage
    {
        public NoSensorPage()
        {
            InitializeComponent();
            Sensor.Start();
            Bluetooth.ClearAllDelegatesOfLostConnection();
            //Navigation.PushAsync(new VideoOrGame());
            Bluetooth.MadeConnection += ((s, e) =>
            {
                Navigation.PopAsync();
                Bluetooth.LostConnection += ((se, ev) =>
                {

                    Navigation.PushAsync(new NoSensorPage());

                });

            });
        }

       

        //private void Scan()
        //{
        //    Thread thread = new Thread(() => {
        //        ObservableCollection<BleList> blelist = Bluetooth.Scan();
        //        //pk.ItemsSource = Bluetooth.Scan();
        //        if (blelist.Count == 0) Debug.WriteLine("Looking for blelist...");
        //        blelist.CollectionChanged += ConnectSensor;
        //    });
        //    thread.Start();
        //}

        //private void ConnectSensor(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if(e.NewItems != null) {
        //    if (e.NewItems.Cast<BleList>().Any(x => x.Name == "46003-81"))
        //    {
        //        Bluetooth.Connect((BleList)e.NewItems.Cast<BleList>().Where(x => x.Name == "46003-81").First());
        //        ObservableCollection<CharacteristicsList> listChar = Bluetooth.GetCharacteristics();

        //        listChar.CollectionChanged += NotifySpeed;
        //    }
        //    }

        //}

        //private void NotifySpeed(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (!Bluetooth.isnotify)
        //    {
        //        if (e.NewItems.Cast<CharacteristicsList>().Any(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb"))
        //        {
        //            Bluetooth.Select_Characteristic(Bluetooth.GetCharacteristics().Where(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb").First());

        //            Bluetooth.NewData += Sensor.GotNewdata;
        //            Sensor.NewDataCadence += changeLabel;

        //            Bluetooth.Notify();
        //        }

        //    }
        //}


        //private void changeLabel(object sender, int e)
        //{
        //    Debug.WriteLine(e.ToString() + " rpm");
        //}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());
        }
    }
}
