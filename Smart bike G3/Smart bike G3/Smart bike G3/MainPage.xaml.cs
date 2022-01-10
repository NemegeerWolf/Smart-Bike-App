using Android.Bluetooth;
using Smart_bike_G3.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smart_bike_G3
{
    public partial class MainPage : ContentPage
    {
        Bluetooth b = new Bluetooth();
        public MainPage()
        {
            InitializeComponent();
            
            
        }

        private async void setup()
        {
            //await Bluethoot3.Scan();
            if (b.GetDevices() == null)
                pk.ItemsSource =new List<String> {"None"};
            else
                
                
                pk.ItemsSource = b.GetDevices();  
        }

        private async void pk_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            //lbl.Text =  Bluethoot.GetCharacteristics().ToString();
            await b.Connect(pk.SelectedIndex);
            lbl.Text = await b.GetData(pk.SelectedIndex);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            setup();
        }
    }
}
