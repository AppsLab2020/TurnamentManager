using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Enums;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrawPage : ContentPage
    {
        public DrawPage()
        {
            InitializeComponent();
            var st = new StackLayout { };
            var frame = new Frame
            {
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HasShadow = true,
                IsClippedToBounds = true,
                Padding = 0,
                HeightRequest = 450,
                WidthRequest = 300,
                BackgroundColor = Color.Black,
                
            };


            var button1 = new Button
            {
                Text = "Random",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 5,
                BorderWidth = 2,
                BackgroundColor = Color.White
            };
            
            var button2 = new Button
            {
                Text = "Manual",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 5,
                BorderWidth = 2,
                BackgroundColor = Color.White
            };
            
            st.Children.Add(button1);
            st.Children.Add(button2);
            frame.Content = st;
            Layout.Children.Add(frame);
        }

        
    }
}