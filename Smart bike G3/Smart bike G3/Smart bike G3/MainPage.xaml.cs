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
        public MainPage()
        {
            InitializeComponent();
            
            
        }

        private void setup()
        {
           // Bluethoot.Scan();
            if (Bluetooth2.GetDevices() == null)
                pk.ItemsSource =new List<String> {"None"};
            else
                
                
                pk.ItemsSource = Bluetooth2.GetDevices();  
        }

        private async void pk_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lbl.Text =  Bluethoot.GetCharacteristics().ToString();
            await Bluetooth2.Connect(pk.SelectedIndex);
            lbl.Text = await Bluetooth2.GetData();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            setup();
        }
    }
}
