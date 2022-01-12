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
    public partial class _123Piano : ContentPage
    {
        public _123Piano()
        {
            InitializeComponent();
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
        }
    }
}