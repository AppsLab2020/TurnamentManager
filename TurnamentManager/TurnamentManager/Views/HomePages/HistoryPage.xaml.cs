using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryModel _model;
        public HistoryPage()
        {
            InitializeComponent();
            _model = new HistoryModel(Navigation);
            BindingContext = _model;
            
            _model.RedrawPlayers += OnRedrawPlayers;
            
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
        
        private async Task DrawTournaments()
        {
            var list = new List<Frame>();
            TournamentLayout.Children.Clear();

            await _model.GetFrames(list);

            foreach (var frame in list)
            {
                TournamentLayout.Children.Add(frame);
            }
        }
    }
}