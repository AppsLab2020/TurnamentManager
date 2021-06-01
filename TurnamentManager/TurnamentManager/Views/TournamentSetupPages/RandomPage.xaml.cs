using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views.TournamentSetupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RandomPage : ContentPage
    {
        private RandomModel _model;
        public RandomPage(int tournamentId)
        {
            InitializeComponent();
            _model = new RandomModel(Navigation, tournamentId);
            BindingContext = _model;

            var frames = _model.GetRandomDraw();

            foreach (var frame in frames)
            {
                StackLayout.HeightRequest += 180;
                FrameLayout.HeightRequest += 150;
                FrameLayout.Children.Add(frame);
            }
            
            _model.RedrawPlayers += (sender, args) =>
            {
                FrameLayout.Children.Clear();
                foreach (var frame in _model.GetRandomDraw())
                {
                    StackLayout.HeightRequest += 180;
                    FrameLayout.HeightRequest += 150;
                    FrameLayout.Children.Add(frame);
                }
            };
        }
    }
}