using System;
using System.Collections.Generic;
using System.Windows.Input;
using TurnamentManager.Views;
using TurnamentManager.Views.TournamentSetupPages;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class ManualModel
    {
        public Command NavigateToNextCommand { get; set; }

        public EventHandler FullMatchEventHandler;

        public INavigation Navigation;
        private ICommand _leftButtonCommand { get; set; }
        private ICommand _rigthButtonCommand { get; set; }

        private List<string> _usedPlayersList;

        private string _leftButtonImage;
        private string _rightButtonImage;

        private int _tournametId;

        public ManualModel(INavigation navigation, int tournamentId)
        {
            Navigation = navigation;

            NavigateToNextCommand = new Command(Navigate);

            _tournametId = tournamentId;

            _leftButtonCommand = new Command(LeftButtonCommand);
            _rigthButtonCommand = new Command(RightButtonCommand);

            _leftButtonImage = "add_button.png";
            _rightButtonImage = "add_button.png";

            _usedPlayersList = new List<string>();
        }

        public Frame GenereateFrame()
        {
            var addButton1 = new ImageButton
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = _leftButtonImage,
                Command = _leftButtonCommand
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
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = _rightButtonImage,
                Command = _rigthButtonCommand
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

            return frame;
        }


        public Frame GenereateFrame(string rightButtonImage, string leftButtonImage)
        {
            var addButton1 = new ImageButton
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = leftButtonImage,
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
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = rightButtonImage,
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

            return frame;
        }

        private void LeftButtonCommand()
        {

        }

        private void RightButtonCommand()
        {

        }

        private string GetPopoutAnswer()
        {
            return "";
        }

        private void Navigate()
        {
            Navigation.PushAsync(new MatchPage());
        }
    }
}