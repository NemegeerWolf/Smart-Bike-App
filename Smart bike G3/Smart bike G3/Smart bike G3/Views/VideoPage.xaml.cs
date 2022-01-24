using Newtonsoft.Json.Linq;
using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.IO;
using Newtonsoft.Json;
using Smart_bike_G3.Models;

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

        private async Task SetVideoAndAudio()
        {
            int videoId = ChooseVideo.VideoId;
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videoUrls.txt");
            List<VideoSettings> settings = JsonConvert.DeserializeObject<List<VideoSettings>>(File.ReadAllText(fileName));
            List<string> urls = new List<string>();
            List<int> buttns = new List<int>();

            foreach (var i in settings)
            {
                urls.Add(i.vid.Url);
                buttns.Add(i.vid.Audio);
            }

            await GetYoutubeSource(urls[videoId - 1]);

            if (buttns[videoId - 1] == 1)
            {
                video.Volume = 0;
                audio.Source = "ms-appx:///testaudio.mp3";
            }
        }


        private void Vid_MediaOpened(object sender, EventArgs e)
        {
            loading.IsVisible = false;
            speedframe.IsVisible = true;
            playing = true;
            speed.Text = "0 km/h";
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), FixAutoplay); //fixes autoplay not working
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), IsCycling);
        }


        private void Vid_MediaEnded(object sender, EventArgs e)
        {
            playing = false;
            int videoId = ChooseVideo.VideoId;
            string user = Name.User;
            Random rand = new Random();
            int score = rand.Next(1, 1000);
            audio.Stop();
            Repository.AddResultsVideo(videoId, user, score);
            Debug.WriteLine("sending data to api");
            Navigation.PushAsync(new Scorebord(score));
        }

        private bool FixAutoplay()
        {
            Device.BeginInvokeOnMainThread(() => {
                video.Pause();
                video.IsLooping = false;
                speed.Text = $"20 km/u";
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

        private async Task GetYoutubeSource(string vidUrl) 
        {
            string vidId = GetIDFromUrl(vidUrl);
            Debug.WriteLine(vidId);
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(vidId);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
            video.Source = streamInfo.Url;
        }
        private string GetIDFromUrl(string url)
        {
            return url.Split('=')[1].Split('?')[0].Split('&')[0];
        }
    }
}
