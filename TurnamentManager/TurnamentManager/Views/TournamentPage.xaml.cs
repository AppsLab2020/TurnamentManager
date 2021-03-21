using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using TurnamentManager.Classes.Tournament;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TournamentPage : ContentPage
    {
        private static ICommand RemoveCommand => new Command<int>(RemoveTournament);
        private static ICommand TapCommand => new Command<int>(OpenPage);

        private static int _changeTournamentID = -1;

        private static void OpenPage(int id)
        {
            App.Current.MainPage = new NavigationPage(new PlayerOrTeamAddPage(id));
        }

        public TournamentPage()
        {
            InitializeComponent();
            
            var th = new Thread(Switcher);
            th.Start();
        }

        private void Switcher()
        {
            while (_changeTournamentID == -1)
            {
                
            }

            SwitchOnClick(this, EventArgs.Empty);
        }
        
        private void SwitchOnClick(object sender, EventArgs e)
        {
            lottie.PlayAnimation();
            Navigation.PushAsync(new TournamentSetupPage());
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            
            Navigation.PushAsync(new CreateTournamentPage());
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            await DrawTournaments();
        }

        private async Task DrawTournaments()
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.CreateTable<Tournament>();
            var tournaments = conn.Table<Tournament>().ToList();

            TournamentLayout.Children.Clear();
            foreach (var tournament in tournaments)
            {
                var frame = await Task.Run(() => MakeFrameAsync(tournament));
                TournamentLayout.Children.Add(frame);
            }
        }
        
        private static async Task<Frame> MakeFrameAsync(Tournament tournament)
        {
            var label = new Label
            {
                Text = tournament.Name,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            
            var imageButton = new ImageButton
            {
                Source = "trash_bin.png",
                Command = RemoveCommand,
                BackgroundColor = Color.Transparent,
                CommandParameter = tournament.ID,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };
            
            var st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                WidthRequest = 300,
                HeightRequest = 80,
            };

            var frame = new Frame
            {
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HasShadow = true,
                IsClippedToBounds = true,
                
            };

            var tap = new TapGestureRecognizer {Command = TapCommand, CommandParameter = tournament.ID};

            frame.GestureRecognizers.Add(tap);
            
            st.Children.Add(label);
            st.Children.Add(imageButton);
            frame.Content = st;

            return frame;
        }
        
        private static void RemoveTournament(int id)
        {
            using var conn = new SQLiteConnection(Path.Combine(App.FolderPath, "tournaments.db3"));
            conn.Query<Player>("DELETE FROM Tournament Where ID=?", id);
        }
    }
}