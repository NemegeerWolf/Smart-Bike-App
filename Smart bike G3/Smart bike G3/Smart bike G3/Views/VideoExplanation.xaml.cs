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
    public partial class VideoExplanation : ContentPage
    {
        public VideoExplanation()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                NavigationPage.SetHasNavigationBar(this, false);
                btnStart.Clicked += BtnStart_Clicked;
                AddEvents();
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }


        private void AddEvents()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VideoPage());
        }
    }
}