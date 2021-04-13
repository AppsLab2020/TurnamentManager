using System;
using TurnamentManager.Views;
using TurnamentManager.Views.TournamentSetupPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager
{
    public partial class App : Application
    {

        public static string FolderPath;
        public App(string folderPath)
        {
            InitializeComponent();

            Device.SetFlags(new []{"Expander_Experimental"});
            FolderPath = folderPath;
            MainPage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Color.FromHex("#D7812A"),
                BarTextColor = Color.White,
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
