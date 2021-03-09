using System;
using Xamarin.Forms;

namespace TurnamentManager.Views
{
    public partial class TournamentPage : ContentPage
    {
        public TournamentPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateTournamentPage());
        }
    }
}