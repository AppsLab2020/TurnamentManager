using System;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
        }

        

        private void Button_OnClicked(object sender, EventArgs e)
        {
            /*await this.TranslateTo(10, 0, 500, Easing.BounceOut);
           // await this.TranslateTo(0, 0);*/
           Navigation.PushAsync(new CreateTeamPage());
        }
        
    }
}