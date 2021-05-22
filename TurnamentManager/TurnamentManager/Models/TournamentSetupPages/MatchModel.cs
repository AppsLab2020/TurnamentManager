using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class MatchModel
    {

        private ICommand TapCommand;

        private const int _frameWidth = 150;
        private const int _frameHeight = 40;

        private const int _xShift = 80;
        private const int _yShift = 80;

        private readonly int _tournamentId;

        private List<string> _matchesList;

        public MatchModel(INavigation navigation, int tournamentId)
        {
            _matchesList = new List<string>();
            _tournamentId = tournamentId;
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

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == _tournamentId))
            {
                _matchesList = tournament.MatchesString.Split('\n').ToList();
            }

            _matchesList.RemoveAt(_matchesList.Count - 1);

            for (var i = 0; i < _matchesList.Count; i++ )
            {
                var match = _matchesList[i];
                var names = match.Split(' ');
                
                layout.Children.Add(GetFrame(names[0], names[2]), new Point(15, 15 + (_yShift * i)));
            }

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
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = leftName,
                FontFamily = "PixL",
                Padding = new Thickness(0,12),
                
            };
            var vsImage = new Image
            {
                Source = "vs_image.png",
                HeightRequest = 30,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            var addButton2 = new Label
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = rightName,
                FontFamily = "PixL",
                Padding = new Thickness(0,12),
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
                WidthRequest = _frameWidth,
                HeightRequest = _frameHeight,
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