using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BalanceGame : ContentPage
    {
        public BalanceGame()
        {
            InitializeComponent();
            oneWheel.AnchorY = 0.85;
            Animate();
            Device.StartTimer(TimeSpan.FromMilliseconds(100), Animate);
            /*Sensor.NewDataSpeed += ((s, e) =>
            {
                Speed = e;
            });*/
        }
        double speed = 11;

        readonly bool playing = true;
        private bool Animate()
        {
            double targetSpeed = 10;
            //5sec 2sec
            double difference = Math.Abs(targetSpeed - speed);

            uint fallSpeed = Convert.ToUInt32((10 - difference) * 1000);
            int angle;
            if (speed < targetSpeed)
            {
                angle = 90;
            }
            else
            {
                angle = -90;
            }

            Rotate(angle, fallSpeed);
            return true;
        }

        private async Task Rotate(int degrees, uint speed)
        {
            await oneWheel.RotateTo(degrees, speed);
            if (Math.Round(oneWheel.Rotation) == 90 || Math.Round(oneWheel.Rotation) == -90)
            {
                Debug.WriteLine("dead");
            }
        }
    }
}