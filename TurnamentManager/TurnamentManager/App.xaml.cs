using System;
using TurnamentManager.Views;
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

            MainPage = new NavigationPage(new HomePage());
            FolderPath = folderPath;
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
