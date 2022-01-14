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
    public partial class NoNetworkPage : ContentPage
    {
        public NoNetworkPage()
        {
            InitializeComponent();
            imgNoInternet.Source = ImageSource.FromResource(@"Smart_Bike_G3.Assets.no_wifi.png");
        }

        private void btnTryAgain_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                Navigation.PushAsync(new Scorebord());
            }
            else
            {
                Console.WriteLine("Still no internet!");
            }
        }
    }
}