using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views.TournamentSetupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualPage : ContentPage
    {
        private ManualModel _model;

        public ManualPage(int tournamentId)
        {
            InitializeComponent();
            _model = new ManualModel(Navigation, tournamentId);
            BindingContext = _model;

            _model.FullMatchEventHandler += (sender, args) =>
            {
                //Daco
            };
        }
    }
}