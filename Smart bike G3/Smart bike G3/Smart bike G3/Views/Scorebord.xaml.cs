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
         int parscore;
         string vidorgame;

        public Scorebord(int score)
        {
            parscore = score;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();

                //prevent sleepmode
                App app = new App();
                app.ToggleScreenLock(true);

                Pictures();
                //lblName.Text = Name.User;
                vidorgame = VideoOrGame.Kind;

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

                //Device.StartTimer(TimeSpan.FromMinutes(1), () =>
                //{
                //    Console.WriteLine("Return to startpage");
                //    Navigation.PushAsync(new VideoOrGame());
                //    return false;
                //});
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
                // var i = await Repository.GetLastUserAsync();
                //string id = null;
                //foreach (var item in i)
                //{
                //    id = item.id;
                //}
                warning(user);
                // await Repository.UpdateName(user, i.id);
                btnAdd.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Check.png");
                entName.Text = "";
                btnAdd.IsEnabled = false;
                entName.IsVisible = false;
                lblName.Text = user;
                lblName.IsVisible = true;

            }
            if (user == null)
            {
                entName.Placeholder = "vul eerst jouw bijnaam in";
            }
        }

        private async void warning(string user)
        {
            var i = await Repository.GetLastUserAsync();
            bool check = await Repository.CheckUsernameAsync(user);
            if (check == true)
            {
                bool answer = await DisplayAlert("Deze naam is al in gebruik.", $"Ben jij {user}?", "Ja", "Nee");
                if (answer == true)
                {
                    await Repository.UpdateName(user, i.id);
                    LoadData(parscore, vidorgame);
                }
                else
                {
                    await DisplayAlert("Kies een andere naam.", "", "OK");
                }
            }
            else
            {
                await Repository.UpdateName(user, i.id);
                LoadData(parscore, vidorgame);
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
                
                bool isEmpty = !i.Any();
                if (isEmpty == false)
                {
                    lvwOverview.ItemsSource = i.Skip(1);
                    //.Count >= 3 ? i.GetRange(1, 4) : i;
                    var first = i.First();
                    lblNameFirst.Text = first.User;
                    lblRankFirst.Text = first.Rank;
                    lblScoreFirst.Text = first.ScoreBordString;
                    lblScore.Text = $"{score.ToString()} s";
                    //if (gameid != null)
                    //{
                        
                    //}
                    //else
                    //{
                    //    lblScore.Text = $"{score.ToString()} m";
                    //}
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
                if (entName.Text == null && i.User == null)
                {
                    await Repository.DeleteAsync(i.id);
                }
                //Navigation.PopToRootAsync(true);
                //Navigation.PopAsync();
                if (ChooseGame.gameId == 1)
                {
                    Navigation.PushAsync(new Spel123Piano());
                }
                if (ChooseGame.gameId == 2)
                {
                    Navigation.PushAsync(new BalanceGame());
                }
                if (ChooseGame.gameId == 3)
                {
                    Navigation.PushAsync(new SpelOverloop());
                }
                else
                {
                    Navigation.PopToRootAsync(true);
                }
                //Navigation.PushAsync(new ChooseGame());
            }
            else
            {
                var i = await Repository.GetLastUserAsync();
                if (entName.Text == null && i.User == null)
                {
                    await Repository.DeleteAsync(i.id);
                }
                Navigation.PushAsync(new ChooseVideo());
            }
        }

        private async void BtnHome_Clicked(object sender, EventArgs e)
        {
            var i = await Repository.GetLastUserAsync();
            if (entName.Text == null && i.User == null)
            {
                await Repository.DeleteAsync(i.id);
            }
            //Navigation.PopToRootAsync(true);
            Navigation.PushAsync(new VideoOrGame());
        }
    }
}