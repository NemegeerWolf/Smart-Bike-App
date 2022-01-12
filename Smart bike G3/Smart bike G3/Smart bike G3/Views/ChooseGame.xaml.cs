using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/****
 * Description:
 * In this view, you can choose a game you like.
 * There are 3 different options:
 * - 123 piano
 * - HillClimb
 * - Overloop
 * 
 */

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseGame : ContentPage
    {
        public ChooseGame()
        {
            InitializeComponent();
            imgBackButton.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.arrow_back.png");
            img123piano.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.traffic_light.png");
            imgHillClimb.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.two_wheeler.png");
            imgOverloop.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.water_drop.png");
        }

        private void imgBackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btn123piano_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new _123Piano());
        }

        private void btnHillClimb_Clicked(object sender, EventArgs e)
        {

        }

        private void btnOverloop_Clicked(object sender, EventArgs e)
        {

        }
    }
}