using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TurnamentManager
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void LogInButton_OnClicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (isEmailEmpty != true && isPasswordEmpty != true)
            {
                DisplayAlert("TRUE ALERT", "it worked", "OK");
            }

            else
            {
                DisplayAlert("FALSE ALERT", "it worked", "OK");
            }
        }
    }
}
