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
                BackgroundColor = Color.FromHex("#D7812A"),
                
            };


            /*var randomButton = new ImageButton
            {
                Source = random_button.png,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                
            };*/
            
           /* var button2 = new Button
            {
                Text = "Manual",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 5,
                BorderWidth = 2,
                BackgroundColor = Color.White,
                TextColor = Color.Black
            };*/
            
            st.Children.Add(RandomButton);
            st.Children.Add(ManualButton);
            frame.Content = st;
            Layout.Children.Add(frame);
        }


        private void SaveButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MatchPage());
        }
    }
}