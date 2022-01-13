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
    public partial class OptionsVideo : ContentPage
    {
        public string Kind { get; set; }
        public OptionsVideo(string kind)
        {
            InitializeComponent();
            Kind = kind;
            btnFirst.Clicked += BtnFirst_Clicked;
            btnSecond.Clicked += BtnSecond_Clicked;
            btnThird.Clicked += BtnThird_Clicked;
            btnFourth.Clicked += BtnFourth_Clicked;
        }

        private void BtnFourth_Clicked(object sender, EventArgs e)
        {
            string kind = Kind;
            Navigation.PushAsync(new ChooseVideo(kind));
        }

        private void BtnThird_Clicked(object sender, EventArgs e)
        {
            string kind = Kind;
            Navigation.PushAsync(new ChooseVideo(kind));
        }

        private void BtnSecond_Clicked(object sender, EventArgs e)
        {
            string kind = Kind;
            Navigation.PushAsync(new ChooseVideo(kind));
        }

        private void BtnFirst_Clicked(object sender, EventArgs e)
        {
            string kind = Kind;
            Navigation.PushAsync(new ChooseVideo(kind));
        }
    }
}