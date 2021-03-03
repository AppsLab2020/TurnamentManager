using System;
using Xamarin.Forms;

namespace TurnamentManager.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private void LogInButton_OnClicked(object sender, EventArgs e)
        {
            var isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            var isPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (isEmailEmpty)
            {
                DisplayAlert("", "Please enter Email", "OK");
            }
            else if (isPasswordEmpty)
            {
                DisplayAlert("", "Please enter Password", "OK");
            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
