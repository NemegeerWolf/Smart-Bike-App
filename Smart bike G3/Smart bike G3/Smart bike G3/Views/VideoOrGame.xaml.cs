using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-Regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoOrGame : ContentPage
    {
        public static string Kind;
       
        public VideoOrGame()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                AddEvents();
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }

        }

        private void AddEvents()
        {
            //TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            //AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);

            TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += AbsLayVideo_Tabbed;
            AbsLayVideo.GestureRecognizers.Add(tapGestureRecognizer2);

            TapGestureRecognizer tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += AbsLayGame_Tabbed;
            AbsLayGame.GestureRecognizers.Add(tapGestureRecognizer3);

            TapGestureRecognizer tapGestureRecognizer4 = new TapGestureRecognizer();
            tapGestureRecognizer4.Tapped += AbsLayOtherUser_Tabbed;
            AbsLayOtherUser.GestureRecognizers.Add(tapGestureRecognizer4);
            lblOtherUser.GestureRecognizers.Add(tapGestureRecognizer4);
        }

        private void AbsLayOtherUser_Tabbed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Name());
        }

        private void AbsLayGame_Tabbed(object sender, EventArgs e)
        {
            
            Kind = "game";
            AbsLayGame.Scale = 8;
            Console.WriteLine(Kind + " chosen");
            Navigation.PushAsync(new ChooseGame());
        }

        private void AbsLayVideo_Tabbed(object sender, EventArgs e)
        {
            Kind = "video";
            AbsLayVideo.Scale = 8;
            Console.WriteLine(Kind + " chosen");
            Navigation.PushAsync(new OptionsVideo());
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
