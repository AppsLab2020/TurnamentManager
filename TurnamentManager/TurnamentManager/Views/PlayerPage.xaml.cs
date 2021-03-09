using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreatePlayerPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();

            foreach (var player in players)
            {
                var label = new Label
                {
                    Text = player.Name,
                };
                var st = new StackLayout { };
                var frame = new Frame
                {
                    
                    CornerRadius = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    HasShadow = true,
                    IsClippedToBounds = true,
                    Padding = 0,
                };
                frame.Content = st;
                st.Children.Add(label);
                
                PlayerStackLayout.Children.Add(frame);
            }
            
          
        }
    }
}