using Logic_Layer;
using Logic_Layer.cards;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI_Layer.UserControls;
using UI_Layer.ViewModels;

namespace UI_Layer.Views
{
    /// <summary>
    /// Logique d'interaction pour GameTable.xaml
    /// </summary>
    public partial class GameTableView : Window
    {
        private PlayerViewModel player;
        
        public GameTableView(Logic_Layer.Table t)
        {
            InitializeComponent();
            MainWindowViewModel.Instance.NavigationViewModel.CurrentWindow = this;
            DataContext = MainWindowViewModel.Instance;

            Player thisPLayer = t.Players[0];

            player = new PlayerViewModel(thisPLayer, PlayerType.PLAYER);
        }


        private void EndPlayerTurn(object sender, RoutedEventArgs e)
        {
            player.IsTurnFinished = true;
        }
    }
}
