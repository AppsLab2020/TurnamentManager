using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using TurnamentManager.Models;
using TurnamentManager.Views.PopOutPages;
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

        public MatchPage()
        {
            
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new MatchResultsPage());
        }
    }
}