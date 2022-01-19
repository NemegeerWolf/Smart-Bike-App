using System;
using System.Collections.Generic;
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
    public partial class _123Piano : ContentPage
    {
        
        public _123Piano()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                string vidorgame = VideoOrGame.Kind;
                Console.WriteLine(vidorgame);
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
            AbsLayBack.Scale = 1.5;
            Navigation.PopAsync();
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            btnStart.Scale = 0.8;
            //start the game
            Console.WriteLine("Start 123 piano");
            Navigation.PushAsync(new Spel123Piano());            
        }
    }
}
