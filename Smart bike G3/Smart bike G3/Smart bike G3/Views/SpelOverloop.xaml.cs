using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestBluethoot.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpelOverloop : ContentPage
    {
        public int Time = 0;

        public int Speed = 30;
        private bool IsPauzed;

        public SpelOverloop()
        {
            InitializeComponent();
            pictures();
            Device.StartTimer(TimeSpan.FromSeconds(1), ChangeTime);
            Device.StartTimer(TimeSpan.FromMilliseconds(100), gameplay);
            

        //protected override void OnAppearing()
        //{

        //    if (Bluetooth.BleStatus != AdapterConnectStatus.Connected)
        //    {
        //        Navigation.PushAsync(new NoSensorPage());
        //    }
        //    base.OnAppearing();
        //}

            // If there is new data -> Read sensor
            Sensor.NewDataSpeed += ((s, e) =>
            {
                Speed = e;
            });
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
            if (!IsPauzed)
            {
                if (lblUWin.IsVisible == false)
                {
                    Time += 1;
                    var dateTime = DateTime.MinValue.AddSeconds(Time);
                    if (dateTime.Minute >= 1)
                    {
                        lblTime.Text = $"{dateTime.Minute}min{dateTime.Second}";
                        return true;

                    }
                    lblTime.Text = $"{dateTime.Second} sec";
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool gameplay()
        {
            if (!IsPauzed)
            {
                int minSpeed = 15;
                lblSnelheid.Text = Speed.ToString();



                Device.BeginInvokeOnMainThread(() =>
                {
                   
                    var i = new GradientStopCollection();

                    startLucht = new GradientStop(startLucht.Color, startLucht.Offset - (float)0.001 * (Speed - minSpeed));
                    stopWater = new GradientStop(stopWater.Color, stopWater.Offset - (float)0.001 * (Speed - minSpeed));
                    i.Add(startLucht);
                    i.Add(stopWater);

                    waterBrush.GradientStops = i;
                    water.Fill = new LinearGradientBrush(i, new Point(0.5, 0), new Point(0.5, 1));

                    
                });

                if (stopWater.Offset > 1)
                {
                    startLucht.Offset = 1 - (float)0.01;
                    stopWater.Offset = 1 - (float)0;
                }

                if (startLucht.Offset < 0)
                {

                    var dateTime = DateTime.MinValue.AddSeconds(Time);
                    lblScore.Text = $"{dateTime.Minute}min{dateTime.Second}";
                    //btnRestart.IsEnabled = false;

                    Repository.AddResultsGame(3, Convert.ToInt32(Time), 0);

                    //Thread.Sleep(3000);
                    Navigation.PushAsync(new Scorebord(Time)); // push to scoreboard
                    return false;
                    
                    startLucht.Offset = 1 - (float)0.01;
                    stopWater.Offset = 1 - (float)0;
                    
                }

            }
            return true;
        }

       

        private void btnPauze_Clicked(object sender, EventArgs e)
        {
            if (IsPauzed)
            {
                IsPauzed = false;

            }
            else
            {
                IsPauzed = true;
            }
            GridPause.IsVisible = IsPauzed;
            pauzedFrame.IsVisible = IsPauzed;
            GridHelpBackGround.IsVisible = IsPauzed;

        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(); 
        }

        private void resumeBtn_Clicked(object sender, EventArgs e)
        {
            pauzedFrame.IsVisible = false;
            IsPauzed = false;
            GridHelpBackGround.IsVisible = false;
            GridPause.IsVisible = false;
        }

        private void quitBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChooseGame());
        }

        private void pauseBtn_Clicked(object sender, EventArgs e)
        {
            GridHelpBackGround.IsVisible = true;
            GridPause.IsVisible = true;
        }
    }
}
