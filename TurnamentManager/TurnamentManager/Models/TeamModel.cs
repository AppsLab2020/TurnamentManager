using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;

namespace TurnamentManager.Views
{
    public class TeamModel
    {
        public Command NextCommand { get; set; }

        private INavigation _navigation;
        public TeamModel(INavigation navigation)
        {
            NextCommand = new Command(Next);
            _navigation = navigation;
        }

        private void Next()
        {
            _navigation.PushAsync(new CreateTeamPage());
        }
    }
}