using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TournamentSetupPage : TabbedPage
    {
        private int tournamentID;
        public TournamentSetupPage(int tournamentID)
        {
            InitializeComponent();
            var clr = Color.FromHex("#D7812A");
            this.BarBackgroundColor = clr;
            
            this.tournamentID = tournamentID;
        }
        public TournamentSetupPage()
        {
            InitializeComponent();
        }
    }
}