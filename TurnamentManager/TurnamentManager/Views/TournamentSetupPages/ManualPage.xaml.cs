using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views.TournamentSetupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualPage : ContentPage
    {
        public ManualPage()
        {
            InitializeComponent();

            var addButton1 = new ImageButton
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = "add_button.png"
            };
              var vsImage = new Image
              {
                  HeightRequest = 65,
                  BackgroundColor = Color.Transparent,
                  HorizontalOptions = LayoutOptions.Start,
                  VerticalOptions = LayoutOptions.CenterAndExpand,
                  Source = "vs_image.png"

              };
              var addButton2 = new ImageButton
              {  HeightRequest = 50,
                  BackgroundColor = Color.Transparent,
                  HorizontalOptions = LayoutOptions.Start,
                  VerticalOptions = LayoutOptions.CenterAndExpand,
                  Source = "add_button.png"
              };
            var frame = new Frame
            {
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HasShadow = true,
                IsClippedToBounds = true,
                Margin = 15,

            };
            var st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                WidthRequest = 300,
                HeightRequest = 80,
            };
            
            st.Children.Add(addButton1);
            st.Children.Add(vsImage);
            st.Children.Add(addButton2);
            frame.Content = st;
            Layout.Children.Add(frame);
        }
    }
}