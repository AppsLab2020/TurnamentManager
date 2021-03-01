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
