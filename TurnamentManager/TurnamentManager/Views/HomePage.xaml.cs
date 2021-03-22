using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurnamentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            var clr = Color.FromHex("#D7812A");
            this.BarBackgroundColor = clr;
       
        }
        public interface IStatusBarPlatformSpecific
        {
            void SetStatusBarColor(Color color);
        }
    }
}