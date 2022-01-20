﻿using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpelOverloop : ContentPage
    {
        public int Time = 0;
        public SpelOverloop()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1), ChangeTime);
            Device.StartTimer(TimeSpan.FromMilliseconds(100), gameplay);
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private bool ChangeTime()
        {
            if (lblUWin.IsVisible == false)
            {
                Time += 1;
                var dateTime = DateTime.MinValue.AddSeconds(Time);
                lblTime.Text = $"{dateTime.Minute}min{dateTime.Second}";
                return true;
            }
            return false;

        }

        private bool gameplay()
        {
            int speed = 30;
            int minSpeed = 10;
            lblSnelheid.Text = speed.ToString();
            


            Device.BeginInvokeOnMainThread  (() => {
                //Action<double> moveWater = tInput =>  startLucht.Offset = (float)tInput;
                //Action<double> moveWater2 = tInput2 => stopWater.Offset = (float)tInput2;

                //water.Animate(name: "move", callback: moveWater, start: 0, end: 1, length: 1000, easing: Easing.BounceOut);
                //water.Animate(name: "move2", callback: moveWater2, start: 0, end: 1, length: 1000, easing: Easing.BounceOut);


                //startLucht.Offset -= (float)0.001 * (speed - 7);
                //stopWater.Offset -= (float)0.001 * (speed - 7);
                var i = new GradientStopCollection();

                startLucht = new GradientStop(startLucht.Color, startLucht.Offset - (float)0.001 * (speed - minSpeed));
                stopWater = new GradientStop(stopWater.Color, stopWater.Offset -(float)0.001 * (speed - minSpeed));
                i.Add(startLucht);
                i.Add(stopWater);

                waterBrush.GradientStops = i;
                water.Fill = new LinearGradientBrush(i, new Point(0.5,0), new Point(0.5, 1));

                lblVolume.Text = Math.Round(100-((stopWater.Offset/1 )*100),0).ToString();

                Waves.TranslationY = 150 - 300.0 * (Convert.ToDouble(lblVolume.Text) / 100.0);
                Waves.ScaleX = 1 + Convert.ToDouble(lblVolume.Text) * 0.3 / 100.0 ;
                Waves.TranslationX = -50 - Convert.ToDouble(lblVolume.Text) * 20.0 / 100.0;
            });

            if (stopWater.Offset > 1)
            {
                startLucht.Offset = 1 - (float)0.01;
                stopWater.Offset = 1- (float)0;
            }

            if (startLucht.Offset < 0)
            {
                    // u win
                   

                    

                    lblUWin.IsVisible = true;
                    btnRestart.IsVisible = true;

                    var dateTime = DateTime.MinValue.AddSeconds(Time);
                    lblScore.Text = $"{dateTime.Minute}min{dateTime.Second}";
                    btnRestart.IsEnabled = false;

                    Repository.AddResultsGame(2, Name.User, Convert.ToInt32(Time), 0);
                    
                    Thread.Sleep(3000);
                    Navigation.PushAsync(new ScorebordTime(Time)); // push to scoreboard
                    return false;
                    // cool effect of zo...
                    

                    lblVolume.Text = "100";
                    // reset to empty for test
                    startLucht.Offset = 1- (float)0.01;
                    stopWater.Offset = 1- (float)0;
                    Waves.TranslationY = 150;
                    Waves.TranslationY = 150 - 300.0 * (Convert.ToDouble(lblVolume.Text) / 100.0);
                    Waves.ScaleX = 1 + Convert.ToDouble(lblVolume.Text) * 0.3 / 100.0;
                    Waves.TranslationX = -50 - Convert.ToDouble(lblVolume.Text) * 20.0 / 100.0;
                }

            
            return true;
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            AbsLayBack.Scale = 1.5;
            Navigation.PopAsync();
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(); // root page moet veranderd worden.
        }
    }
}