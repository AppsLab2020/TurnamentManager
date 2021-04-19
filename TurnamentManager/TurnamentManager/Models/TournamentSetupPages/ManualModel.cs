using TurnamentManager.Views;
using TurnamentManager.Views.TournamentSetupPages;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class ManualModel
    {
       public Command NavigateToNextCommand { get; set; }

       public INavigation Navigation;

       public ManualModel(INavigation navigation)
       {
           Navigation = navigation;
           NavigateToNextCommand = new Command(Navigate);
       }

       private void Navigate()
       {
           Navigation.PushAsync(new MatchPage());
       }
    }
}