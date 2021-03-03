using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TournamentPage : ContentPage
    {
        public TournamentPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateTournamentPage());;
        }
    }
}