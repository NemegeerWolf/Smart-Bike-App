using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsVideo : ContentPage
    {

        public ResultsVideo(double distance, double avg, string dur)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                NavigationPage.SetHasNavigationBar(this, false);
                ShowResults(distance,avg,dur);
                btnHome.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Home.png");
                ImgLeft.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundScore2.png");
                ImgRight.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.BackgroundScore1.png");
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        private void ShowResults(double distance, double avg, string dur)
        {

            lblDistance.Text = $"{distance}m";
            lblAverageSpeed.Text = $"{avg} km/u";
            lblTime.Text = dur;
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VideoOrGame());
        }
    }
}
