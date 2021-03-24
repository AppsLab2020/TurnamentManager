using System;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTeamPage : ContentPage
    {
        public CreateTeamPage()
        {
            InitializeComponent();
            BindingContext = new CreateTeamModel();
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            await SaveButton.ScaleTo(1.2, 500, Easing.SpringOut);
            await SaveButton.ScaleTo(1, 300);
        }
    }
}

//List is not refreshing when someone delete item view doesn't refresh after