using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        private static ICommand RemoveCommand => new Command<int>(RemovePlayer);
        
        private static List<Player> _players;

        public PlayerPage()
        {
            InitializeComponent();
            _players = new List<Player>();
            
        }
        
       async private void Button_OnClicked(object sender, EventArgs e)
        {
            await PlusButton.TranslateTo(10, 0, 500, Easing.BounceOut);
            await PlusButton.TranslateTo(0, 0);
            await Navigation.PushAsync(new CreatePlayerPage());
        }

        public void PrepniMaNaPagu(ContentPage page)
        {
            Navigation.PushAsync(page);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            await DrawPlayers();
        }

        private async Task DrawPlayers()
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();
            _players.Clear();
            _players.AddRange(players);

            PlayerStackLayout.Children.Clear();
            foreach (var player in players)
            {
                var frame = await Task.Run(() => MakeFrameAsync(player)); //slower but appears one by one
                PlayerStackLayout.Children.Add(frame);
            }
            
            /*
             var tasks = players.Select(player => Task.Run(() => MakeFrameAsync(player))).ToList(); //faster but appears all at once
             var frames = await Task.WhenAll(tasks);
             foreach(var frame in frames)
             {
                PlayerStackLayout.Children.Add(frame);
             }
            */
        }
         //Frame specs
        private static async Task<Frame> MakeFrameAsync(Player player)
        {
            var label = new Label
            {
                Text = player.Name,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
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
            
            st.Children.Add(label);
            st.Children.Add(imageButton);
            frame.Content = st;

            return frame;
        }

        private static void RemovePlayer(int id)
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.Query<Player>("DELETE FROM Player Where ID=?", id);
        }
    }
}

//Add removing data from database at one point 