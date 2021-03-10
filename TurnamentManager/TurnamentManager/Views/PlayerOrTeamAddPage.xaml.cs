using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;
using CheckBox = XLabs.Forms.Controls.CheckBox;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerOrTeamAddPage : ContentPage
    {
        public PlayerOrTeamAddPage()
        {
            InitializeComponent();
            
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "players.db3"));
            conn.CreateTable<Player>();
            var players = conn.Table<Player>().ToList();
            
            
            foreach (var player in players)
            {
                var checkBox = new CheckBox
                {
                    DefaultText = player.Name,
                    TextColor = Color.Black,
                };
                
                
                testS.Children.Add(checkBox);
            }
            
        }

      
    }
}