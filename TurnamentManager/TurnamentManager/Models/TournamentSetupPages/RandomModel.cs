using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Views;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class RandomModel
    {
        public EventHandler RedrawPlayers;
        
        public Command NavigateToNextCommand { get; set; }
        
        public Command RedrawCommand { get; set; }

        private int _tournamentId;
        private INavigation _navigation;

        private string _matches;
        
        public RandomModel(INavigation navigation, int tournamentId)
        {
            _navigation = navigation;
            _tournamentId = tournamentId;
            _matches = "";

            NavigateToNextCommand = new Command(Navigate);
            RedrawCommand = new Command(() =>
            {
                RedrawPlayers?.Invoke(this, EventArgs.Empty);
            });
        }

        public List<Frame> GetRandomDraw()
        {
            var frames = new List<Frame>();

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            using var conn2 = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn2.CreateTable<Player>();
            var players = conn2.Table<Player>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == _tournamentId))
            {
                var playerIDs = tournament.PlayersIDString.Split(' ');
                int matches = (playerIDs.Length - 1) / 2;
                var drawMatches = "";
                var usedPlayers = new List<string>();
                var random = new Random();
                var first = true;
                var lastName = "";
                var currentName = "";

                for (var i = 0; i < playerIDs.Length - 1; i++)
                {
                    usedPlayers.Add(playerIDs[i]);
                }

                while (matches != 0)
                {
                    var rand = random.Next(0, usedPlayers.Count);

                    lastName = currentName;

                    foreach (var player in players.Where(player => player.ID == int.Parse(usedPlayers[rand])))
                    {
                        currentName = player.Name;
                    }

                    if (!first)
                    {
                        matches--;
                        drawMatches += $"{lastName} : {currentName} \n";

                        frames.Add(GenerateFrame(lastName, currentName));
                    }
                    
                    usedPlayers.RemoveAt(rand);
                    
                    first = !first;
                }

                _matches = drawMatches;
            }

            return frames;
        }


        public Frame GenerateFrame(string rightButtonImage, string leftButtonImage)
        {
            var addButton1 = new Label
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = leftButtonImage,
                FontFamily = "PixL",
                Padding = new Thickness(0,15,3,0),
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
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = rightButtonImage,
                FontFamily = "PixL",
                Padding = new Thickness(0,15,3,0),
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
        
        private void Navigate()
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.Query<Tournament>($"UPDATE Tournament SET MatchesString='{_matches}' WHERE ID={_tournamentId}");
            _navigation.PushAsync(new MatchPage(_tournamentId));
        }
    }
}