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

namespace TurnamentManager.Views.PopOutPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchResultsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public MatchResultsPage(string match, int tournamentId)
        {
            InitializeComponent();
            
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == tournamentId))
            {
                for (var i = 0; i < tournament.MatchesString.Split('\n').Length; i++)
                {
                    if (tournament.MatchesString.Split('\n')[i] == match)
                    {
                        tournament.ResultsStringList[i] = "0 : 0 \n";
                    }
                }
                
                tournament.MakeResultsString();
            }
        }
        
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}