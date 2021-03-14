using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;
using CheckBox = XLabs.Forms.Controls.CheckBox;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerOrTeamAddPage : ContentPage
    {
        public PlayerOrTeamAddPage()
        {
            InitializeComponent();
        }
        
        public PlayerOrTeamAddPage(int id)
        {
            InitializeComponent();
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


        private void SaveButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DrawPage());
        }
    }
}