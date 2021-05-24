using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;
using ImageButton = Xamarin.Forms.ImageButton;

namespace TurnamentManager.Models
{
    public class PlayerModel : INotifyPropertyChanged
    {
        public event EventHandler RedrawPlayers;
        public Command NextCommand { get; set; }

        public ImageSource TestImage;

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

        private int _height = 0;

        private INavigation _navigation;

        private ICommand RemoveCommand => new Command<int>(RemovePlayer);

        public PlayerModel(INavigation navigation)
        {
            _navigation = navigation;
            NextCommand = new Command(Next);
        }

        public async Task GetFrames(List<Frame> lst)
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();

            Height = players.Count * 90 + 150;

            foreach (var player in players)
            {
                var frame = await Task.Run(() => MakeFrameAsync(player)); //slower but appears one by one
                lst.Add(frame);
            }

            return;
        }

        private void Next()
        {
            _navigation.PushAsync(new CreatePlayerPage());
        }


       
        
        private Frame MakeFrameAsync(Player player)
        {
            
            ImageSource SetAvatar()
            {
                if (player.ImageId == 0)
                {
                    return TestImage = "football_player_avatar.png";
                }

                if (player.ImageId == 1)
                {
                    return  TestImage = "rugby_player_avatar.png";
                }
                if (player.ImageId == 2)
                {
                    return  TestImage = "tennis_player_avatar.png";
                }
                if (player.ImageId == 3)
                {
                    return  TestImage = "pingpong_player_avatar.png";
                }
                if (player.ImageId == 4)
                {
                    return  TestImage = "basketball_avatar.png";
                }
                if (player.ImageId == 5)
                {
                    return  TestImage = "hockey_player_avatar.png";
                }
                if (player.ImageId == 6)
                {
                    return  TestImage = "girl_pic_avatar.png";
                }

                return TestImage;
            }
            

            
            var avatar = new CircleImage
            {
                Source = SetAvatar(),
                Aspect = Aspect.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                ScaleY = 1,
                ScaleX = 1,
            };
            

            var label = new Label
            {
                Text = player.Name,
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
                CommandParameter = player.ID,
                VerticalOptions = LayoutOptions.CenterAndExpand,
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
                Padding = 15,
            };

            
            
            
            st.Children.Add(avatar);
            st.Children.Add(label);
            st.Children.Add(imageButton);
            frame.Content = st;

            return frame;
        }

        private async void RemovePlayer(int id)
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.Query<Player>("DELETE FROM Player Where ID=?", id);

            RedrawPlayers?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}