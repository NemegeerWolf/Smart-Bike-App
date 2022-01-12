using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Name : ContentPage
    {
        public Name()
        {
            InitializeComponent();
            BtnNext.Clicked += BtnNext_Clicked;
        }

        private void BtnNext_Clicked(object sender, EventArgs e)
        {
            if (entName.Text != null)
            {
                Navigation.PushAsync(new VideoOrGame());
            }
            else
            {
                lblValidation.IsVisible = true;
            }
               
        }
    }
}