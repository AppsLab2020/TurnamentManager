using System;
using System.IO;
using TurnamentManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            BindingContext = new HistoryModel();
            
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            View.PlayAnimation();
        }

       
    }
}