using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        private PlayerModel model;
        public PlayerPage()
        {
            InitializeComponent();
            model = new PlayerModel(Navigation);

            BindingContext = model;

            model.RedrawPlayers += async (sender, args) =>
            {
                await DrawPlayers();
            };
        }
        
       private async void Button_OnClicked(object sender, EventArgs e)
        {
            await PlusButton.TranslateTo(10, 0, 500, Easing.BounceOut);
            await PlusButton.TranslateTo(0, 0);
            await Navigation.PushAsync(new CreatePlayerPage());
        }


       public double height = 200;
       protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            await DrawPlayers();
        }

        private async Task DrawPlayers()
        {
            var lst = new List<Frame>();

            await model.GetFrames(lst);

            
            PlayerStackLayout.Children.Clear();
            
            foreach (var frame in lst)
            {
                PlayerStackLayout.Children.Add(frame);
            }
        }
    }
}

//Add removing data from database at one point 