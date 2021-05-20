using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views.TournamentSetupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualPage : ContentPage
    {
        private ManualModel _model;

        private int _numberOfMatches = 0;

        private bool matchesGenerated = false;

        public ManualPage(int tournamentId)
        {
            InitializeComponent();
            _model = new ManualModel(Navigation, tournamentId, ref matchesGenerated);
            BindingContext = _model;

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == tournamentId))
                _numberOfMatches = tournament.PlayersIDString.Split(' ').Length / 2;

            _model.FullMatchEventHandler += GetMatch;

            FrameLayout.Children.Add(_model.GenereateFrame());
        }

        private void GetMatch(object sender, MatchEventArgs args)
        {
            if (_numberOfMatches == 0) 
                return;
            FrameLayout.Children.Add(_model.GenereateFrame(args.LeftSide, args.RightSide));
            _numberOfMatches--;
        }

    }
}