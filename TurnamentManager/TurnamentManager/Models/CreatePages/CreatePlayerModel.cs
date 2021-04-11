using SQLite;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class CreatePlayerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SaveDataCommand { get; set; }
        public ICommand FootballCommand { get; }
        public ICommand RugbyCommand { get; }
        public ICommand TennisCommand { get; }
        public ICommand PingPongCommand { get; }
        public ICommand BasketballCommand { get; }
        public ICommand HockeyCommand { get; }
        public ICommand GirlCommand { get; }

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value == _isExpanded)
                    return;

                _isExpanded = value;
                OnPropertyChanged();
            }
        }

        public string PlayerName { get; set; }

        public int SelectedQuality { get; set; }

        public ImageSource PickedImageSource
        {
            get => _pickImageSource;
            set
            {
                if (value == _pickImageSource)
                    return;

                _pickImageSource = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _pickImageSource;
        private bool _isExpanded;

        private readonly INavigation _navigation;

        private int _selectedImageId = -1;

        public CreatePlayerModel(INavigation navigation)
        {
            _pickImageSource = "choose_player1.png";
            SaveDataCommand = new Command(SaveData);

            FootballCommand = new Command(Football);
            RugbyCommand = new Command(Rugby);
            TennisCommand = new Command(Tennis);
            PingPongCommand = new Command(PingPong);
            BasketballCommand = new Command(Basketball);
            HockeyCommand = new Command(Hockey);
            GirlCommand = new Command(Girl);
            _navigation = navigation;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void SaveData()
        {
            var name = PlayerName;
            var pathToImage = _selectedImageId;
            var quality = SelectedQuality;
            var playerQuality = quality switch
            {
                0 => Player.PlayerQuality.Flexible,
                1 => Player.PlayerQuality.Strategic,
                2 => Player.PlayerQuality.SharesTheirExpertise,
                3 => Player.PlayerQuality.RespectfulToOthers,
                4 => Player.PlayerQuality.ContributeIdeas,
                _ => Player.PlayerQuality.Flexible
            };

            var player = new Player(name, pathToImage, playerQuality);

            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            conn.Insert(player);

            await _navigation.PopAsync();
        }

        private void Football()
        {
            PickedImageSource = "football_player.png";
            IsExpanded = false;
            _selectedImageId = 0;
        }

        private void Rugby()
        {
            PickedImageSource = "rugby_player.png";
            IsExpanded = false;
            _selectedImageId = 1;
        }

        private void Tennis()
        {
            PickedImageSource = "tennis_player.png";
            IsExpanded = false;
            _selectedImageId = 2;
        }

        private void PingPong()
        {
            PickedImageSource = "pingpong_player.png";
            IsExpanded = false;
            _selectedImageId = 3;
        }

        private void Basketball()
        {
            PickedImageSource = "basketball_player.png";
            IsExpanded = false;
            _selectedImageId = 4;
        }
        private void Hockey()
        {
            PickedImageSource = "hockey_player.png";
            IsExpanded = false;
            _selectedImageId = 5;
        }

        private void Girl()
        {
            PickedImageSource = "girl_img.png";
            IsExpanded = false;
            _selectedImageId = 6;
        }
    }
}