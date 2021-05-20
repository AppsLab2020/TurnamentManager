using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class MatchModel
    {

        private ICommand TapCommand;

        private int[] currentCoords;
        
        
        
        public MatchModel(INavigation navigation, int tournamentId)
        {
            currentCoords = new[] {0, 0};
        }

        public ScrollView GetSpider()
        {
            var layout = new AbsoluteLayout
            {
                HeightRequest = 5000,
                WidthRequest = 5000,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Red
            };
            
            layout.Children.Add(GetFrame("test", "test"), new Point(15,15));
            
            var scroll = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                Content = new ScrollView
                {
                    Orientation = ScrollOrientation.Horizontal,
                    Content = layout,
                }
            };

            return scroll;
        }

        private Frame GetFrame(string leftName, string rightName)
        {
            var addButton1 = new Label
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = leftName,
            };
            var vsImage = new Image
            {
                HeightRequest = 65,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = "vs_image.png"
            };
            var addButton2 = new Label
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = rightName,
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
            
            var tap = new TapGestureRecognizer { Command = TapCommand};

            st.Children.Add(addButton1);
            st.Children.Add(vsImage);
            st.Children.Add(addButton2);
            frame.Content = st;

            return frame;
        }
    }
}