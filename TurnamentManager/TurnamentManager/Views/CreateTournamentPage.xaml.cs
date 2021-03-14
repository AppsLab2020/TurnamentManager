using System;
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

        private void SaveButton_OnClicked(object sender, EventArgs e)
        {
            var tournament = new Tournament()
            {
                Format = Format.SelectedIndex switch
                {
                    0 => "League",
                    1 => "Groups",
                    //....
                    _ => throw new ArgumentOutOfRangeException()
                },
                IsTeamBased = false,//Add button to choose
                Name = Name.Text,
                Style = Sports.SelectedIndex switch
                {
                    0 => "Football",
                    1 => "Basketball",
                    _ => throw new ArgumentOutOfRangeException()
                }
            };
        }
    }
}