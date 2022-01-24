using System;
using System.Linq;
using Xamarin.Forms;
using TestBluethoot.Services;
using TestBluethoot.Models;
using System.Threading;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using TestBluethoot.Views;

namespace TestBluethoot
{
    public partial class MainPage : ContentPage
    {
        //Bluetooth b = new Bluetooth();
        public MainPage()
        {
            InitializeComponent();
            Scan();
            

        }

        //private async void setup()
        //{
        //    //await Bluethoot3.Scan();
        //    if (b.GetDevices() == null)
        //        pk.ItemsSource = new List<String> { "None" };
        //    else


        //        pk.ItemsSource = b.GetDevices();
        //}

        private async void pk_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Bluethoottest.Connect((BleList)pk.SelectedItem);
            
            ////lbl.Text =  Bluethoot.GetCharacteristics().ToString();
            //Bluethoot.Connect((IDevice)pk.SelectedItem);
            ////lbl.Text = await Bluethoot.GetData(pk.SelectedIndex);
            //lbl.Text = Bluethoot.GetCharacteristics()[4].StringValue;
        }

        private void changeLabel(object sender, int e)
        {
            lbl.Text = e.ToString() + " rpm";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new Page1());

            //List<string> i = new List<string> { };
            //foreach (CharacteristicsList c in Bluethoottest.GetCharacteristics())
            //{
            //    i.Add(c.Uuid);
            //}
            //pkc.ItemsSource = i;

            //int t = 0;
            //while (!Bluethoottest.GetCharacteristics().Any(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb"))
            //{
            //    Thread.Sleep(1000);
            //    t++;
            //    if (t > 10) 
            //    {
            //        Bluethoottest.Connect((BleList)pk.SelectedItem);
            //        Thread.Sleep(2000);
            //        lbl.Text = "failed";
            //        return;
            //    }
            //}
            //    Bluethoottest.Select_Characteristic(Bluethoottest.GetCharacteristics().Where(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb").First());

            //Bluethoottest.NewData += Sensor.GotNewdata;
            //Sensor.NewDataCadence += changeLabel;
            //Bluethoottest.Notify();
        }

        private void Scan()
        {
            Thread thread = new Thread(() => { 
            ObservableCollection<BleList> blelist = Bluethoottest.Scan();
            //pk.ItemsSource = Bluethoottest.Scan();
            blelist.CollectionChanged += ConnectSensor;
            });
            thread.Start();
        }

        private void ConnectSensor(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems.Cast<BleList>().Any(x => x.Name == "46003-81"))
            {
                Bluethoottest.Connect((BleList)e.NewItems.Cast<BleList>().Where(x => x.Name == "46003-81").First());
                ObservableCollection<CharacteristicsList> listChar = Bluethoottest.GetCharacteristics();

                listChar.CollectionChanged += NotifySpeed;
            }
            //while(!blelist.Any(x => x.Name == "46003-81"))
            //{}





            //int t = 0;
            //while (!Bluetooth.GetCharacteristics().Any(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb"))
            //{
            //    Thread.Sleep(1000);
            //    t++;
            //    if (t > 10)
            //    {
            //        ConnectSensor();
            //        return;
            //    }
            //}
            //Bluetooth.Select_Characteristic(Bluetooth.GetCharacteristics().Where(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb").First());

            //Bluetooth.NewData += Sensor.GotNewdata;
            //Bluetooth.Notify();

        }

        private void NotifySpeed(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!Bluethoottest.isnotify)
            {
                if (e.NewItems.Cast<CharacteristicsList>().Any(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb"))
            {
                Bluethoottest.Select_Characteristic(Bluethoottest.GetCharacteristics().Where(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb").First());
                
                    Bluethoottest.NewData += Sensor.GotNewdata;
                    Sensor.NewDataCadence += changeLabel;
                    Bluethoottest.Notify();
                }
                
            }
        }
    }
}
