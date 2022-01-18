﻿using Newtonsoft.Json;
using Smart_bike_G3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-Regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsVideo : ContentPage
    {

        public static int VideoId;
        public OptionsVideo()
        {
            InitializeComponent();

            Loadpictures();

            imgbtnFirst.Clicked += BtnFirst_Clicked;
            imgbtnSecond.Clicked += BtnSecond_Clicked;
            imgbtnThird.Clicked += BtnThird_Clicked;
            imgbtnFourth.Clicked += BtnFourth_Clicked;
            
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);

            //btnSettings.Clicked += BtnSettings_Clicked;

            TapGestureRecognizer tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += AbsLaSetting_Tabbed;
            AbsLaySettings.GestureRecognizers.Add(tapGestureRecognizer1);

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

        private void Loadpictures()
        {
            int videoId = OptionsVideo.VideoId;
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videoUrls.txt");
            List<VideoSettings> settings = JsonConvert.DeserializeObject<List<VideoSettings>>(File.ReadAllText(fileName));
            List<string> urls = new List<string>();

            foreach (var i in settings)
            {
                urls.Add(i.vid.Url);
            }
            imgbtnFirst.Source = getThumbnail(urls[0]);
            imgbtnSecond.Source = getThumbnail(urls[1]);
            imgbtnThird.Source = getThumbnail(urls[2]);
            imgbtnFourth.Source = getThumbnail(urls[3]);
        }
        private string getThumbnail(string url)
        {
            string vidId = url.Split('=')[1].Split('?')[0].Split('&')[0];
            return $"https://img.youtube.com/vi/{vidId}/maxresdefault.jpg";
        }


        private void BtnFourth_Clicked(object sender, EventArgs e)
        {
            
            VideoId = 4;
            Navigation.PushAsync(new VideoExplanation());
        }

        private void BtnThird_Clicked(object sender, EventArgs e)
        {
            VideoId= 3;
            Navigation.PushAsync(new VideoExplanation());
        }

        private void BtnSecond_Clicked(object sender, EventArgs e)
        {
            VideoId = 2;
            Navigation.PushAsync(new VideoExplanation());
        }

        private void BtnFirst_Clicked(object sender, EventArgs e)
        {
            VideoId = 1;
            Navigation.PushAsync(new VideoExplanation());
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            AbsLayBack.Scale = 1.5;
            Navigation.PopAsync();
        }
    }
}