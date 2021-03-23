using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTeamPage : ContentPage
    {
        private static ICommand RemoveCommand => new Command<int>(RemovePlayer);

        public static ObservableCollection<Player> Players;
        public CreateTeamPage()
        {
            InitializeComponent();
            Players = new ObservableCollection<Player>();
            PlayersDisplay.ItemsSource = Players;
        }

        private void AddPlayerButton_OnClicked(object sender, EventArgs e)
        {
            OnActionSheetSimpleClicked(sender, e);
        }

        private async void OnActionSheetSimpleClicked(object sender, EventArgs e)
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();

            var action = await DisplayActionSheet ("Choose player", "Cancel", null, players.Select(player => player.Name).ToArray());

            var exist = false;

            foreach (var player in Players.Where(player => player.Name == action))
                exist = true;

            if (action == "cancel" || string.IsNullOrWhiteSpace(action) || exist)
                return;
            
            foreach (var player in players.Where(player => player.Name == action))
                Players.Add(player);
        }

        private static void RemovePlayer(int id)
        {
            foreach (var player in Players.Where(player => player.ID == id))
            {
                Players.Remove(player);
                break;
            }
        }

      private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            await SaveButton.ScaleTo(1.2, 500, Easing.SpringOut); 
            await SaveButton.ScaleTo(1, 300);
        }
    }
}

//List is not refreshing when someone delete item view doesn't refresh after