using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseVideo : ContentPage
    {

        public static string Listening;
        public ChooseVideo()
        {
            InitializeComponent();
            AddEvents();
            MakeResponsive();
        }

        private void MakeResponsive()
        {
            // aanpassen aan grote van scherm

            var width = Application.Current.MainPage.Width;
            var height = Application.Current.MainPage.Height;
            Console.WriteLine(width + "x" + height);
            
            //AbsLayBook.Scale =
        }

        private void AddEvents()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);

            TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += AbsLayMusic_Tabbed;
            AbsLayMusic.GestureRecognizers.Add(tapGestureRecognizer2);

            TapGestureRecognizer tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += AbsLayBook_Tabbed;
            AbsLayBook.GestureRecognizers.Add(tapGestureRecognizer3);
        }

        private void AbsLayBook_Tabbed(object sender, EventArgs e)
        {
            Listening = "luisterboek";
            Console.WriteLine("Tabbed luisterboek");
            Navigation.PushAsync(new VideoPage());
            //Kind meegeven met de game
            //wordt terug opgehaald in scorebord
        }

        

        private void AbsLayMusic_Tabbed(object sender, EventArgs e)
        {
            Listening = "muziek";
            Console.WriteLine("Tabbed music");
            Navigation.PushAsync(new VideoPage());
            //Kind meegeven met de game
            //wordt terug opgehaald in scorebord
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
