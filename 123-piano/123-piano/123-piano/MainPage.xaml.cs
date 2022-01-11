using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace _123_piano
{
    public partial class MainPage : ContentPage
    {
        List<Xamarin.Forms.Shapes.Rectangle> wayMarks = new List<Xamarin.Forms.Shapes.Rectangle>();
        private bool started;
        private double width;
        private double height;
        private double gap;

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

            Device.StartTimer(TimeSpan.FromMilliseconds(100.0), straatmove);
        }

        private bool straatmove()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
           
            if (!started)
            {
                started = true;


                Device.BeginInvokeOnMainThread(() =>
                {
                for (int i = 0; i < 7; i++)
                {

                    
                    Xamarin.Forms.Shapes.Rectangle baseRect = new Xamarin.Forms.Shapes.Rectangle();
                    baseRect.Fill = Brush.White;




                    width= Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.05);
                    height = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.10);
                    gap = height * 1.5;
                    
                    var re = new Rectangle(0, 110 * i, 20, 50);
                    baseRect.LayoutTo(new Rectangle(-width/2, gap * i, width, height));
                    
                    AbsoluteLayout.SetLayoutFlags(baseRect, AbsoluteLayoutFlags.None);
                    wayMarks.Add(baseRect);
                    cnv.Children.Add(baseRect);
                    
                }
                });
            }
         
            foreach(Xamarin.Forms.Shapes.Rectangle rectangle in wayMarks)
            {
                
                
                //rectangle.Layout(new Rectangle(rectangle.X, rectangle.Y+70, 10, 10));

                if(rectangle.Y > gap * 6)
                {
                    rectangle.Layout(new Rectangle(-width * 0.3 / 2, -gap, width*0.3, height));
                }
                else
                {
                    rectangle.Layout(new Rectangle(-rectangle.Width/2, rectangle.Y + gap/10, rectangle.Width + rectangle.Width * 0.03, rectangle.Height - rectangle.Height * 0.01));
                }
            }
            return true;
        }
    }
}
