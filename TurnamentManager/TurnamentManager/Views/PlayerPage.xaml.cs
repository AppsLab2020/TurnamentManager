using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreatePlayerPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3")))
            {
                
            }
        }
    }
}