using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-Regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseVideo : ContentPage
    {

        public static string VideoId;
        public static string VideoDur;
        public ChooseVideo()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                ImgBackground.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Background.png");
                SetLoading();
                LoadThumbnails(0);
                LoadThumbnails(1);
            
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
                AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);

                imgHelp.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.help.png");
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

        private void SetLoading()
        {
            List<Thumbnail> thumbnails = new List<Thumbnail>();
            for (var i = 0; i < 3; i++)
            {
                thumbnails.Add(new Thumbnail()
                {
                    Duration = "",
                    Playbuttn = null,
                    Picture = "Loading.png",
                    VideoId = "",
                });
            }
            lvwEnvVideos.ItemsSource = thumbnails;
            lvwShortMovies.ItemsSource = thumbnails;
        }

        private async Task LoadThumbnails(int playlistId)
        {

            List<Thumbnail> thumbnails = new List<Thumbnail>();
            List<string> ids = await YoutubeRepository.GetPlaylist(playlistId);
            foreach (var i in ids)
            {
                thumbnails.Add(new Thumbnail() { Duration = await SetTime(i),
                Playbuttn = ImageSource.FromResource(@"Smart_bike_G3.Assets.Asset2.png"),
                Picture = GetThumbnail(i), VideoId = i
            });
            }
            if (playlistId == 0)
            {
                lvwEnvVideos.ItemsSource = thumbnails;
            }
            else
            {
                lvwShortMovies.ItemsSource = thumbnails;
            }


        }
        private string GetThumbnail(string vidId) {
            
            
            if (RemoteFileExists($"https://img.youtube.com/vi/{vidId}/maxresdefault.jpg"))
            {
                return $"https://img.youtube.com/vi/{vidId}/maxresdefault.jpg";
            }
            else
            {
                return $"https://img.youtube.com/vi/{vidId}/hqdefault.jpg";

            }
        }

        private async Task<string> SetTime(string vidId)
        {
            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync(vidId);
            var duration = video.Duration.ToString().Remove(0, 3);
            return duration; 
        }

        private bool RemoteFileExists(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            AbsLayBack.Scale = 1.5;
            Navigation.PushAsync(new VideoOrGame());
        }

        //private void lvwEnvVideos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    Console.WriteLine("selected Env videos");
        //    Console.WriteLine(lvwEnvVideos.SelectedItem.ToString());
        //    Thumbnail item = (Thumbnail)lvwEnvVideos.SelectedItem;
        //    Console.WriteLine(item.VideoId);
        //    VideoId = item.VideoId;
        //    VideoDur = item.Duration;
        //    Navigation.PushAsync(new VideoPage());
        //}

        //private void lvwShortMovies_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    Console.WriteLine("selected shortmovie");
        //    Console.WriteLine(lvwShortMovies.SelectedItem.ToString());
        //    Thumbnail item = (Thumbnail)lvwShortMovies.SelectedItem;
        //    Console.WriteLine(item.VideoId);
        //    VideoId = item.VideoId;
        //    VideoDur = item.Duration;
        //    Navigation.PushAsync(new VideoPage());
        //}

        private void imgHelp_Clicked(object sender, EventArgs e)
        {
            GridHelp.IsVisible = true;
            GridHelpBackGround.IsVisible = true;
        }

        private void btnOk_Clicked(object sender, EventArgs e)
        {
            GridHelp.IsVisible = false;
            GridHelpBackGround.IsVisible = false;
        }

        private void lvwShortMovies_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Console.WriteLine("selected shortmovie");
            Console.WriteLine(lvwShortMovies.SelectedItem.ToString());
            Thumbnail item = (Thumbnail)lvwShortMovies.SelectedItem;
            Console.WriteLine(item.VideoId);
            VideoId = item.VideoId;
            VideoDur = item.Duration;
            Navigation.PushAsync(new VideoPage());
            lvwShortMovies.SelectedItem = null;
        }

        private void lvwEnvVideos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Console.WriteLine("selected Env videos");
            Console.WriteLine(lvwEnvVideos.SelectedItem.ToString());
            Thumbnail item = (Thumbnail)lvwEnvVideos.SelectedItem;
            Console.WriteLine(item.VideoId);
            VideoId = item.VideoId;
            VideoDur = item.Duration;
            Navigation.PushAsync(new VideoPage());
            lvwEnvVideos.SelectedItem = null;
        }
    }
}