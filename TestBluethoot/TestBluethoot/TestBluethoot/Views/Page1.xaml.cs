using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBluethoot.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestBluethoot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            Sensor.NewDataCadence += changelbl;
        }

        private void changelbl(object sender, int e)
        {
            lbl.Text = e.ToString();
        }
    }
}