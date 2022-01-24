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


[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scorebord : ContentPage
    {
        public Scorebord(int score)
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                lblName.Text = Name.User;
                string vidorgame = VideoOrGame.Kind;

                Console.WriteLine(vidorgame);

                if (vidorgame == "video" || vidorgame == "game")
                {
                    LoadData(score,vidorgame);
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

        private async void LoadData(int score,string kind)
        {
            await SetRank(score, VideoOrGame.Kind);
            if (kind == "video")
            { 
                int videoid = OptionsVideo.VideoId;
                List<Video> i = await Repository.GetAllscoresVideoAsync(videoid);
                lvwOverview.ItemsSource = i.Count >= 3 ? i.GetRange(0, 3) : i;
                lblScore.Text = $"{score.ToString()} m";

            }
            else if (kind == "game")
            {
                int gameid = ChooseGame.gameId;
                List<Game> i = await Repository.GetAllscoresGameAsync(gameid);
                lvwOverview.ItemsSource = i.Count >= 3 ? i.GetRange(0, 3) : i;
                if (gameid != 3)
                {
                    lblScore.Text = $"{score.ToString()} s";
                }
                else
                {
                    lblScore.Text = $"{score.ToString()} m";
                }
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        private async Task SetRank(int score, string kind)
        {
            int rank = await Repository.CheckRank(Name.User, score, kind);
            lblPosition.Text = $"{rank}.";
        }


        private void BtnOpnieuw_Clicked(object sender, EventArgs e)
        {
            if (VideoOrGame.Kind == "game")
            {
                Navigation.PushAsync(new ChooseGame());
            }
            else
            {
                Navigation.PushAsync(new OptionsVideo());
            }
        }

        private void BtnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VideoOrGame());
        }
    }
}