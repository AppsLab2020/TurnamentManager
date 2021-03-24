using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Views;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class CreatePlayerModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand PickPhotoCommand { get; set; }

        public ICommand SaveDataCommand { get; set; }

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

        private string _imagePath;

        private readonly INavigation _navigation;

        public CreatePlayerModel(INavigation navigation)
        {
            _pickImageSource = "upload_pic.png";
            PickPhotoCommand = new Command(PickImage);
            SaveDataCommand = new Command(SaveData);
            _navigation = navigation;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void PickImage()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Not supported", "not supported", "ok");
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "could not get image", "ok");
                return;
            }

            PickedImageSource = ImageSource.FromStream(() => selectedImageFile.GetStream());
            _imagePath = selectedImageFile.Path;
        }

        private async void SaveData()
        {
            var name = PlayerName;
            var pathToImage = _imagePath;
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
    }
}