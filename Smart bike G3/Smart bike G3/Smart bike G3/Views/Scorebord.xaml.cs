using Smart_bike_G3.Repositories;
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
    public partial class Scorebord : ContentPage
    {
        
        //public Scorebord()
        //{
        //    InitializeComponent();
        //    loadData();
        //    btnHome.Clicked += BtnHome_Clicked;
        //    btnOpnieuw.Clicked += BtnOpnieuw_Clicked;
        //}

        public Scorebord(string kind)
        {
            InitializeComponent();
            VideoOrGame vidOrGame = new VideoOrGame();
            string varVidOrGame = vidOrGame.Kind;
            Console.WriteLine(varVidOrGame);

            Console.WriteLine(kind);
            if (kind == "video")
            {
                //Er werd een video afgespeeld
                loadData(kind);
            } else if (kind == "game")
            {
                //Er werd een game gespeeld
                loadData(kind);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

            btnHome.Clicked += BtnHome_Clicked;
            btnOpnieuw.Clicked += BtnOpnieuw_Clicked;
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
                lvwOverview.ItemsSource = await Repository.GetAllscoresVideoAsync(1);
            } else if (kind == "game")
            {
                lvwOverview.ItemsSource = await Repository.GetAllscoresGameAsync(1);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
           

        }
    }
}