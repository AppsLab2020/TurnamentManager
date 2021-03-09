using System;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlayerPage : ContentPage
    {
        private string _imagePath;
        public CreatePlayerPage()
        {
            _imagePath = "";
            InitializeComponent();
        }
        
        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not supported", "not supported", "ok");
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            _imagePath = selectedImageFile.Path;
            
            if (selectedImage == null)
            {
                await DisplayAlert("Error", "could not get image", "ok");
            }
            
            selectedImage.Source = ImageSource.FromStream(()=> selectedImageFile.GetStream());
        }
        
        private void SaveButton_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_imagePath))
            {
                return; //Vypis ze chce obrazek !!!
            }
            
            var name = Name.Text;
            var pathToImage = _imagePath;
            var quality = Quality.SelectedIndex;
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

            Navigation.PushAsync(new HomePage());
        }
    }
}