using System;
using System.IO;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTournamentPage : ContentPage
    {
        public CreateTournamentPage()
        {
            InitializeComponent();
        }

       async private void SaveButton_OnClicked(object sender, EventArgs e)
        { 
            await SaveButton.ScaleTo(1.2, 500, Easing.SpringOut); 
            await SaveButton.ScaleTo(1, 300);
            var tournament = new Tournament()
            {
                Format = Format.SelectedIndex switch
                {
                    0 => "Knockout",
                    1 => "Groups",
                    //....
                    _ => throw new ArgumentOutOfRangeException()
                },
                IsTeamBased = Type.SelectedIndex switch
                {
                    0 => true,
                    1 => false,
                    _ => throw new ArgumentOutOfRangeException()
                },//Add button to choose
                Name = Name.Text,
                Style = Sports.SelectedIndex switch
                {
                    0 => "Football",
                    1 => "Basketball",
                    _ => throw new ArgumentOutOfRangeException()
                }
            };
            
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            conn.Insert(tournament);

           await Navigation.PopAsync();
        }
    }
}