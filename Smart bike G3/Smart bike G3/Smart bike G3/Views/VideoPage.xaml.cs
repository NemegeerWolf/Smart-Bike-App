using Newtonsoft.Json.Linq;
using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.Web;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        public VideoPage()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                SetVideoAndAudio();
                AddEvents();
                NavigationPage.SetHasNavigationBar(this, false);
            } 
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }

                
        }
        private bool playing = false;

        private void AddEvents()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


        private readonly Dictionary<int, string[]> Videos = new Dictionary<int, string[]>() {
            { 1 , new string[] { "07d2dXHYb94", "ms-appx:///testaudio.mp3" } },
            { 2 , new string[] { "uXUVGhqwywE", "ms-appx:///testaudio.mp3" } },
            { 3 , new string[] { "nLpjuAR_aRs", "ms-appx:///testaudio.mp3" } },
            { 4 , new string[] { "uXUVGhqwywE", "ms-appx:///testaudio.mp3" } }
        };




        private void SetVideoAndAudio()
        {
            int videoId = OptionsVideo.VideoId;
            Debug.WriteLine(videoId);
            if (Videos.ContainsKey(videoId))
            {
                bool keyValue = Videos.TryGetValue(videoId, out string[] values);
                GetYoutubeSource(values[0]);
                if (videoId == 3 || videoId == 4)
                {
                    audio.Source = values[1];
                    audio.IsLooping = true;
                }
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
                playing = true;
                speed.Text = "0 km/h";
                loading.IsVisible = false;
                speedframe.IsVisible = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(1000), SetAgain); //fix loading bug
                Device.StartTimer(TimeSpan.FromMilliseconds(1000), FixAutoplay); //fixes autoplay not working
                Device.StartTimer(TimeSpan.FromMilliseconds(1000), IsCycling);
                int videoId = OptionsVideo.VideoId;
                if (videoId == 3 || videoId == 4)
                {
                    video.Volume = 0;
                }
                
            });
        }

        private bool SetAgain()
        {
            loading.IsVisible = false;
            speedframe.IsVisible = true;
            return false;
        }

        private void Vid_MediaEnded(object sender, EventArgs e)
        {
            playing = false;
            int videoId = OptionsVideo.VideoId;
            string user = Name.User;
            Repository.AddResultsVideo(videoId, user, 400);
            Debug.WriteLine("sending data to api");
            Navigation.PushAsync(new ScorebordDistance(100));


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
            int speedVal = (int)val;
            speed.Text = $"{speedVal} km/u";
            if (playing)
            {
                if (speedVal > 1)
                {
                    video.Play();
                }
                else
                {
                    video.Pause();
                }
            }
            return true;
        }

        private async Task GetYoutubeSource(string vidId) 
        {
            var videoURL = $"https://www.youtube.com/watch?v={vidId}";
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(vidId);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
            video.Source = streamInfo.Url;  
        }
    }
}
