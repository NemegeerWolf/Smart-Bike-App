using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class videoScorebord : ContentPage
    {
        public videoScorebord(int score)
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                lblScore.Text = score.ToString() + " km";
                lblName.Text = Name.User;
                string vidorgame = VideoOrGame.Kind;
                Console.WriteLine(vidorgame);

                if (vidorgame == "video")
                {
                    //Er werd een video afgespeeld
                    loadData(vidorgame);
                }
                else if (vidorgame == "game")
                {
                    //Er werd een game gespeeld
                    loadData(vidorgame);
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }

                btnHome.Clicked += BtnHome_Clicked;
                btnOpnieuw.Clicked += BtnOpnieuw_Clicked;
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        private void BtnOpnieuw_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChooseGame());
        }

        private void BtnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Name());
        }

        private async void loadData(string kind)
        {
            if (kind == "video")
            {
                
                int videoid = OptionsVideo.VideoId;
                lvwOverview.ItemsSource = await Repository.GetAllscoresVideoAsync(videoid);
            }
            else if (kind == "game")
            {
                var i = await Repository.GetAllscoresGameAsync(1);

                lvwOverview.ItemsSource = i.GetRange(0, 3);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }
    }
}