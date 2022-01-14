using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsVideo : ContentPage
    {

        public static int VideoId;
        public OptionsVideo()
        {
            InitializeComponent();
            
            btnFirst.Clicked += BtnFirst_Clicked;
            btnSecond.Clicked += BtnSecond_Clicked;
            btnThird.Clicked += BtnThird_Clicked;
            btnFourth.Clicked += BtnFourth_Clicked;
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);

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