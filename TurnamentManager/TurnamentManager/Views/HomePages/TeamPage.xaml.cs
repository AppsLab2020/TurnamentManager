using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamPage : ContentPage
    {
        private TeamModel _model;
        public TeamPage()
        {
            InitializeComponent();
            _model = new TeamModel(Navigation);

            BindingContext = _model;
        }
    }
}