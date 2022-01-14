
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace spel_123_piano
{
    public partial class MainPage : ContentPage
    {
        //public variables

        public double Distance { get; set; } = 500; // 5000m / 5km
        public bool GameOver { get; set; } = false;


        // local variables
        List<Xamarin.Forms.Shapes.Rectangle> wayMarks = new List<Xamarin.Forms.Shapes.Rectangle>();
        private bool started;
        private double width;
        private double height;
        private double gap;
        private DisplayInfo mainDisplayInfo;
        private double globalSpeed = 1000;
        Random random = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
        
        private DateTime startOrange = DateTime.MinValue;
        private DateTime startRed = DateTime.MinValue;

        public bool IsRed { get; private set; }

        public MainPage()
        {
            InitializeComponent();




            //int width = 0;
            //int height = 0;
            //for (int i = 0; i <= 10; i++)
            //{
            //    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            //    width = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.20);
            //    height = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.50);
            //    int gap = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) *0.70);
            //    Xamarin.Forms.Shapes.Rectangle baseRect = new Xamarin.Forms.Shapes.Rectangle();

            //    var re = new Rectangle(0, 70 * i, 20, 50);
            //    baseRect.LayoutTo(new Rectangle(0, gap * i, width, height));
            //    baseRect.Fill = Brush.White;
            //    AbsoluteLayout.SetLayoutFlags(baseRect, AbsoluteLayoutFlags.None);
            //    wayMarks.Add(baseRect);
            //    cnv.Children.Add(baseRect);
            //}
            
            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), Streetmove);
            
            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), GamePlay);
        }

        

        private bool GamePlay()
        {
            globalSpeed = 30;// read sensor here


            // lights
            if (startRed == DateTime.MinValue && startOrange == DateTime.MinValue) { 
                int luck = random.Next(0, 80);
                if (luck == 1)
                {
                circGreen.Opacity = 0.5;
                circOrange.Opacity = 1;
                startOrange = DateTime.Now;
                    //circGreen.Opacity = 0.5;
                    //circOrange.Opacity = 1;
                    //Thread.Sleep(1000);
                    //circOrange.Opacity = 0.5;
                    //circRed.Opacity = 1;
                    //IsRed = true;
                    //Thread.Sleep(3000);
                    //circGreen.Opacity = 1;
                    //isOrange = true;
                    //IsRed = false;
                
                Debug.WriteLine("orange");

            }
        }

            if(startOrange.AddSeconds(2) < DateTime.Now && startOrange!=DateTime.MinValue)
            {
                startOrange = DateTime.MinValue;
                startRed = DateTime.Now;
                circOrange.Opacity = 0.5;
                circRed.Opacity = 1;
                IsRed = true;
                Debug.WriteLine("rood");
            }

            if (startRed.AddSeconds(8) < DateTime.Now && startRed != DateTime.MinValue)
            {
                startRed = DateTime.MinValue;
                circRed.Opacity = 0.5;
                circGreen.Opacity = 1;
                IsRed = false;
                Debug.WriteLine("groen");
            }

            //game over went speed more than 1km/u

            //if (IsRed == true && globalSpeed > 1)
            //{
            //    lblGameOver.Text = "GAME OVER";
            //    lblGameOver.TextColor = Brush.Red.Color;

            
            //    lblGameOver.IsVisible = true;
            //    btnRestart.IsVisible = true;


            //    return false;

            //}

            Distance -= globalSpeed * 0.0277777778; // meter
            lblscore.Text = $"{Math.Round(Distance, 4).ToString()} m";

            if (Distance < 0)
            {
                lblGameOver.Text = "YOU WIN";
                lblGameOver.TextColor = Brush.Green.Color;

                lblscore.Text = "0";

                lblGameOver.IsVisible = true;
                btnRestart.IsVisible = true;


                return false;
            }



            return true;
        }

        private bool Streetmove()
        {
            if (!lblGameOver.IsVisible)
            {
                mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                DeviceDisplay.MainDisplayInfoChanged += ((l, m) =>
                {
                    Debug.WriteLine("Hellloooow wolfffiieeieieieiei");
                    if (mainDisplayInfo.Height != DeviceDisplay.MainDisplayInfo.Height)
                    {
                        loadingIndicator.IsRunning = true;
                        setup();
                        Thread.Sleep(3000);
                        setup();
                        loadingIndicator.IsRunning = false;
                    }



                });

                if (!started)
                {
                    started = true;
                    loadingIndicator.IsRunning = true;
                    setup();
                    loadingIndicator.IsRunning = false;
                    //Device.BeginInvokeOnMainThread(() =>
                    //{
                    //for (int i = 0; i < 7; i++)
                    //{


                    //    Xamarin.Forms.Shapes.Rectangle baseRect = new Xamarin.Forms.Shapes.Rectangle();
                    //    baseRect.Fill = Brush.White;




                    //    width= Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.05);
                    //    height = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.10);
                    //    gap = height * 1.5;

                    //    var re = new Rectangle(0, 110 * i, 20, 50);
                    //    baseRect.LayoutTo(new Rectangle(-width/2, gap * i, width, height));

                    //    AbsoluteLayout.SetLayoutFlags(baseRect, AbsoluteLayoutFlags.None);
                    //    wayMarks.Add(baseRect);
                    //    cnv.Children.Add(baseRect);

                    //}
                    //});
                }
                double speed = globalSpeed;
                if (!(speed <= 0))
                {
                    foreach (Xamarin.Forms.Shapes.Rectangle rectangle in wayMarks)
                    {

                        
                        //rectangle.Layout(new Rectangle(rectangle.X, rectangle.Y+70, 10, 10));

                        if (rectangle.Y > gap * 6)
                        {
                            if (Distance < 30 && Distance > 27)
                            {
                                rectangle.Layout(new Rectangle(-width * 0.3 / 2, -height + gap / (200 / speed), width*6 , 20));
                            }
                            else if(Distance > 35)
                            {
                                rectangle.Layout(new Rectangle(-width * 0.3 / 2, -height + gap / (200 / speed), width * 0.2, height));
                            }
                            
                        }
                        else
                        {

                            rectangle.Layout(new Rectangle(-rectangle.Width / 2, rectangle.Y + gap / (200 / speed), rectangle.Width + rectangle.Width * (0.001 / (1.0 / speed)), rectangle.Height - rectangle.Height * (0.00025 / (1.0 / speed))));
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void setup()
        {

            
            mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            wayMarks.Clear();
            cnv.Children.Clear();
            Device.BeginInvokeOnMainThread(() =>
            {
                loadingIndicator.IsRunning = true;
                for (int i = 0; i < 7; i++)
                {


                    Xamarin.Forms.Shapes.Rectangle baseRect = new Xamarin.Forms.Shapes.Rectangle();
                    baseRect.Fill = Brush.White;



                    if (mainDisplayInfo.Height > mainDisplayInfo.Width)
                    {
                        width = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.05);
                        height = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.20);
                        gap = height * 1.2;
                    }
                    else
                    {
                        width = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.05);
                        height = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.10);
                        gap = height * 1.2;
                    }
                    double width1 = width*0.3;
                    double height1 = height;
                    for (int e = 0; e <= i; e++)
                    {
                        width1 = width1 + width1 * 0.03;
                        height1 = height1 - height1 * 0.005;
                    }
                    var re = new Rectangle(0, 110 * i, 20, 50);
                    baseRect.LayoutTo(new Rectangle(-width1 / 2, gap * i, width1, height1));

                    AbsoluteLayout.SetLayoutFlags(baseRect, AbsoluteLayoutFlags.None);
                    wayMarks.Add(baseRect);
                    cnv.Children.Add(baseRect);

                }
                loadingIndicator.IsRunning = false;
            });
            
        }

        private void btnRestart_Clicked(object sender, EventArgs e)
        {
            setup();

            lblGameOver.IsVisible = false;
            btnRestart.IsVisible = false;
            Distance = 500;
            
            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), Streetmove);

            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), GamePlay);
        }
    }
}
