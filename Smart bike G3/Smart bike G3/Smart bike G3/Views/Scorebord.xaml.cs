using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
                DeviceDisplay.KeepScreenOn = false;

                Pictures();

                vidorgame = VideoOrGame.Kind;

                Console.WriteLine(vidorgame);

                if (vidorgame == "video" || vidorgame == "game")
                {
                    LoadData(score, vidorgame);
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

                warning(user);

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

        private async void LoadData(int score, string kind)
        {
            await SetRank(score, VideoOrGame.Kind);

            if (kind == "game")
            {
                int gameid = ChooseGame.gameId;
                List<Game> i = await Repository.GetAllscoresGameAsync(gameid);

                bool isEmpty = !i.Any();
                if (isEmpty == false)
                {
                    lvwOverview.ItemsSource = i.Skip(1);

                    var first = i.First();
                    lblNameFirst.Text = first.User;
                    lblRankFirst.Text = first.Rank;
                    lblScoreFirst.Text = first.ScoreBordString;




                    if (score >= 60)
                    {
                        double minuten = score / 60;
                        int minuten1 = (int)Math.Truncate(minuten);
                        int seconden = score - (minuten1 * 60);
                        lblScore.Text = $"{minuten1} min {seconden} s";


                    }
                    else
                    {
                        lblScore.Text = $"{score} s";
                    }
                    



                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }
            }
            }

            private async Task SetRank(int score, string kind)
            {

                var i = await Repository.GetLastUserAsync();



                int rank = await Repository.CheckRank(i.id, score);
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

                    if (ChooseGame.gameId == 1)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                        Navigation.PushAsync(new Spel123Piano());
                    }
                    else if (ChooseGame.gameId == 2)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);



                        await Navigation.PushAsync(new BalanceGame());
                        //return;




                    }
                    else if (ChooseGame.gameId == 3)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                        Navigation.PushAsync(new SpelOverloop());
                    }
                    else
                    {
                        Navigation.PopToRootAsync(true);
                    }

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
                //await Navigation.PopToRootAsync(true);

                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                Navigation.PopAsync();
                //Navigation.PushAsync(new VideoOrGame());
            }
        }
    }