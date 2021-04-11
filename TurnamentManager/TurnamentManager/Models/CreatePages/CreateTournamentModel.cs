using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class CreateTournamentModel : INotifyPropertyChanged
    {
        public ICommand SaveDataCommand { get; set; }

        public string Name { get; set; }

        public ICommand GoldTournamentCommand { get; }
        public ICommand SilverTournamentCommand { get; }
        public ICommand BronzeTournamentCommand { get; }
        public ICommand TennisCommand { get; }
        public ICommand BasketballCommand { get; }
        public ICommand FootballCommand { get; }
        public ICommand TableFootballCommand { get; }
        public ICommand PoolCommand { get; }
        public ICommand PingPongRacketCommand { get; }
        public ICommand KnockoutCommand { get; }
        public ICommand TeamBasedCommand { get; }
        public ICommand PlayerBasedCommand { get; }

        public ImageSource CupImageSource
        {
            get => _cupImageSource;
            set
            {
                if (value == _cupImageSource)
                    return;

                _cupImageSource = value;
                OnPropertyChanged();
            }
        }

        public ImageSource SportImageSource
        {
            get => _sportImageSource;
            set
            {
                if (value == _sportImageSource)
                    return;

                _sportImageSource = value;
                OnPropertyChanged();
            }
        }

        public ImageSource FormatImageSource
        {
            get => _formatImageSource;
            set
            {
                if (value == _formatImageSource)
                    return;

                _formatImageSource = value;
                OnPropertyChanged();
            }
        }

        public ImageSource StyleImageSource
        {
            get => _styleImageSource;
            set
            {
                if (value == _styleImageSource)
                    return;

                _styleImageSource = value;
                OnPropertyChanged();
            }
        }

        public bool IsTypeExpanded
        {
            get => _isTypeExpanded;
            set
            {
                if (value == _isTypeExpanded)
                    return;

                _isTypeExpanded = value;
                OnPropertyChanged();
            }
        }

        public bool IsSportExpanded
        {
            get => _isSportExpanded;
            set
            {
                if (value == _isSportExpanded)
                    return;

                _isSportExpanded = value;
                OnPropertyChanged();
            }
        }

        public bool IsFormatExpanded
        {
            get => _isFormatExpanded;
            set
            {
                if (value == _isFormatExpanded)
                    return;

                _isFormatExpanded = value;
                OnPropertyChanged();
            }
        }

        public bool IsStyleExpanded
        {
            get => _isStyleExpanded;
            set
            {
                if (value == _isStyleExpanded)
                    return;

                _isStyleExpanded = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _cupImageSource;
        private ImageSource _sportImageSource;
        private ImageSource _formatImageSource;
        private ImageSource _styleImageSource;

        private bool _isTypeExpanded;
        private bool _isSportExpanded;
        private bool _isFormatExpanded;
        private bool _isStyleExpanded;

        private int _selectedCupId = -1;
        private int _selectedSportId = -1;
        private int _selectedFormatId = -1;
        private int _selectedStyleId = -1;

        private INavigation _navigation;

        public CreateTournamentModel(INavigation navigation)
        {
            _navigation = navigation;
            SaveDataCommand = new Command(SaveData);

            GoldTournamentCommand = new Command(Gold);
            SilverTournamentCommand = new Command(Silver);
            BronzeTournamentCommand = new Command(Bronz);
            TennisCommand = new Command(Tennis);
            BasketballCommand = new Command(Basketball);
            FootballCommand = new Command(Football);
            TableFootballCommand = new Command(TableFootball);
            PoolCommand = new Command(Pool);
            PingPongRacketCommand = new Command(PingPongRacket);
            KnockoutCommand = new Command(Knockout);
            TeamBasedCommand = new Command(TeamBased);
            PlayerBasedCommand = new Command(PlayerBased);

            CupImageSource = "trophy.png";
            SportImageSource = "choose_sport1.png";
            FormatImageSource = "Knockout.png";
            StyleImageSource = "player_icon.png";
        }

        private async void SaveData()
        {
            var tournament = new Tournament()
            {
                Format = _selectedFormatId switch
                {
                    0 => "Knockout",
                    //....
                    _ => throw new ArgumentOutOfRangeException()
                },
                IsTeamBased = _selectedStyleId switch
                {
                    0 => true,
                    1 => false,
                    _ => throw new ArgumentOutOfRangeException()
                }, //Add button to choose
                Name = Name,
                Style = _selectedSportId switch
                {
                    0 => "Tennis",
                    1 => "Basketball",
                    2 => "Football",
                    3 => "TableFootball",
                    4 => "Pool",
                    5 => "PingPongRacket",
                    _ => throw new ArgumentOutOfRangeException()
                }
            };

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            conn.Insert(tournament);

            await _navigation.PopAsync();
        }

        private void Gold()
        {
            CupImageSource = "trophy.png";
            IsTypeExpanded = false;
            _selectedCupId = 0;
        }

        private void Silver()
        {
            CupImageSource = "silver_trophy.png";
            IsTypeExpanded = false;
            _selectedCupId = 1;
        }

        private void Bronz()
        {
            CupImageSource = "bronze_trophy.png";
            IsTypeExpanded = false;
            _selectedCupId = 2;
        }

        private void Tennis()
        {
            SportImageSource = "tennis_ball.png";
            IsSportExpanded = false;
            _selectedSportId = 0;
        }

        private void Basketball()
        {
            SportImageSource = "basket_ball.png";
            IsSportExpanded = false;
            _selectedSportId = 1;
        }

        private void Football()
        {
            SportImageSource = "football_ball.png";
            IsSportExpanded = false;
            _selectedSportId = 2;
        }

        private void TableFootball()
        {
            SportImageSource = "table_football.png";
            IsSportExpanded = false;
            _selectedSportId = 3;
        }

        private void Pool()
        {
            SportImageSource = "pool_ball.png";
            IsSportExpanded = false;
            _selectedSportId = 4;
        } 
        private void PingPongRacket()
        {
            SportImageSource = "ping_pong_racket.png";
            IsSportExpanded = false;
            _selectedSportId = 5;
        } 

        private void Knockout()
        {
            FormatImageSource = "Knockout.png";
            IsFormatExpanded = false;
            _selectedFormatId = 0;
        }

        private void TeamBased()
        {
            StyleImageSource = "team_iconn.png";
            IsStyleExpanded = false;
            _selectedStyleId = 0;
        }

        private void PlayerBased()
        {
            StyleImageSource = "player_icon.png";
            IsStyleExpanded = false;
            _selectedStyleId = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}