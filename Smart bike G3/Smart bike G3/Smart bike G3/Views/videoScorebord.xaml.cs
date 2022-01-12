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
    public partial class videoScorebord : ContentPage
    {
        public videoScorebord()
        {
            InitializeComponent();
            loadData();
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

        private async void loadData()
        {
            lvwOverview.ItemsSource = await Repository.GetAllscoresVideoAsync(1);

        }
    }
}