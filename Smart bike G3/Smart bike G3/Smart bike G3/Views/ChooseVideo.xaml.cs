using Newtonsoft.Json;
using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        public static int VideoId;
        public ChooseVideo()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();

                LoadThumbnails(2);
                PlayImage();

                //ImgPlay1.Clicked += BtnFirst_Clicked;
                //ImgPlay2.Clicked += BtnSecond_Clicked;
                //ImgPlay3.Clicked += BtnThird_Clicked;
                //ImgPlay4.Clicked += BtnFourth_Clicked;
            
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
                //AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);

                //btnSettings.Clicked += BtnSettings_Clicked;

                //TapGestureRecognizer tapGestureRecognizer1 = new TapGestureRecognizer();
                //tapGestureRecognizer1.Tapped += AbsLaSetting_Tabbed;
                //AbsLaySettings.GestureRecognizers.Add(tapGestureRecognizer1);
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        private void PlayImage()
        {
            //ImgPlay1.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Asset2.png");
            //ImgPlay2.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Asset2.png");
            //ImgPlay3.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Asset2.png");
            //ImgPlay4.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Asset2.png");
        }

        private async void AbsLaSetting_Tabbed(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Geef de code", "pincode", maxLength: 4, keyboard: Keyboard.Numeric);
            if (result != null)
            {
                if (Int32.Parse(result) == 8000)
                {
                    Debug.WriteLine("oké");
                    await Navigation.PushAsync(new VideoAdminPage());
                }
                else
                {
                    await DisplayAlert("Foutieve code", "", "OK");

                }
            }
        }

        private async Task LoadThumbnails(int playlistId)
        {
            List<Thumbnail> thumbnails = new List<Thumbnail>();
            Thumbnail thumbnial = new Thumbnail();
            List<string> ids = await YoutubeRepository.GetPlaylist(playlistId);
            foreach (var i in ids)
            {
                thumbnial.Duration = SetTime(i);
                thumbnails.Add();
            }
            
         
        }
        private void setThumbnail(string url,ImageButton btn) {
            string vidId = GetIDFromUrl(url);
            
            if (RemoteFileExists($"https://img.youtube.com/vi/{vidId}/maxresdefault.jpg"))
            {
                btn.Source = $"https://img.youtube.com/vi/{vidId}/maxresdefault.jpg";
            }
            else
            {
                btn.Source = $"https://img.youtube.com/vi/{vidId}/hqdefault.jpg";

            }
            Debug.Write(btn.Source);
        }

        private async Task<string> SetTime(string vidId)
        {
            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync(vidId);
            var duration = video.Duration.ToString().Remove(0, 3);
            return duration; 
        }

        private string GetIDFromUrl(string url)
        {
            return url.Split('=')[1].Split('?')[0].Split('&')[0];
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



        //private void BtnFourth_Clicked(object sender, EventArgs e)
        //{
            
        //    VideoId = 4;
        //    //Navigation.PushAsync(new VideoExplanation());
        //    Navigation.PushAsync(new VideoPage());
        //}

        //private void BtnThird_Clicked(object sender, EventArgs e)
        //{
        //    VideoId= 3;
        //    //Navigation.PushAsync(new VideoExplanation());
        //    Navigation.PushAsync(new VideoPage());
        //}

        //private void BtnSecond_Clicked(object sender, EventArgs e)
        //{
        //    VideoId = 2;
        //    //Navigation.PushAsync(new VideoExplanation());
        //    Navigation.PushAsync(new VideoPage());
        //}

        //private void BtnFirst_Clicked(object sender, EventArgs e)
        //{
        //    VideoId = 1;
        //    //Navigation.PushAsync(new VideoExplanation());
        //    Navigation.PushAsync(new VideoPage());
        //}

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            AbsLayBack.Scale = 1.5;
            Navigation.PushAsync(new VideoOrGame());
        }
    }
}