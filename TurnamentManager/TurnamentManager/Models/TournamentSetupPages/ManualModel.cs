using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Views;
using TurnamentManager.Views.TournamentSetupPages;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class ManualModel : INotifyPropertyChanged
    {
        public delegate void MatchEventHandler(
            object sender,
            MatchEventArgs args);

        public Command NavigateToNextCommand { get; set; }

        public event MatchEventHandler FullMatchEventHandler;

        public INavigation Navigation;
        private ICommand _leftButtonCommand { get; set; }
        private ICommand _rigthButtonCommand { get; set; }

        private List<Player> _usablePlayerList;

        private string _leftButtonImage;
        private string _rightButtonImage;

        private int _tournametId;

        private string _matches;

        public string LeftButtonImage
        {
            get
            {
                return _leftButtonImage;
            }
            set
            {
                if (value == _leftButtonImage)
                    return;

                _leftButtonImage = value;
                OnPropertyChanged();
            }
        }

        public string RightButtonImage
        {
            get
            {
                return _rightButtonImage;
            }
            set
            {
                if (value == _rightButtonImage)
                    return;

                _rightButtonImage = value;
                OnPropertyChanged();
            }
        }

        public ManualModel(INavigation navigation, int tournamentId, ref bool allMatchesGenerated)
        {
            _matches = "";
            Navigation = navigation;

            NavigateToNextCommand = new Command(Navigate);

            _tournametId = tournamentId;

            _leftButtonCommand = new Command(LeftButtonCommand);
            _rigthButtonCommand = new Command(RightButtonCommand);

            LeftButtonImage = "add_button.png";
            RightButtonImage = "add_button.png";

            _usablePlayerList = new List<Player>();

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            using var conn2 = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn2.CreateTable<Player>();
            var players = conn2.Table<Player>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == tournamentId))
            {
                var playerIds = tournament.PlayersIDString.Split(' ').ToList();
                playerIds.RemoveAt(playerIds.Count - 1);
                Console.WriteLine(playerIds[0]);
                Console.WriteLine();

                foreach (var t in from t in players from t1 in playerIds let compare = t.ID == int.Parse(t1) where compare select t)
                {
                    _usablePlayerList.Add(t);
                }
            }

            Console.WriteLine("ha");
        }

        public Frame GenereateFrame()
        {
            var addButton1 = new ImageButton
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Command = _leftButtonCommand
            };
            addButton1.SetBinding(ImageButton.SourceProperty, new Binding("LeftButtonImage", BindingMode.Default));
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
                Command = _rigthButtonCommand
            };
            addButton2.SetBinding(ImageButton.SourceProperty, new Binding("RightButtonImage", BindingMode.Default));
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
            var addButton1 = new Label
            {
                HeightRequest = 50,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = leftButtonImage,
                FontFamily = "PixL",
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
                Text = rightButtonImage,
                FontFamily = "PixL",
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

        private async void LeftButtonCommand()
        {
            var action = await Application.Current.MainPage.DisplayActionSheet("Choose player", "Cancel", null,
                _usablePlayerList.Select(player => player.Name).ToArray());

            foreach (var player in _usablePlayerList.Where(player => player.Name == action))
            {
                _usablePlayerList.Remove(player);
                break;
            }

            if (action == "Cancel")
                return;

            LeftButtonImage = action;

            if (LeftButtonImage == "add_button.png" || RightButtonImage == "add_button.png") 
                return;

            
            FullMatchEventHandler?.Invoke(this, new MatchEventArgs(LeftButtonImage, RightButtonImage));
            GenerateMatch();

        }

        private async void RightButtonCommand()
        {
            var action = await Application.Current.MainPage.DisplayActionSheet("Choose player", "Cancel", null,
                _usablePlayerList.Select(player => player.Name).ToArray());

            foreach (var player in _usablePlayerList.Where(player => player.Name == action))
            {
                _usablePlayerList.Remove(player);
                break;
            }

            if (action == "Cancel")
                return;

            RightButtonImage = action;

            if (LeftButtonImage == "add_button.png" || RightButtonImage == "add_button.png")
                return;

            
            FullMatchEventHandler?.Invoke(this, new MatchEventArgs(LeftButtonImage, RightButtonImage));
            GenerateMatch();

        }

        private void Navigate()
        {
            Navigation.PushAsync(new MatchPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GenerateMatch()
        {
            _matches += $"{LeftButtonImage} : {RightButtonImage}  \n";
            LeftButtonImage = "add_button.png";
            RightButtonImage = "add_button.png";
        }
    }
}