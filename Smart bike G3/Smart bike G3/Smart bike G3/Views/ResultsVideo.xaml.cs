using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ResultsVideo()
        {
            InitializeComponent();
            btnScorebord.Clicked += BtnScorebord_Clicked;
            showKilometers();
        }

        private async void showKilometers()
        {

            int kilometers = 12;
            string kilometerString = $"{kilometers}km";
            lblKilometers.Text = kilometerString;

            // await Repository.AddResultsVideo(1, "test", kilometers);
        }

        private void BtnScorebord_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Scorebord());
        }

    }
}
