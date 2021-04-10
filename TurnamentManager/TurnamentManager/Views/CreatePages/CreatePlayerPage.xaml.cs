using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using TurnamentManager.Models;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlayerPage : ContentPage
    {
        public CreatePlayerPage()
        {
            InitializeComponent();
            BindingContext = new CreatePlayerModel(Navigation);
        }
    }
}