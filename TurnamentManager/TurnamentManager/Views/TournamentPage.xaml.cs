using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TournamentPage : ContentPage
    {
        private TournamentModel model;
        public TournamentPage()
        {
            InitializeComponent();

            model = new TournamentModel(Navigation);

            BindingContext = model;

            model.RedrawPlayers += OnRedrawPlayers;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await DrawTournaments();
        }

        private async void OnRedrawPlayers(object sender, EventArgs e)
        {
            await DrawTournaments();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await PlusButton.TranslateTo(10, 0, 500, Easing.BounceOut);
            await PlusButton.TranslateTo(0, 0);
            await Navigation.PushAsync(new CreateTournamentPage());
        }
        
        private async Task DrawTournaments()
        {
            var list = new List<Frame>();
            TournamentLayout.Children.Clear();

            await model.GetFrames(list);

            foreach (var frame in list)
            {
                TournamentLayout.Children.Add(frame);
            }
        }
    }
}