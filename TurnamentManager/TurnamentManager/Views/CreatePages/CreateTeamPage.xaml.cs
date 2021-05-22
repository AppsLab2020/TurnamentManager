﻿using System;
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
            BindingContext = new CreateTeamModel(Navigation);
            
            
       
        }
    }
}
