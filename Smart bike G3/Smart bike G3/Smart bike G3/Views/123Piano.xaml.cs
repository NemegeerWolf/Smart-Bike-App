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