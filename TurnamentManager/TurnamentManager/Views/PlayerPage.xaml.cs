﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        private List<Player> _players;
        public PlayerPage()
        {
            InitializeComponent();

            _players = new List<Player>();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreatePlayerPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();
            _players.Clear();
            _players.AddRange(players);
            
            /*foreach (var player in players)
            {
                var frame = await Task.Run(() => MakeFrameAsync(player)); //slower but appears one by one
                PlayerStackLayout.Children.Add(frame);
            }*/
            
            
             var tasks = players.Select(player => Task.Run(() => MakeFrameAsync(player))).ToList(); //faster but appears all at once
             var frames = await Task.WhenAll(tasks);
             foreach(var frame in frames)
             {
                PlayerStackLayout.Children.Add(frame);
             }
             
        }

        private static async Task<Frame> MakeFrameAsync(Player player)
        {
            var label = new Label
            {
                Text = player.Name,
                TextColor = Color.Black,
            };
            var imageButton = new ImageButton
            {
                Source = "binTest.png",
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
            st.Children.Add(label);
            st.Children.Add(imageButton);
            frame.Content = st;

            return frame;
        }
    }
}