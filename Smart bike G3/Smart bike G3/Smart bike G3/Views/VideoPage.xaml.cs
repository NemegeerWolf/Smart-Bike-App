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
using TestBluethoot.Services;
using System.Linq;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        double speed;
        int count = 0;
        List<double> speedOvertime = new List<double>();

        public VideoPage()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                Cross.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Cross.png");
                BackLft.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundScore2.png");
                BackRgt.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundScore1.png");
                SetVideo();
                NavigationPage.SetHasNavigationBar(this, false);
                Sensor.NewDataSpeed += ((s, e) =>
                {
                    speed = e;
                });

            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        private bool playing = false;

     
        private async Task SetVideo()
        {
            string vidId = ChooseVideo.VideoId;
            await SetYoutubeSource(vidId);  
        }

        private void SetAudio(bool audioBool)
        {
            if (audioBool == true){
                video.Volume = 0;
                audio.Source = "ms-appx:///testaudio.mp3";
            }
        }


        private void Vid_MediaOpened(object sender, EventArgs e)
        {
            BackLft.IsVisible = false;
            BackRgt.IsVisible = false;
            loading.IsVisible = false;
            speedframe.IsVisible = true;
            playing = true;
            speedlbl.Text = "0 km/h";
           // Device.StartTimer(TimeSpan.FromMilliseconds(1000), FixAutoplay);
            Device.StartTimer(TimeSpan.FromMilliseconds(2000), IsCycling);
        }


        private void Vid_MediaEnded(object sender, EventArgs e)
        {
            playing = false;
            video.IsVisible = false;
            speedframe.IsVisible = false;
            BackLft.IsVisible = true;
            BackRgt.IsVisible = true;
            audio.Stop();
            double average = CheckEnoughData(speedOvertime);
            
            double distance = CalcDistance(average, ChooseVideo.VideoDur);
            Avglbl.Text = $"{average} km/u";
            Dislbl.Text = $"{distance} m";
            resultAvg.IsVisible = true;
            resultDis.IsVisible = true;
            Debug.WriteLine(average);
        }

        private double CheckEnoughData(List<double> list)
        {
            if (speedOvertime.Count != 0)
            {
                return Queryable.Average(speedOvertime.AsQueryable());
            }
            else
            {
                return 0;
            }
        }

        private double CalcDistance(double average , string duration)
        {
            double MeterPerSecond = speed / 3.6;
            int minutes = int.Parse(duration.Split(':')[0]);
            int seconds = int.Parse(duration.Split(':')[1]);
            int totalSeconds = (minutes * 60) + seconds;
            return  Math.Round(MeterPerSecond * totalSeconds);

        }

        private bool FixAutoplay()
        {
            Device.BeginInvokeOnMainThread(() => {
                video.Pause();
                video.IsLooping = false;
            });
            return false;
        }

        private bool IsCycling()
        {
            //Random rand = new Random();
            //speed = rand.Next(10, 56);
            count += 1;
            if (count == 30)
            {
                speedOvertime.Add(speed);
                count = 0;
            }
            speedlbl.Text = $"{speed} km/u";
            if (playing)
            {
                if (speed > 1)
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

        private async Task SetYoutubeSource(string vidId)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(vidId);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
            video.Source = streamInfo.Url;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Cross.Scale = 0.8;
        }
    }
}