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

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseGame : ContentPage
    {
        public ChooseGame()
        {
            InitializeComponent();
        }
    }
}