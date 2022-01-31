using Quick.Xamarin.BLE.Abstractions;
using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using Smart_bike_G3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestBluethoot.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpelOverloop : ContentPage
    {
        public int Time = 0;

        public int Speed = 30;

        public SpelOverloop()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                pictures();
                Device.StartTimer(TimeSpan.FromSeconds(1), ChangeTime);
                Device.StartTimer(TimeSpan.FromMilliseconds(100), gameplay);
                //TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                //tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
                //AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);
                

                // If there is new data -> Read sensor
                Sensor.NewDataSpeed += ((s, e) =>
                {
                    Speed = e;
                });

            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        protected override void OnAppearing()
        {

            if (Bluetooth.BleStatus != AdapterConnectStatus.Connected)
            {
                Navigation.PushAsync(new NoSensorPage());
            }
            base.OnAppearing();
        }

            private void pictures()
        {
            imgbackground.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundGlass.png");
            imgGlass.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.GlassGame.png");
            resumeBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Resume.png");
            quitBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Quit.png");
            pauseBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.pauze.png");
        }

        private bool ChangeTime()
        {
            if (lblUWin.IsVisible == false)
            {
                Time += 1;
                var dateTime = DateTime.MinValue.AddSeconds(Time);
                if(dateTime.Minute >= 1)
                {
                    lblTime.Text = $"{dateTime.Minute}min{dateTime.Second}";
                    return true;

                }
                lblTime.Text = $"{dateTime.Second} sec";
                return true;
            }
            return false;
        }

        private bool gameplay()
        {
            
            int minSpeed = 15;
            lblSnelheid.Text = Speed.ToString();
            


            Device.BeginInvokeOnMainThread  (() => {
                
                var i = new GradientStopCollection();

                startLucht = new GradientStop(startLucht.Color, startLucht.Offset - (float)0.001 * (Speed - minSpeed));
                stopWater = new GradientStop(stopWater.Color, stopWater.Offset -(float)0.001 * (Speed - minSpeed));
                i.Add(startLucht);
                i.Add(stopWater);

                waterBrush.GradientStops = i;
                water.Fill = new LinearGradientBrush(i, new Point(0.5,0), new Point(0.5, 1));

                
            });

            if (stopWater.Offset > 1)
            {
                startLucht.Offset = 1 - (float)0.01;
                stopWater.Offset = 1- (float)0;
            }

            if (startLucht.Offset < 0)
            {
                    // u win
                   

                    

                    

                    var dateTime = DateTime.MinValue.AddSeconds(Time);
                    lblScore.Text = $"{dateTime.Minute}min{dateTime.Second}";
                    

                    Repository.AddResultsGame(3, Convert.ToInt32(Time), 0);
                    
                    
                    Navigation.PushAsync(new Scorebord(Time)); // push to scoreboard
                    return false;
                    // cool effect of zo...
                    

                    
                    // reset to empty for test
                    startLucht.Offset = 1- (float)0.01;
                    stopWater.Offset = 1- (float)0;
                    
                }

            
            return true;
        }

        

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(); // root page moet veranderd worden.
        }

        private void resumeBtn_Clicked(object sender, EventArgs e)
        {
            GridHelpBackGround.IsVisible = false;
            GridPause.IsVisible = false;
        }

        private void quitBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void pauseBtn_Clicked(object sender, EventArgs e)
        {
            GridHelpBackGround.IsVisible = true;
            GridPause.IsVisible = true;
        }
    }
}