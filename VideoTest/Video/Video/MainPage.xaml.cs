using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Video
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            setVideo();
        }

        private void Vid_MediaOpened(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
       
                var autoEvent = new AutoResetEvent(false);
                Timer timer = new Timer(FixAutoplay, autoEvent, 1000, 0);
                
            });
        }

        private void FixAutoplay(object e)
        {
            Device.BeginInvokeOnMainThread(() => {
                video.Pause();
                setSpeed();

            });

        }

        private void setSpeed()
        {
            Random test = new Random();
            int testSpeed = test.Next(20, 25);
            speed.Text = $"{testSpeed} km/u";

            var autoEvent = new AutoResetEvent(false);
            Timer timer = new Timer(Update, autoEvent, 1000, 0);
        }

        private void setVideo()
        {
            video.Source = "ms-appx:///testvideo.mp4";
        }
       


        private void Update(object e)
        {
            setSpeed();
        }

        int toggle = 1;
        private void btnFiets_Clicked(object sender, EventArgs e)
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
        }
    
    }
}
