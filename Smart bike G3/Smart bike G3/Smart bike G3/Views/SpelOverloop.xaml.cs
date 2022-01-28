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

        public SpelOverloop()
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
                //Action<double> moveWater = tInput =>  startLucht.Offset = (float)tInput;
                //Action<double> moveWater2 = tInput2 => stopWater.Offset = (float)tInput2;

                //water.Animate(name: "move", callback: moveWater, start: 0, end: 1, length: 1000, easing: Easing.BounceOut);
                //water.Animate(name: "move2", callback: moveWater2, start: 0, end: 1, length: 1000, easing: Easing.BounceOut);


                //startLucht.Offset -= (float)0.001 * (speed - 7);
                //stopWater.Offset -= (float)0.001 * (speed - 7);
                var i = new GradientStopCollection();

                startLucht = new GradientStop(startLucht.Color, startLucht.Offset - (float)0.001 * (Speed - minSpeed));
                stopWater = new GradientStop(stopWater.Color, stopWater.Offset -(float)0.001 * (Speed - minSpeed));
                i.Add(startLucht);
                i.Add(stopWater);

                waterBrush.GradientStops = i;
                water.Fill = new LinearGradientBrush(i, new Point(0.5,0), new Point(0.5, 1));

                //lblVolume.Text = Math.Round(100-((stopWater.Offset/1 )*100),0).ToString();

                //Waves.TranslationY = 150 - 300.0 * (Convert.ToDouble(lblVolume.Text) / 100.0);
                //Waves.ScaleX = 1 + Convert.ToDouble(lblVolume.Text) * 0.3 / 100.0 ;
                //Waves.TranslationX = -50 - Convert.ToDouble(lblVolume.Text) * 20.0 / 100.0;
            });

            if (stopWater.Offset > 1)
            {
                startLucht.Offset = 1 - (float)0.01;
                stopWater.Offset = 1- (float)0;
            }

            if (startLucht.Offset < 0)
            {
                    // u win
                   

                    

                    //lblUWin.IsVisible = true;
                    //btnRestart.IsVisible = true;

                    var dateTime = DateTime.MinValue.AddSeconds(Time);
                    lblScore.Text = $"{dateTime.Minute}min{dateTime.Second}";
                    //btnRestart.IsEnabled = false;

                    Repository.AddResultsGame(3, Convert.ToInt32(Time), 0);
                    
                    //Thread.Sleep(3000);
                    Navigation.PushAsync(new Scorebord(Time)); // push to scoreboard
                    return false;
                    // cool effect of zo...
                    

                    //lblVolume.Text = "100";
                    // reset to empty for test
                    startLucht.Offset = 1- (float)0.01;
                    stopWater.Offset = 1- (float)0;
                    //Waves.TranslationY = 150;
                    //Waves.TranslationY = 150 - 300.0 * (Convert.ToDouble(lblVolume.Text) / 100.0);
                    //Waves.ScaleX = 1 + Convert.ToDouble(lblVolume.Text) * 0.3 / 100.0;
                    //Waves.TranslationX = -50 - Convert.ToDouble(lblVolume.Text) * 20.0 / 100.0;
                }

            
            return true;
        }

        //private async void AbsLayBack_Tabbed(object sender, EventArgs e)
        //{
        //    Game lastuser = await Repository.GetLastUserAsync();
        //    if (lastuser.User == null)
        //    {
        //        await Repository.DeleteAsync(lastuser.id);
        //    }
        //    //AbsLayBack.Scale = 1.5;
        //    Navigation.PopAsync();
            
        //}

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(); // root page moet veranderd worden.
        }

        private void resumeBtn_Clicked(object sender, EventArgs e)
        {
            GridPause.IsVisible = false;
            pauzedFrame.IsVisible = false;
        }

        private void quitBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChooseGame());
        }

        private void pauseBtn_Clicked(object sender, EventArgs e)
        {
            GridPause.IsVisible = true;
            pauzedFrame.IsVisible = true;
        }
    }
}