using System.Windows.Input;
using TurnamentManager.Views;
using Xamarin.Forms;

namespace TurnamentManager.Models
{
    public class PlayerOrTeamAddModel
    {
        public ICommand NextCommand { get; set; }

        private INavigation _navigation;
        public PlayerOrTeamAddModel(INavigation navigation)
        {
            _navigation = navigation;
            NextCommand = new Command(Next);
        }

        private void Next()
        {
            _navigation.PushAsync(new DrawPage());
        }
    }
}