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

            if (isEmailEmpty || isPasswordEmpty)
            {
                DisplayAlert("", "Please enter email and password", "OK");
            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
