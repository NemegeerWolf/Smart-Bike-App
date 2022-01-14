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

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-Regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseGame : ContentPage
    {

        public ChooseGame()
        {
            InitializeComponent();
            AddEvents();
        }
        public ChooseGame(string kind)
        {
            InitializeComponent();
            AddEvents();
        }

        private void AddEvents()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);
            

            TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += AbsLay123piano_Tabbed;
            AbsLay123piano.GestureRecognizers.Add(tapGestureRecognizer2);

            TapGestureRecognizer tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += AbsLayHillClimb_Tabbed;
            AbsLayHillClimb.GestureRecognizers.Add(tapGestureRecognizer3);

            TapGestureRecognizer tapGestureRecognizer4 = new TapGestureRecognizer();
            tapGestureRecognizer4.Tapped += AbsLayOverloop_Tabbed;
            AbsLayOverloop.GestureRecognizers.Add(tapGestureRecognizer4);
        }

        private void AbsLayOverloop_Tabbed(object sender, EventArgs e)
        {
            AbsLayOverloop.Scale = 8;
            Navigation.PushAsync(new Overloop());
        }

        private void AbsLayHillClimb_Tabbed(object sender, EventArgs e)
        {
            AbsLayHillClimb.Scale = 8;
            Console.WriteLine("Tabbed hill climb");
        }

        private void AbsLay123piano_Tabbed(object sender, EventArgs e)
        {
            AbsLay123piano.Scale = 8;
            Navigation.PushAsync(new _123Piano());
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            AbsLayBack.Scale = 1.5;
            Navigation.PopAsync();
        }
    }
}
