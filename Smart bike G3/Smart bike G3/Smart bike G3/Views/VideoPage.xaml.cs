using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.Linq;
using Smart_bike_G3.Services;
using Quick.Xamarin.BLE.Abstractions;
using TestBluethoot.Services;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        double speed = 10;
        int count = 0;
        List<double> speedOvertime = new List<double>();
        bool checkSpeed;
        private bool stopped = false;
        private bool playing = false;
        private bool pauzed = false;
        int seconds;
        DateTime time;
        public VideoPage()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                Cross.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Cross.png");
                BackLft.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundScore2.png");
                BackRgt.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundScore1.png");
                SetVideo();
                Device.StartTimer(TimeSpan.FromSeconds(1), Timer);

                NavigationPage.SetHasNavigationBar(this, false);
                /*Sensor.NewDataSpeed += ((s, e) =>
                {
                    speed =e;
                });*/
                
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }



        //protected override void OnAppearing()
        //{

        //    if (Bluetooth.BleStatus != AdapterConnectStatus.Connected)
        //    {
        //        Navigation.PushAsync(new NoSensorPage());
        //    }
        //    base.OnAppearing();
        //}

        private bool Timer()
        {
            if (!stopped)
            {
                if (playing && !pauzed)
                {
                    seconds += 1;
                    time = DateTime.MinValue.AddSeconds(seconds);
                    timePassdlbl.Text = $"{TimeForDisplay(time)}/{ChooseVideo.VideoDur}";
                    return true;
                }
                return true;
            }
            return false;     
        }


        private async Task SetVideo()
        {
            string vidId = ChooseVideo.VideoId;
            await SetYoutubeSource(vidId);  
        }

        private void Vid_MediaOpened(object sender, EventArgs e)
        {
            checkSpeed = true;
            BackLft.IsVisible = false;
            BackRgt.IsVisible = false;
            loading.IsVisible = false;
            speedframe.IsVisible = true;
            playing = true;
            speedlbl.Text = "0 km/u";
            video.KeepScreenOn = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), IsCycling);
        }

        private void Vid_MediaEnded(object sender, EventArgs e)
        {
            playing = false;
            video.IsVisible = false;
            speedframe.IsVisible = false;
            BackLft.IsVisible = true;
            BackRgt.IsVisible = true;
            stopped = true;
            double average = CheckEnoughData(speedOvertime);
            double distance = CalcDistance(average, ChooseVideo.VideoDur);
            Navigation.PushAsync(new ResultsVideo(distance,average, ChooseVideo.VideoDur));
            checkSpeed = false;
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
        private bool IsCycling()
        {
            count += 1;
            if (count == 30)
            {
                speedOvertime.Add(speed);
                count = 0;
            }
            speedlbl.Text = $"{speed} km/u";
            if (playing)
            {
                if (speed > 1 & video.CurrentState == Xamarin.CommunityToolkit.UI.Views.MediaElementState.Paused)
                {
                    video.Play();
                    pauzed = false;
                }
                else if (speed < 1 & video.CurrentState == Xamarin.CommunityToolkit.UI.Views.MediaElementState.Playing)
                {
                    video.Pause();
                    pauzed = true;

                }
            }
            if (checkSpeed)
            {
                return true;

            }
            else
            {
                return false;
            }
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
            stopped = true;
            Cross.Scale = 0.8;
        }
        private string TimeForDisplay(DateTime time)
        {
            string minute = CheckDigits(time.Minute.ToString());
            string second = CheckDigits(time.Second.ToString());
            return $"{minute}:{second}";
        }
        private string CheckDigits(string str)
        {
            if (str.Length == 1)
            {
                return $"0{str}";
            }
            return str;
        }
    }
}
