using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Path = System.IO.Path;

namespace TurnamentManager.Models
{
    public class MatchModel
    {
        private int stage = 0;

        private ICommand TapCommand;

        private const int _frameWidth = 150;
        private const int _frameHeight = 60;

        private const int _xShift = 90;
        private const int _yShift = 150;

        private const int _lineWidth = 60;

        private readonly int _tournamentId;

        private List<string> _matchesList;

        private List<List<Position>> _frames;

        public MatchModel(INavigation navigation, int tournamentId)
        {
            _matchesList = new List<string>();
            _tournamentId = tournamentId;
            _frames = new List<List<Position>>();
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

            var framesList = new List<Position>();

            for (var i = 0; i < _matchesList.Count; i++ )
            {
                var match = _matchesList[i];
                var names = match.Split(' ');
                
                var frame = GetFrame(names[0], names[2]);
                layout.Children.Add(frame, new Point(15, 15 + (_yShift * i)));
                var position = new Position()
                {
                    X = 15,
                    Y = 15 + (_yShift * i)
                };
                framesList.Add(position);
            }
            
            _frames.Add(framesList);

            var doneGenerating = false;

            while (!doneGenerating)
            {
                for (var i = 0; i < _frames[stage].Count; i++)
                {
                    var top = _frames[stage][i];
                    var bot = _frames[stage][i + 1];

                    foreach (var line in GenerateLine(new[] {top.X, top.Y}, new[] {bot.X, bot.Y}))
                    {
                        layout.Children.Add(line);
                    }

                    i++;
                }

                /*foreach () //for a pre kazde dva turnaje sa urobi jeden frame
                {
                    GetFrame();
                }

                if (all)
                    doneGenerating = true;*/

                doneGenerating = true;
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
                HasShadow = true,
                IsClippedToBounds = true,
                WidthRequest = _frameWidth,
                HeightRequest = _frameHeight,
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

        private List<Line> GenerateLine(double[] top, double[] bot)
        {
            var lines = new List<Line>();
            var topLine = new Line
            {
                X1 = top[0] + _frameWidth + 40,
                X2 = top[0] + _frameWidth + _lineWidth,
                Y1 = top[1] + (_frameHeight / 2) + 20,
                Y2 = top[1] + (_frameHeight / 2) + 20,
                Stroke = Brush.Aqua
            };
            
            var botLine = new Line
            {
                X1 = bot[0] + _frameWidth + 40,
                X2 = bot[0] + _frameWidth + _lineWidth,
                Y1 = bot[1] + (_frameHeight / 2) + 20,
                Y2 = bot[1] + (_frameHeight / 2) + 20,
                Stroke = Brush.Aqua
            };

            var connectingLine = new Line
            {
                X1 = top[0] + _frameWidth + _lineWidth,
                X2 = bot[0] + _frameWidth + _lineWidth,
                Y1 = top[1] + (_frameHeight / 2) + 20,
                Y2 = bot[1] + (_frameHeight / 2) + 20,
                Stroke = Brush.Aqua
            };

            var lenght = (top[1] + (_frameHeight / 2) + 20 + bot[1] + (_frameHeight / 2) + 20) / 2;

            var middleLine = new Line
            {
                X1 = top[0] + _frameWidth + _lineWidth,
                X2 = top[0] + _frameWidth + _lineWidth * 3,
                Y1 = lenght,
                Y2 = lenght,
                Stroke = Brush.Aqua
            };

            lines.Add(topLine);
            lines.Add(botLine);
            lines.Add(connectingLine);
            lines.Add(middleLine);

            return lines;
        }
    }
}