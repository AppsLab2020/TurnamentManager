using System;
using System.Collections.Generic;
using SQLite;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TurnamentManager.Classes;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class CreatePlayerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ExpanderTemplate PlayerExpander;

        public ICommand SaveDataCommand { get; set; }

        public string PlayerName { get; set; }

        public int SelectedQuality { get; set; }

        private readonly INavigation _navigation;

        public CreatePlayerModel(INavigation navigation)
        {
            SaveDataCommand = new Command(SaveData);

            _navigation = navigation;

            PlayerExpander = new ExpanderTemplate(new List<ImageSource>() { "choose_player1.png", "football_player.png", "rugby_player.png", 
                "tennis_player.png", "pingpong_player.png", "basketball_player.png", "hockey_player.png", "girl_img.png"});

            PlayerExpander.Changed += (sender, args) =>
            {
                OnPropertyChanged("PlayerExpander.IsExpanded");
                OnPropertyChanged("PlayerExpander.CurrentImageSource");
            };

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void SaveData()
        {
            if (!string.IsNullOrEmpty(PlayerName) && PlayerExpander.SelectedId != -1 && SelectedQuality is >= 0 and <= 4)
            {
                var name = PlayerName;
                var pathToImage = PlayerExpander.SelectedId;
                var quality = SelectedQuality;
                var playerQuality = quality switch
                {
                    0 => Player.PlayerQuality.Flexible,
                    1 => Player.PlayerQuality.Strategic,
                    2 => Player.PlayerQuality.SharesTheirExpertise,
                    3 => Player.PlayerQuality.RespectfulToOthers,
                    4 => Player.PlayerQuality.ContributeIdeas,
                    _ => throw new NotImplementedException()
                };

                var player = new Player(name, pathToImage, playerQuality);

                using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
                conn.CreateTable<Player>();
                conn.Insert(player);

                await _navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You didn't fill everything", "OK");
            }
        }
    }
}