using System.Windows;
using UI_Layer.ViewModels;

namespace UI_Layer
{
    /// <summary>
    /// Logique d'interaction pour HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        public HomeView()
        {
            InitializeComponent();
            this.DataContext = new HomeViewModel(this);
        }
    }
}
