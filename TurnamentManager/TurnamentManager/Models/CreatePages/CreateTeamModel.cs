using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class CreateTeamModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddPlayerCommand { get; set; }

        public ObservableCollection<string> PlayerNamesCollection { get; set; }

        private List<Player> _players;

        public CreateTeamModel()
        {
            _players = new List<Player>();
            PlayerNamesCollection = new ObservableCollection<string>();
            AddPlayerCommand = new Command(AddPlayer);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void AddPlayer()
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();

            var action = await Application.Current.MainPage.DisplayActionSheet("Choose player", "Cancel", null,
                players.Select(player => player.Name).ToArray());

            var exist = false;

            foreach (var player in _players.Where(player => player.Name == action))
                exist = true;

            if (action == "cancel" || string.IsNullOrWhiteSpace(action) || exist)
                return;

            foreach (var player in players.Where(player => player.Name == action))
            {
                _players.Add(player);
                PlayerNamesCollection.Add(player.Name);
            }
        }
    }
}

//TODO: Add removing players, saving