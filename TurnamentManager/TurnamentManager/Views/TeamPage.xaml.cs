﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamPage : ContentPage
    {
        public TeamPage()
        {
            InitializeComponent();
            BindingContext = new TeamModel(Navigation);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await PlusButton.TranslateTo(10, 0, 500, Easing.BounceOut);
            await PlusButton.TranslateTo(0, 0);
            await Navigation.PushAsync(new CreateTeamPage());
        }
        
    }
}