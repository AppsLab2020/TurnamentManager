using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using SQLite;
using TurnamentManager.Classes;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Views.PopOutPages;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Path = System.IO.Path;

namespace TurnamentManager.Models
{
    public class MatchModel
    {
        private Command TapCommand => new Command<string>(OpenPopup);

        private int _stage = 0;

        private const int _frameWidth = 150;
        private const int _frameHeight = 60;

        private const int _yShift = 150;

        private const int _lineWidth = 60;

        private readonly int _tournamentId;

        private List<string> _matchesList;

        private List<List<Position>> _frames;

        private INavigation _navigation;

        public MatchModel(INavigation navigation, int tournamentId)
        {
            _matchesList = new List<string>();
            _navigation = navigation;
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
            };

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == _tournamentId))
            {
                _matchesList = tournament.MatchesString.Split('\n').ToList();
            }

            _matchesList.RemoveAt(_matchesList.Count - 1);

            if (_matchesList.Count >= 2)
            {
                while (!IsNumberPowerOf2(_matchesList.Count))
                {
                    _matchesList.Add("test : test \n");
                }
                
                foreach (var tournament in tournaments.Where(tournament => tournament.ID == _tournamentId))
                {
                    foreach (var match in _matchesList)
                    {
                        tournament.ResultsStringList.Add("none : none \n");
                    }
                }
                
                var framesList = new List<Position>();

                for (var i = 0; i < _matchesList.Count; i++)
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
                    if (_frames[_stage].Count < 2)
                    {
                        doneGenerating = true;
                        continue;
                    }

                    var linesList = new List<List<Line>>();
                    for (var i = 0; i < _frames[_stage].Count; i++)
                    {
                        var top = _frames[_stage][i];
                        var bot = _frames[_stage][i + 1];

                        var lines = GenerateLine(new[] {top.X, top.Y}, new[] {bot.X, bot.Y});
                        linesList.Add(lines);

                        foreach (var line in lines)
                        {
                            layout.Children.Add(line);
                        }

                        i++;
                    }

                    var frames = new List<Position>();

                    foreach (var lines in linesList)
                    {
                        var frame = GetFrame("", "");

                        layout.Children.Add(frame, new Point(lines[3].X2, lines[3].Y2 - 50));

                        frames.Add(new Position() {X = lines[3].X2, Y = lines[3].Y2 - 50});
                    }

                    _frames.Add(frames);
                    _stage++;
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
            else
            {
                var scroll = new ScrollView
                {
                    Orientation = ScrollOrientation.Vertical,
                    Content = new ScrollView
                    {
                        Orientation = ScrollOrientation.Horizontal,
                        //TODO: add error message not enought players to generate spider or idk...
                    }
                };

                return scroll;
            }
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
                Padding = new Thickness(0, 12),
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
                Padding = new Thickness(0, 12),
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

            var tap = new TapGestureRecognizer {Command = TapCommand, CommandParameter = $"{leftName} : {rightName}"};

            st.Children.Add(addButton1);
            st.Children.Add(vsImage);
            st.Children.Add(addButton2);

            frame.Content = st;
            frame.GestureRecognizers.Add(tap);

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
                Stroke = Brush.Black
            };

            var botLine = new Line
            {
                X1 = bot[0] + _frameWidth + 40,
                X2 = bot[0] + _frameWidth + _lineWidth,
                Y1 = bot[1] + (_frameHeight / 2) + 20,
                Y2 = bot[1] + (_frameHeight / 2) + 20,
                Stroke = Brush.Black
            };

            var connectingLine = new Line
            {
                X1 = top[0] + _frameWidth + _lineWidth,
                X2 = bot[0] + _frameWidth + _lineWidth,
                Y1 = top[1] + (_frameHeight / 2) + 20,
                Y2 = bot[1] + (_frameHeight / 2) + 20,
                Stroke = Brush.Black
            };

            var lenght = (top[1] + (_frameHeight / 2) + 20 + bot[1] + (_frameHeight / 2) + 20) / 2;

            var middleLine = new Line
            {
                X1 = top[0] + _frameWidth + _lineWidth,
                X2 = top[0] + _frameWidth + _lineWidth * 3,
                Y1 = lenght,
                Y2 = lenght,
                Stroke = Brush.Black
            };

            lines.Add(topLine);
            lines.Add(botLine);
            lines.Add(connectingLine);
            lines.Add(middleLine);

            return lines;
        }
        
        private void OpenPopup(string matchString)
        {
            _navigation.PushPopupAsync(new MatchResultsPage(matchString, _tournamentId));
        }

        private bool IsNumberPowerOf2(int num)
        {
            if (num is 0 or 1)
                return false;

            while (num != 1)
            {
                if (num % 2 != 0)
                    return false;
                
                num /= 2;
            }
            return true;
        }
    }
}