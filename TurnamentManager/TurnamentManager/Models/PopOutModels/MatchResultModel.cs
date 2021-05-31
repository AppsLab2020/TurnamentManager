using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Rg.Plugins.Popup.Extensions;
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

        public string LeftName
        {
            get => _leftName;
            set
            {
                if (value == _leftName)
                    return;

                _leftName = value;
                OnPropertyChanged();
            }
        }

        public string RightName
        {
            get => _rightName;
            set
            {
                if (value == _rightName)
                    return;

                _rightName = value;
                OnPropertyChanged();
            }
        }

        private int _leftScore = 0;
        private int _rightScore = 0;

        private readonly int _tournamentId;
        private readonly string _match;
        private INavigation _navigation;
        private int _totalMatches;
        private string _leftName;
        private string _rightName;
        private int _matchId;

        public MatchResultModel(string match, int tournamentId, INavigation navigation, int totalMatches) //TODO: canEdit bool if false plus and minus will not work
        {
            _match = match;
            _tournamentId = tournamentId;
            _navigation = navigation;
            _totalMatches = totalMatches;

            SaveCommand = new Command(Save);

            AddLeftCommand = new Command(AddLeft);
            RemoveLeftCommand = new Command(RemoveLeft);

            AddRightCommand = new Command(AddRight);
            RemoveRightCommand = new Command(RemoveRight);

            LeftName = _match.Split(' ')[0];
            RightName = _match.Split(' ')[2];
            
            _matchId = int.Parse(_match.Split(' ')[_match.Split(' ').Length - 1]);
            
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            foreach (var tournament in tournaments.Where(tournament => tournament.ID == _tournamentId))
            {
                if (string.IsNullOrEmpty(tournament.ResultsString))
                {
                    LeftScore = 0;
                    RightScore = 0;
                }
                else
                {
                    var results = tournament.ResultsString.Split('\n').ToList();
                    results.RemoveAt(results.Count-1);

                    foreach (var scores in from result in results where result != ";" select result.Split(' '))
                    {
                        tournament.ResultsStringList.Add(scores[0] + " : " + scores[2] + " \n");
                    }

                    if (tournament.ResultsStringList[_matchId].Split(' ')[0] == "none")
                        return;
                    
                    LeftScore = int.Parse(tournament.ResultsStringList[_matchId].Split(' ')[0]);
                    RightScore = int.Parse(tournament.ResultsStringList[_matchId].Split(' ')[2]);
                }
            }
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
                if (string.IsNullOrEmpty(tournament.ResultsString))
                {
                    for (var i = 0; i < _totalMatches; i++)
                    {
                        tournament.ResultsStringList.Add("none : none \n");
                    }
                }
                else
                {
                    var results = tournament.ResultsString.Split('\n').ToList();
                    results.RemoveAt(results.Count-1);

                    foreach (var scores in from result in results where result != ";" select result.Split(' '))
                    {
                        tournament.ResultsStringList.Add(scores[0] + " : " + scores[2] + " \n");
                    }
                }

                tournament.ResultsStringList[_matchId] = $"{LeftScore} : {RightScore} \n";
                tournament.ResultsString = "";
                tournament.MakeResultsString();

                conn.Query<Tournament>($"UPDATE Tournament SET ResultsString='{tournament.ResultsString}' WHERE ID={tournament.ID}");
                
                _navigation.PopPopupAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}