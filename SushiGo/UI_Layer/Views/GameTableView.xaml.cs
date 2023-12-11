using Logic_Layer;
using Logic_Layer.cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI_Layer.UserControls;

namespace UI_Layer.Views
{
    /// <summary>
    /// Logique d'interaction pour GameTable.xaml
    /// </summary>
    public partial class GameTableView : Window
    {
        public GameTableView(Logic_Layer.Table t)
        {
            InitializeComponent();
            MainWindowViewModel.Instance.NavigationViewModel.CurrentWindow = this;
            DataContext = MainWindowViewModel.Instance;

            //TODO : Attention à la dupplication de code entre ce qui est ici et dans le GameTableViewModel (propriété Deck)
            Player thisPLayer = t.Players[0];
            player = new PlayerViewModel(thisPLayer, PlayerType.PLAYER);
            KeyDown += Show_Leaderboard;
        }

        /// <summary>
        /// Permet d'afficher le tableau des scores
        /// </summary>
        private void Show_Leaderboard(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Tab)
            {
                MainWindowViewModel.Instance.GameTableViewModel.ShowLeaderboard = !MainWindowViewModel.Instance.GameTableViewModel.ShowLeaderboard;
            }
            
        }

    }
}
