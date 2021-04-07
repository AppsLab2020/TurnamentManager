using System;
using System.IO;
using SQLite;
using TurnamentManager.Classes.Tournament;
using TurnamentManager.Models;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTournamentPage : ContentPage
    {
        public CreateTournamentPage()
        {
            InitializeComponent();
            BindingContext = new CreateTournamentModel(Navigation);
        }
        
    }
}