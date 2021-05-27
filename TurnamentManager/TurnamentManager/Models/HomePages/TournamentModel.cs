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

        public ImageSource SportI;

        public ImageSource TrophyI;
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

            ImageSource SetSport()
            {
                if (tournament.Style == "Tennis")
                {
                    return  SportI = "tennis_ball.png";
                }
                if (tournament.Style == "Basketball")
                {
                    return  SportI = "basket_ball.png";
                }
                if (tournament.Style == "Football")
                {
                    return  SportI = "football_ball1.png";
                }
                if (tournament.Style == "TableFootball")
                {
                    return  SportI = "table_football.png";
                }
                if (tournament.Style == "Pool")
                {
                    return  SportI = "pool_ball.png";
                }
                if (tournament.Style == "PingPongRacket")
                {
                    return  SportI = "ping_pong_racket.png";
                }
                if (tournament.Style == "Badminton")
                {
                    return  SportI = "badminton_yellow_avatar.png";
                }
                if (tournament.Style == "Baseball")
                {
                    return  SportI = "baseball_avatar.png";
                }
                if (tournament.Style == "Golf")
                {
                    return  SportI = "golf_avatar.png";
                }


                return SportI;
            }
            
            ImageSource SetTrophy()
            {
                if (tournament.Trophy == 0)
                {
                    return  TrophyI = "trophy_avarar.png";
                }
                if (tournament.Trophy == 1)
                {
                    return  TrophyI = "silver_trophy_avatar.png";
                }
                if (tournament.Trophy == 2)
                {
                    return  TrophyI = "bronze_trophy_avatar.png";
                }

                return TrophyI;
            }

            
            
            
            var sport = new CircleImage
            {
                Source = SetSport(),
                Aspect = Aspect.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                ScaleX = 0.5,
                ScaleY = 0.5,
            };
            
            var avatar = new Image
            {
                Source = SetTrophy(),
                Aspect = Aspect.Fill,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                ScaleX = 0.5,
                ScaleY = 0.5,
            };
            
            var label = new Label
            {
                Text = tournament.Name,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.StartAndExpand,
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
            var middleStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center
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

            
            middleStackLayout.Children.Add(label);
            middleStackLayout.Children.Add(avatar);
            
            st.Children.Add(sport);
            st.Children.Add(middleStackLayout);
            
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