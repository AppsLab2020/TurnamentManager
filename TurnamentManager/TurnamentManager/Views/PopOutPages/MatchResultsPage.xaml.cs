using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views.PopOutPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchResultsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public MatchResultsPage()
        {
            InitializeComponent();
        }
        
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}