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
            Images();
        }
        private void Images()
        {
            imgLuisterboek.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Luisterboek.png");
            imgMuziek.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Muziek.png");
        }
    }
}