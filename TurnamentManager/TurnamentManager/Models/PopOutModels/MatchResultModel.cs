using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;

namespace TurnamentManager.Models.PopOutModels
{
    public class MatchResultModel : INotifyPropertyChanged
    {
        public Command SaveCommand { get; set; }

        public Command AddLeftCommand { get; set; }
        public Command RemoveLeftCommand { get; set; }

        public Command AddRightCommand { get; set; }
        public Command RemoveRightCommand { get; set; }

        public int LeftScore
        {
            get => _leftScore;
            set
            {
                if (value == _leftScore)
                    return;

                _leftScore = value;
                OnPropertyChanged();
            }
        }

        public int RightScore
        {
            get => _rightScore;
            set
            {
                if (value == _rightScore)
                    return;

                _rightScore = value;
                OnPropertyChanged();
            }
        }

        private int _leftScore = 0;
        private int _rightScore = 0;

        private readonly int _tournamentId;
        private readonly string _match;
        private INavigation _navigation;

        public MatchResultModel(string match, int tournamentId, INavigation navigation)
        {
            _match = match;
            _tournamentId = tournamentId;
            _navigation = navigation;

            SaveCommand = new Command(Save);

            AddLeftCommand = new Command(AddLeft);
            RemoveLeftCommand = new Command(RemoveLeft);

            AddRightCommand = new Command(AddRight);
            RemoveRightCommand = new Command(RemoveRight);
        }

        private void AddLeft()
        {
            LeftScore++;
        }

        private void RemoveLeft()
        {
            LeftScore--;
        }

        private void AddRight()
        {
            RightScore++;
        }

        private void RemoveRight()
        {
            RightScore--;
        }

        private void Save()
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == _tournamentId))
            {
                for (var i = 0; i < tournament.MatchesString.Split('\n').Length; i++)
                {
                    if (tournament.MatchesString.Split('\n')[i] == _match)
                    {
                        tournament.ResultsStringList[i] = $"{LeftScore} : {RightScore} \n";
                    }
                }

                tournament.MakeResultsString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}