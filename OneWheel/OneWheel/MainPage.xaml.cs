using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneWheel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            oneWheel.AnchorY = 0.85;
            Animate();
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), Animate);

        }

        readonly bool playing = true;
        private bool Animate()
        {
            Random rand = new Random();
            int test = rand.Next(1, 3);
            uint speed = Convert.ToUInt32(rand.Next(3000, 6000));
            int angle;
            if (test == 1)
            {
                angle = 90;
            }
            else
            {
                angle = -90;
            }

            Rotate(angle, speed);
            return true;
        }

        private async Task Rotate(int degrees, uint speed)
        {        
            await oneWheel.RotateTo(degrees, speed);
            if(Math.Round(oneWheel.Rotation) == 90 || Math.Round(oneWheel.Rotation) == -90)
            {
                Debug.WriteLine("dead");
            }
            Debug.WriteLine(Math.Round(oneWheel.Rotation));
        }
    }
}
