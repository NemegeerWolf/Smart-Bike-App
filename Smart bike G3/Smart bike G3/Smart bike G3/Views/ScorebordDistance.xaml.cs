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
    public partial class ScorebordDistance : ContentPage
    {
        public ScorebordDistance(int score)
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
                var i = await Repository.GetAllscoresVideoAsync(videoid);
                if (i.Count >= 3) { lvwOverview.ItemsSource = i.GetRange(0, 3); }
                else { lvwOverview.ItemsSource = i; }
            }
            else if (kind == "game")
            {
                int gameid = ChooseGame.gameId;
                var i = await Repository.GetAllscoresGameAsync(gameid);

                if (i.Count >= 3) { lvwOverview.ItemsSource = i.GetRange(0, 3); }
                else { lvwOverview.ItemsSource = i; }
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }
    }
}