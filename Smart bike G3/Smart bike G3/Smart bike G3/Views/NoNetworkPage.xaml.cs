using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-Regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoNetworkPage : ContentPage
    {
        public NoNetworkPage()
        {
            InitializeComponent();

            //prevent sleepmode
           
            DeviceDisplay.KeepScreenOn = false;


            //imgNoInternet.Source = ImageSource.FromResource(@"Smart_Bike_G3.Assets.no_wifi.png");

            Connectivity.ConnectivityChanged += btnTryAgain_Clicked;
        }

        private void btnTryAgain_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked try again");
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                Navigation.PushAsync(new VideoOrGame());
                Console.WriteLine("Yay, internet!");
            }
            else
            {
                Console.WriteLine("Still no internet!");
            }
        }
    }
}
