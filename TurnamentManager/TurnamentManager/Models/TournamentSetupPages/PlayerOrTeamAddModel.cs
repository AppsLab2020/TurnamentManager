using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Views;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class PlayerOrTeamAddModel
    {
        public ICommand NextCommand { get; set; }

        public List<bool> Players { get; }

        private INavigation _navigation;
        private int _tournamentId;
        public PlayerOrTeamAddModel(INavigation navigation, int tournamentId)
        {
            _navigation = navigation;
            NextCommand = new Command(Next);
            Players = new List<bool>();
            _tournamentId = tournamentId;

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();

            foreach (var a in players.Select(player => false))
            {
                Players.Add(a);
            }
        }

        private void Next()
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            using var conn2 = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn2.CreateTable<Player>();
            var players = conn2.Table<Player>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == _tournamentId))
            {
                for (var i = 0; i < Players.Count; i++)
                {
                    if (Players[i] == true)
                    {
                        tournament.PlayersID.Add(players[i].ID);
                    }
                }

                tournament.CreatePlayerIDString();
                conn.Query<Tournament>($"UPDATE Tournament SET PlayersIDString={tournament.PlayersIDString} WHERE ID={tournament.ID}");
            }

            _navigation.PushAsync(new DrawPage());
        }
    }
}