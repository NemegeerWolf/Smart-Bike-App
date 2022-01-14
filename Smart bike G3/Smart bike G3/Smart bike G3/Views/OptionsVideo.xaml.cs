using System;
using System.Collections.Generic;
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

        }

        private void Loadpictures()
        {
            imgbtnFirst.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.video1.png");
            imgbtnSecond.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.video1.png");
            imgbtnThird.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.video1.png");
            imgbtnFourth.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.video1.png");
        }

        private void BtnFourth_Clicked(object sender, EventArgs e)
        {
            
            VideoId = 4;
            Navigation.PushAsync(new VideoPage());
        }

        private void BtnThird_Clicked(object sender, EventArgs e)
        {
            VideoId= 3;
            Navigation.PushAsync(new VideoPage());
        }

        private void BtnSecond_Clicked(object sender, EventArgs e)
        {
            VideoId = 2;
            Navigation.PushAsync(new VideoPage());
        }

        private void BtnFirst_Clicked(object sender, EventArgs e)
        {
            VideoId = 1;
            Navigation.PushAsync(new VideoPage());
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            AbsLayBack.Scale = 1.5;
            Navigation.PopAsync();
        }
    }
}