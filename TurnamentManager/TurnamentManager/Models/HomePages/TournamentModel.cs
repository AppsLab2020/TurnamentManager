using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Views;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using ImageButton = Xamarin.Forms.ImageButton;

namespace TurnamentManager.Models
{
    public class TournamentModel : INotifyPropertyChanged
    {
        public event EventHandler RedrawPlayers;

        public Command NextCommand { get; set; }

        public int Height
        {
            get => _height;
            set
            {
                if (value == _height)
                    return;

                _height = value;
                OnPropertyChanged();
            }
        }

        private int _height;

        private Command RemoveCommand => new Command<int>(RemoveTournament);
        private Command TapCommand => new Command<int>(OpenPage);

        private readonly INavigation _navigation;

        public TournamentModel(INavigation navigation)
        {
            _navigation = navigation;

            NextCommand = new Command(Next);
        }

        public async Task GetFrames(List<Frame> frames)
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            Height = tournaments.Count * 80 + 150;

            foreach (var tournament in tournaments)
            {
                var frame = await Task.Run(() => MakeFrameAsync(tournament));
                frames.Add(frame);
            }
        }

        private Frame MakeFrameAsync(Tournament tournament)
        {
            var avatar = new CircleImage
            {
                Source = "trophy_avarar.png",
                Aspect = Aspect.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                ScaleX = 1,
                ScaleY = 1,
            };
            
            
            var label = new Label
            {
                Text = tournament.Name,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "PixL",
            };

            var imageButton = new ImageButton
            {
                Source = "trash_bin.png",

                Command = RemoveCommand,
                BackgroundColor = Color.Transparent,
                CommandParameter = tournament.ID,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand,

            };

            var st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                WidthRequest = 300,
                HeightRequest = 80,
            };

            var frame = new Frame
            {
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HasShadow = true,
                IsClippedToBounds = true,

            };

            var tap = new TapGestureRecognizer { Command = TapCommand, CommandParameter = tournament.ID };

            frame.GestureRecognizers.Add(tap);

            st.Children.Add(avatar);
            st.Children.Add(label);
            st.Children.Add(imageButton);
            frame.Content = st;

            return frame;
        }

        private void OpenPage(int id)
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();
            foreach (var tournament in tournaments.Where(tournament => tournament.ID == id))
            {
                if (string.IsNullOrEmpty(tournament.MatchesString))
                {
                    _navigation.PushAsync(new PlayerOrTeamAddPage(id));
                }
                else
                {
                    _navigation.PushAsync(new MatchPage(id));
                }
            }
            
            
            
        }

        private void RemoveTournament(int id)
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.Query<Player>("DELETE FROM Tournament Where ID=?", id);

            RedrawPlayers?.Invoke(this, EventArgs.Empty);
        }

        private void Next()
        {
            _navigation.PushAsync(new CreateTournamentPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}