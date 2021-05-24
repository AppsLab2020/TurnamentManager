using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Android.Content.PM;
using Android.Support.V7.App;
using Felipecsl.GifImageViewLibrary;

namespace TurnamentManager.Droid
{
    [Activity(Theme  = "@style/splashTheme", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class SplashScreen : AppCompatActivity
    {
        private GifImageView gifImageView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashScreen);
            
            gifImageView = FindViewById<GifImageView>(Resource.Id.gifImageView);

            Stream input = Assets.Open("trophy_gif.gif");
            byte[] bytes = ConvertFileToByteArray(input);
            gifImageView.SetBytes(bytes);
            gifImageView.StartAnimation();

            System.Timers.Timer timer = new  System.Timers.Timer();
            timer.Interval = 5000;
            timer.AutoReset = false;
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }

        private byte[] ConvertFileToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);

                return ms.ToArray();
            }
        }
    }
}