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
            imgBackButton.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.arrow_back.png");

        }

        private void imgBackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}