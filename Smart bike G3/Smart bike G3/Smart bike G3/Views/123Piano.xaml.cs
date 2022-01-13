using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Kind;
        public _123Piano(string kind)
        {
            InitializeComponent();
            Kind = kind;
            Console.WriteLine("kind 123piano = " + kind);
            VideoOrGame vidOrGame = new VideoOrGame();
            string varVidOrGame = vidOrGame.Kind;
            Console.WriteLine(varVidOrGame);
            AddEvents();
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

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            //start the game
            Console.WriteLine("Start 123 piano");
            Navigation.PushAsync(new Spel123Piano());

            //Kind meegeven tot aan het scorebord!
            //Navigation.PushAsync(new Spel123Piano(Kind));
            
        }
    }
}
