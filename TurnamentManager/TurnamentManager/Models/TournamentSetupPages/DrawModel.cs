using TurnamentManager.Views.TournamentSetupPages;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class DrawModel
    {
      
        public Command NavigateCommand { get; set; }

            public INavigation Navigation;

            public DrawModel(INavigation navigation)
            {
                Navigation = navigation;
                NavigateCommand = new Command(Navigate);
            }

            private void Navigate()
            {
                Navigation.PushAsync(new ManualPage());
            }
        }
    }

