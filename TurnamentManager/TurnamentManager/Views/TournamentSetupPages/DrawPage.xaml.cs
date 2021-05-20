using System;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrawPage : ContentPage
    {
        private DrawModel _model;
        private int _tournamentID;

        public DrawPage()
        {

        }
        public DrawPage(int tournamentID)
        {
            _tournamentID = tournamentID;
            InitializeComponent();
            
            _model = new DrawModel(Navigation, tournamentID);
            BindingContext = _model;
            
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

            st.Children.Add(RandomButton);
            st.Children.Add(ManualButton);
            frame.Content = st;
            Layout.Children.Add(frame);
        }

        private void SaveButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MatchPage(_tournamentID));
        }
    }
}