using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;
using CheckBox = XLabs.Forms.Controls.CheckBox;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerOrTeamAddPage : ContentPage
    {
        private PlayerOrTeamAddModel model;
        public PlayerOrTeamAddPage()
        {
            InitializeComponent();
            model = new PlayerOrTeamAddModel(Navigation);
            BindingContext = model;
        }
        
        public PlayerOrTeamAddPage(int id)
        {
            InitializeComponent();
            model = new PlayerOrTeamAddModel(Navigation);
            BindingContext = model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();
            Players.Children.Clear();

            foreach (var checkBox in players.Select(player => new CheckBox
            {
                DefaultText = player.Name,
                TextColor = Color.Black,
            }))
            {
                Players.Children.Add(checkBox);
            }
            
        }
    }
}