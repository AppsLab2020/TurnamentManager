using TurnamentManager.Models.PopOutModels;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views.PopOutPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchResultsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        
        
        public MatchResultsPage(string match, int tournamentId)
        {
            InitializeComponent();
            var model = new MatchResultModel(match, tournamentId, Navigation);
            BindingContext = model;
        }
        
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

    }
}