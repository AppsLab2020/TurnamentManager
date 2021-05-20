using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchPage : ContentPage
    {
        private readonly MatchModel _model;
        public MatchPage(int tournamentId)
        {
            InitializeComponent();
            _model = new MatchModel(Navigation, tournamentId);
            BindingContext = _model;

            Layout.Children.Add(_model.GetSpider());
        }
    }
}