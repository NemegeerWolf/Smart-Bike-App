using Quick.Xamarin.BLE.Abstractions;
using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using Smart_bike_G3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestBluethoot.Models;
using TestBluethoot.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Android;


[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-Regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]



namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoOrGame : ContentPage
    {
        public static string Kind;

        public VideoOrGame()
        {
            InitializeComponent();
            Pictures();
            AddEvents();
            //if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            //{
            //    if (Bluetooth.BleStatus == AdapterConnectStatus.Connected)
            //    {

            //        //Bluetooth.LostConnection += ((s, e) =>
            //        //{

            //        //    Navigation.PushAsync(new NoSensorPage());

            //        //});
            //    }
            //    else
            //    {

                
                
            //        Navigation.PushAsync(new NoSensorPage());
            //        //  /*****UIT COMMENTAAR HALEN OM BLUETOOTH TE DOEN WERKEN!!! --> mainactivity.cs lijn 29 ook uit commentaar******/
            //    }
                
            //}
            //else
            //{
            //    Navigation.PushAsync(new NoNetworkPage());
            //}

        }

        /*protected override void OnAppearing()
        {
           
            if (Bluetooth.BleStatus == AdapterConnectStatus.Connected)
            {
                Bluetooth.ClearAllDelegatesOfMadeConnection();
            }
            base.OnAppearing();
            // Niet verwijderen (zou moeten werken maar werk om een of andere reden niet)
            //Bluetooth.LostConnection += ((s, e) =>
            //{

            //    Navigation.PushAsync(new NoSensorPage());

            //});

        }*/

        

        private void Pictures()
        {
            ImgBackground.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Background.png");
            ImgGame.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.GameConsole.png");
            ImgVideo.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Play.png");
            ImgClouds.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Clouds.png");
        }

        private void AddEvents()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            

            TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += AbsLayVideo_Tabbed;
            AbsLayVideo.GestureRecognizers.Add(tapGestureRecognizer2);

            TapGestureRecognizer tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += AbsLayGame_Tabbed;
            AbsLayGame.GestureRecognizers.Add(tapGestureRecognizer3);

            
        }

        

        private void AbsLayGame_Tabbed(object sender, EventArgs e)
        {

            Kind = "game";

            Console.WriteLine(Kind + " chosen");
            Navigation.PushAsync(new ChooseGame());
        }

        private void AbsLayVideo_Tabbed(object sender, EventArgs e)
        {
            Kind = "video";

            Console.WriteLine(Kind + " chosen");
            Navigation.PushAsync(new ChooseVideo());
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
