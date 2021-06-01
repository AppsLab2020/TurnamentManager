using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Views.TournamentSetupPages;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class DrawModel
    {
        public Command NavigateCommand { get; set; }

        public INavigation Navigation;
        public ICommand RandomDrawCommand { get; set; }

        private int _tournamentId;

        public DrawModel(INavigation navigation, int tournamentID)
        {
            Navigation = navigation;
            NavigateCommand = new Command(Navigate);
            RandomDrawCommand = new Command(Draw);
            _tournamentId = tournamentID;
        }

        private void Draw()
        {
            Navigation.PushAsync(new RandomPage(_tournamentId));

            /*using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            using var conn2 = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn2.CreateTable<Player>();
            var players = conn2.Table<Player>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == _tournamentId))
            {
                var playerIDs = tournament.PlayersIDString.Split(' ');
                int matches = (playerIDs.Length - 1) / 2;
                var drawMatches = "";
                var usedPlayers = new List<string>();
                var random = new Random();
                var first = true;

                for (var i = 0; i < playerIDs.Length - 1; i++)
                {
                    usedPlayers.Add(playerIDs[i]);
                }

                while (matches != 0)
                {
                    var rand = random.Next(0, usedPlayers.Count);

                    drawMatches += first ? usedPlayers[rand].ToString() + " : " : usedPlayers[rand].ToString() + " \n";

                    usedPlayers.RemoveAt(rand);

                    if (!first)
                        matches--;

                    first = !first;
                }

                var save = Application.Current.MainPage.DisplayAlert("Random Draw", drawMatches, "save", "discard");

            }*/
        }

        private void Navigate()
        {
            Navigation.PushAsync(new ManualPage(_tournamentId));
        }
    }
}