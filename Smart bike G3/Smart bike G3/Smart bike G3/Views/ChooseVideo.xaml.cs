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
        public ChooseVideo()
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
            tapGestureRecognizer2.Tapped += AbsLayMusic_Tabbed;
            AbsLayMusic.GestureRecognizers.Add(tapGestureRecognizer2);

            TapGestureRecognizer tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += AbsLayBook_Tabbed;
            AbsLayBook.GestureRecognizers.Add(tapGestureRecognizer3);
        }

        private void AbsLayBook_Tabbed(object sender, EventArgs e)
        {
            //Console.WriteLine("tabbed book");
        }

        private void AbsLayMusic_Tabbed(object sender, EventArgs e)
        {
            //Console.WriteLine("tabbed music");
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}