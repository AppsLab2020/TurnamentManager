using System;
using System.IO;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class CreateTournamentModel
    {
        public ICommand SaveDataCommand { get; set; }

        public int Format { get; set; }
        public int IsTeamBased { get; set; }
        public int Sport { get; set; }
        public string Name { get; set; }

        private INavigation _navigation;

        public CreateTournamentModel(INavigation navigation)
        {
            _navigation = navigation;
            SaveDataCommand = new Command(SaveData);
        }

        private async void SaveData()
        {
            var tournament = new Tournament()
            {
                
                Format = Format switch
                {
                    0 => "Knockout",
                    1 => "Groups",
                    //....
                    _ => throw new ArgumentOutOfRangeException()
                },
                IsTeamBased = IsTeamBased switch
                {
                    0 => true,
                    1 => false,
                    _ => throw new ArgumentOutOfRangeException()
                },//Add button to choose
                Name = Name,
                Style = Sport switch
                {
                    0 => "Football",
                    1 => "Basketball",
                    _ => throw new ArgumentOutOfRangeException()
                }
            };

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            conn.Insert(tournament);

            await _navigation.PopAsync();
        }
    }
}