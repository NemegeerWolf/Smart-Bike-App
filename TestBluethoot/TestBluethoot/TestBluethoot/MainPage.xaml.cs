using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Smart_bike_G3.Services;
using BluetoothLE.Core;
using TestBluethoot.Services;
using Quick.Xamarin.BLE;
using Quick.Xamarin.BLE.Abstractions;
using TestBluethoot.Models;
using System.Threading;

namespace TestBluethoot
{
    public partial class MainPage : ContentPage
    {
        //Bluetooth b = new Bluetooth();
        public MainPage()
        {
            InitializeComponent();

            pk.ItemsSource = Bluethoottest.Scan();

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
            List<string> i = new List<string> { };
            foreach (CharacteristicsList c in Bluethoottest.GetCharacteristics())
            {
                i.Add(c.Uuid);
            }
            pkc.ItemsSource = i;

            int t = 0;
            while (!Bluethoottest.GetCharacteristics().Any(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb"))
            {
                Thread.Sleep(1000);
                t++;
                if (t > 10) 
                {
                    Bluethoottest.Connect((BleList)pk.SelectedItem);
                    Thread.Sleep(2000);
                    lbl.Text = "failed";
                    return;
                }
            }
                Bluethoottest.Select_Characteristic(Bluethoottest.GetCharacteristics().Where(x => x.Uuid == "00002a5b-0000-1000-8000-00805f9b34fb").First());

            Bluethoottest.NewData += Sensor.GotNewdata;
            Sensor.NewDataCadence += changeLabel;
            Bluethoottest.Notify();
        }
    }
}
