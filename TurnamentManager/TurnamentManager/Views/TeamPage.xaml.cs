using System;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamPage : ContentPage
    {
        public TeamPage()
        {
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
            
            if (selectedImage == null)
            {
                await DisplayAlert("Error", "could not ger image", "ok");
            }
            
            selectedImage.Source = ImageSource.FromStream(()=> selectedImageFile.GetStream());
        }
    }
}