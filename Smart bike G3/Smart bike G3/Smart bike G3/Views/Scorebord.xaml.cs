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
                Pictures();
                //lblName.Text = Name.User;
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
                btnAdd.Clicked += BtnAdd_Clicked;
                
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            
            string user = entName.Text;

            if (user != null)
            {
                var i = await Repository.GetLastUserAsync();
                //string id = null;
                //foreach (var item in i)
                //{
                //    id = item.id;
                //}
                await Repository.UpdateName(user, i.id);
                btnAdd.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Check.png");
                entName.Text = "";
            }
            if (user == null)
            {
                entName.Placeholder = "vul jouw naam in";
            }
        }

        private void Pictures()
        {
            btnAdd.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Plus.png");
            btnHome.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Home.png");
            btnOpnieuw.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Again.png");
            ImgLeft.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundScore2.png");
            ImgRight.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundScore1.png");
        }

        private async void LoadData(int score,string kind)
        {
            await SetRank(score, VideoOrGame.Kind);
            //if (kind == "video")
            //{ 
            //    int videoid = ChooseVideo.VideoId;
            //    List<Video> i = await Repository.GetAllscoresVideoAsync(videoid);
            //    lvwOverview.ItemsSource = i.Count >= 3 ? i.GetRange(0, 3) : i;
            //    lblScore.Text = $"{score.ToString()} m";

            //}
            //else
            if (kind == "game")
            {
                int gameid = ChooseGame.gameId;
                List<Game> i = await Repository.GetAllscoresGameAsync(gameid);
                lvwOverview.ItemsSource = i.Skip(1);
                    //.Count >= 3 ? i.GetRange(1, 4) : i;
                var first = i.First();
                lblNameFirst.Text = first.User;
                lblRankFirst.Text = first.Rank;
                lblScoreFirst.Text = first.ScoreBordString;
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

            var i = await Repository.GetLastUserAsync();
            
            //string id = null;
            //foreach (var item in i)
            //{
            //    id = item.id;
            //}

            int rank = await Repository.CheckRank(i.id, score, kind);

            


            lblPosition.Text = $"{rank}";
        }


        private async void BtnOpnieuw_Clicked(object sender, EventArgs e)
        {
            if (VideoOrGame.Kind == "game")
            {
                var i = await Repository.GetLastUserAsync();
                if (entName.Text == null)
                {
                    await Repository.DeleteAsync(i.id);
                }
                Navigation.PushAsync(new ChooseGame());
            }
            else
            {
                var i = await Repository.GetLastUserAsync();
                if (entName.Text == null)
                {
                    await Repository.DeleteAsync(i.id);
                }
                Navigation.PushAsync(new ChooseVideo());
            }
        }

        private async void BtnHome_Clicked(object sender, EventArgs e)
        {
            var i = await Repository.GetLastUserAsync();
            if (entName.Text == null)
            {
                await Repository.DeleteAsync(i.id);
            }
            Navigation.PushAsync(new VideoOrGame());
        }
    }
}