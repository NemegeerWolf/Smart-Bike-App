﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit;
using Xamarin.Forms;

namespace Video
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            SetVideoAndAudio(1);
            AddEvents();
        }


        private void AddEvents()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);
       }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            Debug.WriteLine("works");

        }


        private readonly Dictionary<int, string[]> Videos = new Dictionary<int, string[]>() {
            { 1 , new string[] {"ms-appx:///testvideo.mp4", "ms-appx:///testaudio.mp3" } }
        };

       
       

        private void SetVideoAndAudio(int videoId)
        {
            if (Videos.ContainsKey(videoId)) 
            {
                bool keyValue = Videos.TryGetValue(videoId, out string[] values);
                video.Source = values[0];
                video.Volume = 0;
                audio.Source = values[1];
            }
            else
            {
                Debug.WriteLine("Something went wrong in setVideo");
            }
        }


        private void Vid_MediaOpened(object sender, EventArgs e)
        {

            Task.Run(() =>
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(1000),FixAutoplay);
                Device.StartTimer(TimeSpan.FromMilliseconds(1000), IsCycling);

            });
        }

        private void Vid_MediaEnded(object sender, EventArgs e)
        {
            //send distance to api
            Debug.WriteLine("sending data to api");
        }

        private bool FixAutoplay()
        {
            Device.BeginInvokeOnMainThread(() => {
                video.Pause();
                video.IsLooping = false;
                speed.Text = $"20 km/u";
                video.Play();//temp

            });
            return false;
        }




        private bool IsCycling()
        {
            Random test = new Random();
            int testSpeed = test.Next(0, 25);

            float val = testSpeed; //get value from sensor 
            //calc sensordata to km/u
            int speedVal = (int) val;
            speed.Text = $"{speedVal} km/u";
            if (speedVal > 1)
            {
                video.Play();
            }
            else
            {
                video.Pause();
            }
            return true;
        }


        //-------------TestFuncties tijdelijk-------------//



        //int toggle = 1;
        /* private void btnFiets_Clicked(object sender, EventArgs e)
         {
             Debug.WriteLine("clicked");
             if (toggle == 1)
             {
                 btnFiets.Text = "Stop";

                 video.Play();
                 toggle = 2;
             }
             else
             {
                 btnFiets.Text = "Fiets";

                 video.Pause();
                 toggle = 1;
             }
         }*/

    }
}
