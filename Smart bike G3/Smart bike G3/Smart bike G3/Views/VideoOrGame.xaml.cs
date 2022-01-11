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
    public partial class VideoOrGame : ContentPage
    {
        public VideoOrGame()
        {
            InitializeComponent();
            btnGame.Clicked += BtnGame_Clicked;
            btnVideo.Clicked += BtnVideo_Clicked;
            Images();
        }

        private void Images()
        {
            imgVideo.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.video.png");
            imgGame.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Game.png");
        }

        private void BtnVideo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChooseVideo());
        }

        private void BtnGame_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}